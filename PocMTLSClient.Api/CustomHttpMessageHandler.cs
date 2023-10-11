using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
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
        public CustomHttpMessageHandler()
        {
            //code for local development
            var certificate = new X509Certificate2(Path.Combine("C:/git/poctest.pfx"), "1234");
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
