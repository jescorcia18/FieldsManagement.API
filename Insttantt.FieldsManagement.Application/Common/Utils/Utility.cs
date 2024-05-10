using Insttantt.FieldsManagement.Application.Common.Interfaces.Utils;
using Insttantt.FieldsManagement.Domain.Entities;
using Insttantt.FieldsManagement.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.FieldsManagement.Application.Common.Utils
{
    public class Utility : IUtility
    {
        #region Global variables
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public Utility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region public Methods
        public async Task<string> Decrypt(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            var keyDecode = Convert.FromBase64String(key);
            key = Encoding.UTF8.GetString(keyDecode);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            var decrypt = await streamReader.ReadToEndAsync();

                            var decryptConn = Convert.FromBase64String(decrypt);
                            return Encoding.UTF8.GetString(decryptConn);
                        }
                    }
                }
            }
        }

        public async Task<string> Encrypt(string key, string data)
        {
            byte[] iv = new byte[16];
            byte[] array;
            if (string.IsNullOrEmpty(key)) throw new Exception("EncryptKey is invalid");
            var keyDecode = Convert.FromBase64String(key);
            var _key = Encoding.UTF8.GetString(keyDecode);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return await Task.FromResult(Convert.ToBase64String(array));
        }

        public async Task<IEnumerable<FieldResponse>> MapToFieldResponse(IEnumerable<Field> fields)
        {
            return await Task.FromResult(fields.Select( f => new FieldResponse
            {
                FieldId = f.FieldId,
                FieldName = f.FieldName,
                FieldType = f.FieldType,
                FieldRequired = f.FieldRequired,
                FieldValidation = f.FieldValidation
            }));
        }

        public async Task<FieldResponse> MapToFieldResponse(Field fields)
        {
            return await Task.FromResult(new FieldResponse
            {
                FieldId = fields.FieldId,
                FieldName = fields.FieldName,
                FieldType = fields.FieldType,
                FieldRequired = fields.FieldRequired,
                FieldValidation = fields.FieldValidation
            });
        }
        #endregion
    }
}
