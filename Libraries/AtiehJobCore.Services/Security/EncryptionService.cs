using AtiehJobCore.Core.Domain.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AtiehJobCore.Services.Security
{
    public class EncryptionService : IEncryptionService
    {
        private readonly SecuritySettings _securitySettings;

        public EncryptionService(SecuritySettings securitySettings)
        {
            _securitySettings = securitySettings;
        }

        /// <inheritdoc />
        /// <summary>
        /// Create salt key
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        public virtual string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = RandomNumberGenerator.Create();

            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a password hash
        /// </summary>
        /// <param name="password">{password</param>
        /// <param name="saltKey">Salk key</param>
        /// <param name="passwordFormat"></param>
        /// <returns>Password hash</returns>
        public virtual string CreatePasswordHash(string password, string saltKey,
            string passwordFormat = "SHA1")
        {
            if (string.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";

            var saltAndPassword = string.Concat(password, saltKey);

            var algorithm = default(HashAlgorithm);
            switch (passwordFormat)
            {
                case "SHA1":
                    algorithm = SHA1.Create();
                    break;
                case "SHA256":
                    algorithm = SHA256.Create();
                    break;
                case "SHA384":
                    algorithm = SHA384.Create();
                    break;
                case "SHA512":
                    algorithm = SHA512.Create();
                    break;
                default:
                    throw new NotSupportedException("Not supported hash");
            }

            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <inheritdoc />
        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (string.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _securitySettings.EncryptionKey;

            var tDesSalg = TripleDES.Create();

            var w = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 24));

            tDesSalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 24));
            tDesSalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(16, 8));

            var encryptedBinary = EncryptTextToMemory(plainText, tDesSalg.Key, tDesSalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        /// <inheritdoc />
        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (string.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _securitySettings.EncryptionKey;

            var tDesSalg = TripleDES.Create();
            tDesSalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 24));
            tDesSalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(16, 8));

            var buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDesSalg.Key, tDesSalg.IV);
        }

        #region Utilities

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, TripleDES.Create().CreateEncryptor(key, iv),
                    CryptoStreamMode.Write))
                {
                    var toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, TripleDES.Create().CreateDecryptor(key, iv),
                    CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion
    }
}
