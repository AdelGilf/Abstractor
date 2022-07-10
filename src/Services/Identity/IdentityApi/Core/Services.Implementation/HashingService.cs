using Core.Services.Abstraction;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Implementation
{
    public class HashingService : IHashingService
    {
        private IConfiguration _iConfigeration { get; }
        public HashingService(IConfiguration iConfiguration)
        {
            _iConfigeration = iConfiguration;
        }
        public string Hashing(string password)
        {
            Encoding encoding = Encoding.UTF8;
            string solt = _iConfigeration["solt"]!;
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: encoding.GetBytes(solt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
