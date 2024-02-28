﻿using System.Security.Cryptography.X509Certificates;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    public interface ICertificateProvider
    {
        X509Certificate2 GetClientCertificate();

    }
}