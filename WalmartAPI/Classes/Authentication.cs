using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using Serilog;
using Extensions;
using System.Net.Http;

namespace WalmartAPI.Classes
{

    public class Authentication
    {
        #region constructors

        public Authentication()
        {
            //set timestemp
            var ts = DateTimeOffset.UtcNow;
            timeStamp = ts.ToUnixTimeMilliseconds().ToString();
            correlationId = Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// Create an authentication object to be used with a request to walmart.com API
        /// </summary>
        /// <param name="consumerId">WalMart provided ConsumerID</param>
        /// <param name="baseUrl">The url for the request to be authenticated</param>
        /// <param name="privateKey">Provided by walmart</param>
        /// <param name="httpRequestMethod">the request method</param>
        /// <param name="channelType">Channel type provided by walmart</param>
        public Authentication(string consumerId,Uri baseUrl,string privateKey, HttpMethod httpRequestMethod,string channelType) : this(consumerId,privateKey)
        {
            //this.consumerId = consumerId;
            this.baseUrl = baseUrl.AbsoluteUri;
            //this.privateKey = privateKey;
            this.httpRequestMethod = httpRequestMethod.Method;
            this.channelType = channelType;
            signData();
        }

        public Authentication(string consumerId,string privateKey) : this()
        {
            this.consumerId = consumerId;
            this.privateKey = privateKey;
        }
        public Authentication(string consumerId,string privateKey,string channelType) : this(consumerId, privateKey)
        {
            this.channelType = channelType;
        }
        #endregion

        #region Properties

        public string consumerId { get; set; }
        public string baseUrl { get; set; }
        public string privateKey { get; set; }
        public string httpRequestMethod { get; set; }
        public string timeStamp { get; set; }
        public string signature { get; set; }
        public string channelType { get; set; }
        public string correlationId { get; set; }

        #endregion

        public void signData()
        {
            Log.Verbose("Begining signData()");
            try
            {
                //set timestemp
                //var ts = DateTimeOffset.UtcNow;
                //timeStamp = ts.ToUnixTimeMilliseconds().ToString();
                var strToSign = string.Format("{0}\n{1}\n{2}\n{3}\n", consumerId, baseUrl, httpRequestMethod, timeStamp);

                //Decoding the Base 64, PKCS - 8 representation of your private key.Note that the key is encoded using PKCS-8. Libraries in various languages offer the ability to specify that the key is in this format and not in other conflicting formats such as PKCS-1.
                var decoded = Convert.FromBase64String(privateKey);
                var byteTosign = Encoding.Default.GetBytes(strToSign);

                string signed;
                using (var bb = CngKey.Import(decoded, CngKeyBlobFormat.Pkcs8PrivateBlob))
                {
                    using (var rsa = new RSACng(bb))
                    {
                        //Use this byte representation of your key to sign the data using SHA-256 With RSA.
                        var signedBytes = rsa.SignData(byteTosign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                        signed = Convert.ToBase64String(signedBytes);
                    }
                }
                //Encode the resulting signature using Base 64.
                signature = signed;
                Log.Debug("Signature set to {signature} for {timestemp}", signature,timeStamp);
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }
    }
}
