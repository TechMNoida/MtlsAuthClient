using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PocMTLSServer.Api
{
    public interface ICertificateValidationService 
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
}
