using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Tools.Criptografia
{
    public static class Encriptador
    {
        //private static SymmetricAlgorithm _cryptoService = new AesManaged();
        private static byte[] IV = new byte[] { 152, 245, 38, 153, 148, 160, 4, 207, 83, 6, 73, 193, 229, 186, 149, 251 };
        private static byte[] Key = new byte[] { 36, 208, 181, 116, 12, 2, 34, 226, 94, 248, 126, 229, 137, 171, 46, 205, 70,
                                                 46, 1, 195, 230, 236, 36, 126, 30, 156, 83, 204, 201, 114, 23, 201 };

        //https://damienbod.com/2020/08/19/symmetric-and-asymmetric-encryption-in-net-core/
        private static Aes CreateCipher()
        {
            // Default values: Keysize 256, Padding PKC27
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC

            cipher.Padding = PaddingMode.PKCS7;
            cipher.Key = Key;
            cipher.IV = IV;

            return cipher;
        }

        public static string Criptografar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return null;

            var cipher = CreateCipher();

            ICryptoTransform cryptTransform = cipher.CreateEncryptor();
            byte[] plaintext = Encoding.UTF8.GetBytes(texto);
            byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

            return Convert.ToBase64String(cipherText);
        }

        public static string Decriptografar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            Aes cipher = CreateCipher();

            ICryptoTransform cryptTransform = cipher.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(texto);
            byte[] plainBytes = cryptTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }

        //public static string Criptografar(string texto)
        //{
        //    if (string.IsNullOrEmpty(texto))
        //        return null;

        //    return Transformar(texto, _cryptoService.CreateEncryptor(Key, IV), Encoding.UTF8);
        //}

        //public static string Decriptografar(string texto)
        //{
        //    if (string.IsNullOrEmpty(texto))
        //        return texto;
        //    var encoder = CodePagesEncodingProvider.Instance.GetEncoding(1252);

        //    return Transformar(texto, _cryptoService.CreateDecryptor(Key, IV), encoder);
        //}

        //private static string Transformar(string texto, ICryptoTransform cryptoTransform, Encoding encoder)
        //{
        //    try
        //    {
        //        MemoryStream stream = new MemoryStream();
        //        CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write);

        //        byte[] input = encoder.GetBytes(texto);

        //        cryptoStream.Write(input, 0, input.Length);

        //        if (input.Length > 0)
        //        {
        //            cryptoStream.FlushFinalBlock();
        //        }

        //        return Encoding.UTF8.GetString(stream.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}