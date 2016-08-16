using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Configuration;

namespace WalmartAPI
{

    public class Authentication
    {
        #region Properties

        public string consumerId { get; set; }
        public string baseUrl { get; set; }
        public string privateKey { get; set; }
        public string httpRequestMethod { get; set; }
        public string timeStemp { get; set; }
        public string signature { get; set; }
        public string channelType { get; set; }
        public string correlationId { get; set; }

        #endregion
        public void signData()
        {
            //set timestemp
            var ts = DateTimeOffset.UtcNow;
            timeStemp  =ts.ToUnixTimeMilliseconds().ToString();
            var strToSign = string.Format("{0}\n{1}\n{2}\n{3}\n", consumerId, baseUrl, httpRequestMethod, timeStemp);
            
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
        }
    }
}
