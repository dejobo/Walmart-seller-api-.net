using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Net.Http;
using Serilog;
using Extensions;
using Serilog.Context;

namespace WalmartAPIOOP
{

    public class Authentication : IWMOperation
    {
        #region Properties

        public string consumerId { get; private set; }
        private string baseUrl { get; set; }
        private string privateKey { get; set; }
        private string httpRequestMethod { get; set; }
        private string timeStamp { get; set; }
        public string signature { get; private set; }
        private string channelType { get; set; }
        public string correlationId { get; private set; }
        public bool IsSigned { get; set; }

        public Authentication authentication { get { return this; } }
        #endregion

        #region constructors
        private Authentication()
        {
            timeStamp = getTimestemp();
            correlationId = Guid.NewGuid().ToString().Replace("-", "");
            IsSigned = false;
        }

        /// <summary>
        /// Create an authentication object to be used with a request to walmart.com API
        /// </summary>
        /// <param name="consumerId">WalMart provided ConsumerID</param>
        /// <param name="baseUrl">The url for the request to be authenticated</param>
        /// <param name="privateKey">Provided by walmart</param>
        /// <param name="httpRequestMethod">the request method</param>
        /// <param name="channelType">Channel type provided by walmart</param>
        private Authentication(string consumerId,string baseUrl,string privateKey, string httpRequestMethod,string channelType) : this()
        {
            this.consumerId = consumerId;
            this.baseUrl = baseUrl;
            this.privateKey = privateKey;
            this.httpRequestMethod = httpRequestMethod;
            this.channelType = channelType;
        }

        public Authentication(string consumerId, Uri baseUrl, string privateKey, HttpMethod httpRequestMethod, string channelType) : this(consumerId, baseUrl.AbsoluteUri, privateKey, httpRequestMethod.Method, channelType) { }
        #endregion

        #region Fluent Methods

        public Authentication Sign() =>
            Sign("");

        public Authentication Sign(string baseUrl)
        {
            using (LogContext.PushProperty("MethodSignature", "signData(string)"))
            {
                Log.Verbose("Begining signData()");

                var auth = new Authentication(
                this.consumerId,
                string.IsNullOrEmpty(baseUrl) ? this.baseUrl : baseUrl,
                this.privateKey,
                this.httpRequestMethod,
                this.channelType);

                try
                {
                    var strToSign = string.Format("{0}\n{1}\n{2}\n{3}\n", auth.consumerId, auth.baseUrl, auth.httpRequestMethod, auth.timeStamp);

                    Log.Verbose("string to sign set to {strToSign}", strToSign);

                    //Decoding the Base 64, PKCS - 8 representation of your private key.Note that the key is encoded using PKCS-8. Libraries in various languages offer the ability to specify that the key is in this format and not in other conflicting formats such as PKCS-1.
                    var decoded = Convert.FromBase64String(auth.privateKey);
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

                    auth.signature = signed;
                    auth.IsSigned = true;

                    Log.Verbose("Signature set to {signature} for {timestemp} and url {url}", signature, timeStamp, baseUrl);

                    return auth;
                }
                catch (Exception ex)
                {
                    ex.LogWithSerilog();
                    throw;
                }
            }
        }

        #endregion

        #region Internal Methods
        private string getTimestemp()
        {
            //set timestemp
            var ts = DateTimeOffset.UtcNow;
            return ts.ToUnixTimeMilliseconds().ToString();
        }
        #endregion

        public IWMOperation Request<T>() where T : IWMOperation
        {
            throw new NotImplementedException();
        }

        public IWMOperation Response<T>() where T : IWMOperation
        {
            throw new NotImplementedException();
        }
    }
}
