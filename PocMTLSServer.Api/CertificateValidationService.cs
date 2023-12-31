﻿using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PocMTLSServer.Api
{
    public class CertificateValidationService : ICertificateValidationService
    {
        private readonly IConfiguration _configuration;

        public CertificateValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            //string certPath = _configuration.GetSection("CertificatePath").Value.ToString();
            //string certPwd = _configuration.GetSection("CertPwd").Value.ToString();
            //var certificate = new X509Certificate2(Path.Combine(certPath), certPwd);

            var clientCertificatesList = _configuration.GetSection("ClientCertificates").Get<string[]>() ?? throw new Exception("Invalid configuration");

            return clientCertificatesList.Contains(clientCertificate.Thumbprint);
        }
    }
}
