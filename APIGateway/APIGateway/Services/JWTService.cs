using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;


namespace APIGateway.Services
{
    public class JWTService
    {



        //  private static final Logger LOGGER = LoggerFactory.getLogger(JWTService.class);

        public String sign(String plainText)
        {
          
            //initialise the JsonWebSignature
            JsonWebSignature jws = new JsonWebSignature();
            // Set the payload, or signed content, on the JWS object
            jws.setPayload(plainText);
            // Set the signature algorithm on the JWS that will integrity protect the payload
            jws.setAlgorithmHeaderValue(AlgorithmIdentifiers.RSA_USING_SHA256);
            /* Set the signing key on the JWS
             Note that your application will need to determine where/how to get the key
             and here we just use an example from the JWS spec*/
            PrivateKey privateKey = jwtKeyLoader.getPrivateKey();
            jws.setKey(privateKey);
            //set the certificate on JWS Token
            X509Certificate certificate = jwtKeyLoader.getCertificate();
            jws.setCertificateChainHeaderValue(certificate);
            /* Sign the JWS and produce the compact serialization or complete JWS representation, which
             is a string consisting of three dot ('.') separated base64url-encoded
             parts in the form Header.Payload.Signature*/
            String jwsCompactSerialization = jws.getCompactSerialization();
            //make the Detached JWS
            int first = jwsCompactSerialization.IndexOf(".");
            int last = jwsCompactSerialization.LastIndexOf(".");
            String sign = jwsCompactSerialization.Substring(0, first + 1) + jwsCompactSerialization.Substring(last);
            return sign;
        }

        public void validate(String jwsText, String plainText)
        {
            // Create a new JsonWebSignature object
            JsonWebSignature jws = new JsonWebSignature();
            // create proper JWS String from detached
            String newEncodedText = Base64Url.encode(plainText.getBytes(StandardCharsets.UTF_8));
            int first = jwsText.IndexOf(".");
            int last = jwsText.LastIndexOf(".");
            jwsText = jwsText.Substring(0, first + 1) + newEncodedText + jwsText.Substring(last);
            // Set the algorithm constraints based on what is agreed upon or expected from the sender
            jws.setAlgorithmConstraints(new AlgorithmConstraints(AlgorithmConstraints.ConstraintType.WHITELIST,
                    AlgorithmIdentifiers.RSA_USING_SHA256));
            // Set the compact serialization on the JWS
            jws.setCompactSerialization(jwsText);
            Headers headers = jws.getHeaders();
            String alg = headers.getStringHeaderValue("alg");
            if (!AlgorithmIdentifiers.RSA_USING_SHA256.equals(alg))
            {
                // LOGGER.error("Received Header does not match algorithm: or, AlgorithmIdentifiers.RSA_USING_SHA256");
                throw new Exception("INVALID_SYSTEM_SIGNATURE");
            }
            // The verification key on the JWS is the public key from the JWK we pulled from the JWK Set.
            X509Certificate userCertificate = jws.getCertificateChainHeaderValue().get(0);
            jws.setKey(userCertificate.getPublicKey());
            return jws.verifySignature();
        }
    }

    class JWS_Signature_RFC7515
    {
        static void Main(string[] args)
        {
            JWS_RFC7515_Example();
        }
        static void JWS_RFC7515_Example()
        {
            int n;

            Console.WriteLine("Creating JSON Web Signature...");

            // Read in JWS RSA private key from hard-coded string
            string jwsPriKey = "{\"kty\":\"RSA\"," +
                "\"n\":\"ofgWCuLjybRlzo0tZWJjNiuSfb4p4fAkd_wWJcyQoTbji9k0l8W26mPddxHmfHQp-Vaw-4qPCJrcS2mJPMEzP1Pt0Bm4d4QlL-yRT-SFd2lZS-pCgNMsD1W_YpRPEwOWvG6b32690r2jZ47soMZo9wGzjb_7OMg0LOL-bSf63kpaSHSXndS5z5rexMdbBYUsLA9e-KXBdQOS-UTo7WTBEMa2R2CapHg665xsmtdVMTBQY4uDZlxvb3qCo5ZwKh9kG4LT6_I5IhlJH7aGhyxXFvUK-DWNmoudF8NAco9_h9iaGNj8q2ethFkMLs91kzk2PAcDTW9gb54h4FRWyuXpoQ\"," +
                "\"e\":\"AQAB\"," +
                "\"d\":\"Eq5xpGnNCivDflJsRQBXHx1hdR1k6Ulwe2JZD50LpXyWPEAeP88vLNO97IjlA7_GQ5sLKMgvfTeXZx9SE-7YwVol2NXOoAJe46sui395IW_GO-pWJ1O0BkTGoVEn2bKVRUCgu-GjBVaYLU6f3l9kJfFNS3E0QbVdxzubSu3Mkqzjkn439X0M_V51gfpRLI9JYanrC4D4qAdGcopV_0ZHHzQlBjudU2QvXt4ehNYTCBr6XCLQUShb1juUO1ZdiYoFaFQT5Tw8bGUl_x_jTj3ccPDVZFD9pIuhLhBOneufuBiB4cS98l2SR_RQyGWSeWjnczT0QU91p1DhOVRuOopznQ\"," +
                "\"p\":\"4BzEEOtIpmVdVEZNCqS7baC4crd0pqnRH_5IB3jw3bcxGn6QLvnEtfdUdiYrqBdss1l58BQ3KhooKeQTa9AB0Hw_Py5PJdTJNPY8cQn7ouZ2KKDcmnPGBY5t7yLc1QlQ5xHdwW1VhvKn-nXqhJTBgIPgtldC-KDV5z-y2XDwGUc\"," +
                "\"q\":\"uQPEfgmVtjL0Uyyx88GZFF1fOunH3-7cepKmtH4pxhtCoHqpWmT8YAmZxaewHgHAjLYsp1ZSe7zFYHj7C6ul7TjeLQeZD_YwD66t62wDmpe_HlB-TnBA-njbglfIsRLtXlnDzQkv5dTltRJ11BKBBypeeF6689rjcJIDEz9RWdc\"," +
                "\"dp\":\"BwKfV3Akq5_MFZDFZCnW-wzl-CCo83WoZvnLQwCTeDv8uzluRSnm71I3QCLdhrqE2e9YkxvuxdBfpT_PI7Yz-FOKnu1R6HsJeDCjn12Sk3vmAktV2zb34MCdy7cpdTh_YVr7tss2u6vneTwrA86rZtu5Mbr1C1XsmvkxHQAdYo0\"," +
                "\"dq\":\"h_96-mK1R_7glhsum81dZxjTnYynPbZpHziZjeeHcXYsXaaMwkOlODsWa7I9xXDoRwbKgB719rrmI2oKr6N3Do9U0ajaHF-NKJnwgjMd2w9cjz3_-kyNlxAr2v4IKhGNpmM5iIgOS1VZnOZ68m6_pbLBSp3nssTdlqvd0tIiTHU\"," +
                "\"qi\":\"IYd7DHOhrWvxkwPQsRM2tOgrjbcrfvtQJipd-DlcxyVuuM9sQLdgjVk2oy26F0EmpScGLq2MowX7fhd_QJQ3ydy5cY7YIBi87w93IKLEdfnbJtoOPLUW0ITrJReOgo1cq9SbsxYawBgfp_gh6A5603k2-ZQwVK0JKSHuLFkuQ3U\"" +
            "}";

            // Read JWS key string into ephemeral RSA private key string and display key characteristics
            // (we don't need to do this to make a signature, it's just a check all is OK)
            string privateKey = Rsa.ReadPrivateKey(jwsPriKey, "").ToString();
            Debug.Assert(privateKey.Length > 0);
            Console.WriteLine("Key length={0} bits", Rsa.KeyBits(privateKey));
            Console.WriteLine("Key hash=0x{0,8:X}", Rsa.KeyHashCode(privateKey));

            // Compose JWS Protected Header
            string header = "{\"alg\":\"RS256\"}";
            Console.WriteLine("JWS Protected Header={0}", header);
            string headerURL = cnvBase64urlFromString(header);
            Console.WriteLine("BASE64URL(UTF8(JWS Protected Header))={0}", headerURL);

            // Compose JWS Payload (note CR-LF line endings and single space indents)
            string payload = "{\"iss\":\"joe\"," + "\r\n" +
                " \"exp\":1300819380," + "\r\n" +
                " \"http://example.com/is_root\":true}";
            Console.WriteLine("JWS Payload={0}", payload);
            string payloadURL = cnvBase64urlFromString(payload);
            Console.WriteLine("BASE64URL(UTF8(JWS Payload))={0}", payloadURL);

            // Compose JWS Signing Input
            // BASE64URL(UTF8(JWS Protected Header) || '.' || BASE64URL(JWS Payload)
            string jwsSigningInput = headerURL + "." + payloadURL;
            Console.WriteLine("JWS Signing Input={0}", jwsSigningInput);

            // Encode signing input as a byte array
            byte[] b = System.Text.Encoding.Default.GetBytes(jwsSigningInput);

            // Compute JWS Signature value BASE64URL(JWS Signature)
            // -- Note we can use the JSW key string directly here. There is no password.
            string jwsSignature = Sig.SignData(b, jwsPriKey, "", SigAlgorithm.Rsa_Sha256, Sig.SigOptions.Default, Sig.Encoding.Base64url);
            Console.WriteLine("BASE64URL(JWS Signature)={0}", jwsSignature);

            // The correct signature value from RFC 7515
            string jwsSigOK = "cC4hiUPoj9Eetdgtv3hF80EGrhuB__dzERat0XF9g2VtQgr9PJbu3XOiZj5RZmh7AAuHIm4Bh-0Qc_lF5YKt_O8W2Fp5jujGbds9uJdbF9CUAr7t1dnZcAcQjbKBYNX4BAynRFdiuB--f_nZLgrnbyTyWzO75vRK5h6xBArLIARNPvkSjtQBMHlb1L07Qe7K0GarZRmB_eSN9383LcOLn6_dO--xi12jzDwusC-eOkHWEsqtFZESc6BfI7noOPqvhJ1phCnvWh6IeYI2w9QOYEUipUTI8np6LbgGY9Fs98rqVt5AXLIhWkWywlVmtVrBp0igcN_IoypGlUPQGe77Rw";
            Console.WriteLine("Correct value           ={0}", jwsSigOK);
            Debug.Assert(jwsSigOK == jwsSignature, "Signature does not match reference");

            // Output full JWS Compact Serialization
            // Header.Payload.Signature with period ('.') characters between the parts, all parts base64url encoded.
            Console.WriteLine("JWS Compact Serialization=");
            Console.WriteLine(headerURL + "." + payloadURL + "." + jwsSignature);

            // ------------------
            // VALIDATE SIGNATURE
            // ------------------
            Console.WriteLine();
            Console.WriteLine("Validating signature...");
            // INPUT: JwsSignature, JwsSigningInput, PublicKey, SigningAlgorithm
            // OUTPUT: Signature is valid (returns 0) or invalid

            string jwsPubKey = "{\"kty\":\"RSA\"," +
                "\"n\":\"ofgWCuLjybRlzo0tZWJjNiuSfb4p4fAkd_wWJcyQoTbji9k0l8W26mPddxHmfHQp-Vaw-4qPCJrcS2mJPMEzP1Pt0Bm4d4QlL-yRT-SFd2lZS-pCgNMsD1W_YpRPEwOWvG6b32690r2jZ47soMZo9wGzjb_7OMg0LOL-bSf63kpaSHSXndS5z5rexMdbBYUsLA9e-KXBdQOS-UTo7WTBEMa2R2CapHg665xsmtdVMTBQY4uDZlxvb3qCo5ZwKh9kG4LT6_I5IhlJH7aGhyxXFvUK-DWNmoudF8NAco9_h9iaGNj8q2ethFkMLs91kzk2PAcDTW9gb54h4FRWyuXpoQ\"," +
                "\"e\":\"AQAB\"" +
                "}";
            // Read in public key to internal ephemeral key string to check details (extra check, not necessary)
            string publicKey = Rsa.ReadPublicKey(jwsPubKey).ToString();
            Debug.Assert(publicKey.Length > 0);
            Console.WriteLine("Display public key characteristics (should be the same as private key above)...");
            Console.WriteLine("Key length={0} bits", Rsa.KeyBits(publicKey));
            Console.WriteLine("Key hash=0x{0,8:X}", Rsa.KeyHashCode(publicKey));
            n = Rsa.KeyMatch(privateKey, publicKey);
            Console.WriteLine("Rsa.KeyMatch() returns {0} (expected 0 => keys match OK)", n);

            // Verify the signature value against original signing input using the JSON RSA public key
            n = Sig.VerifyData(jwsSignature, b, jwsPubKey, SigAlgorithm.Rsa_Sha256);
            Console.WriteLine("Sig.VerifyData() returns {0} (expecting 0 => signature is valid)", n);
            Debug.Assert(0 == n, "Signature is invalid");

        }

        // *******************
        // BASE64URL UTILITIES
        // *******************

        /// <summary>
        /// Encode string in base64url encoding.
        /// </summary>
        /// <param name="s">String to be encoded</param>
        /// <returns>Base64url-encoded string</returns>
        static string cnvBase64urlFromString(string s)
        {
            s = Cnv.ToBase64(s);
            s = s.Replace("+", "-");
            s = s.Replace("/", "_");
            s = s.Replace("=", "");
            return s;
        }
    }
}