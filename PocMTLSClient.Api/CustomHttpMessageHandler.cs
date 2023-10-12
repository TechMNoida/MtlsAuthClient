using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PocMTLSClient.Api
{
    public class CustomHttpMessageHandler: HttpClientHandler
    {
        private readonly IConfiguration _configuration;

        public CustomHttpMessageHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            //code for local development
            string certPath = _configuration.GetSection("CertificatePath").Value.ToString();
            string certPwd = _configuration.GetSection("CertPwd").Value.ToString();
            var certificate = new X509Certificate2(certPath, certPwd);
            ClientCertificates.Add(certificate);

            //var keyvaultUrl = $"https://kv-gg-dev-poc.vault.azure.net/";
            //var certificateName = "poctest";
            //var azureServiceTokenProvider = new AzureServiceTokenProvider();
            //var keyvaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            //var secret = Task.Run(async () => await keyvaultClient.GetSecretAsync(keyvaultUrl, certificateName)).Result;

            //var privateKeyBytes = Convert.FromBase64String(secret.Value);
            //var certificate = new X509Certificate2(privateKeyBytes, string.Empty);
            //ClientCertificates.Add(certificate);
        }
    }
}
