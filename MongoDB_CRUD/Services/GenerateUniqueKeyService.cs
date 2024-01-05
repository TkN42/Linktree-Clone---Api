using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class GenerateUniqueKeyService : IGenerateUniqueKeyService
    {
        public string GenerateUniqueKey()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int keyLength = 8;
            char[] key = new char[keyLength];
            Random random = new Random();

            for (int i = 0; i < keyLength; i++)
            {
                key[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(key);
        }

    }
}
