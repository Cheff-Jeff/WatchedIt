﻿using System.Security.Cryptography;

namespace WatchedItApi.Models
{
    public class EncryptionHandler
    {
        public string hash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);
            string hashed = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return hashed;
        }

        public byte[] AddSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public bool VerifyPassword(string password, string sSalt, string userPass)
        {
            byte[] salt = Convert.FromBase64String(sSalt);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(userPass, salt, 10000);

            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == password;
        }
    }
}
