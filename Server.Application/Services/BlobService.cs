using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Server.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly StorageClient _storageClient;
        private readonly IConfiguration _configuration;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration; // Initialize the class-level field
            var keyFilePath = _configuration["Firebase:KeyPath"];

            if (string.IsNullOrEmpty(keyFilePath))
            {
                throw new ArgumentNullException(nameof(keyFilePath), "KeyFilePath is not configured.");
            }

            Console.WriteLine($"KeyFilePath: {keyFilePath}");

            // Check if the key file path exists
            if (!File.Exists(keyFilePath))
            {
                throw new FileNotFoundException("The Firebase Storage key file was not found.", keyFilePath);
            }

            _storageClient = StorageClient.Create(Google.Apis.Auth.OAuth2.GoogleCredential.FromFile(keyFilePath));
        }

    }
}
