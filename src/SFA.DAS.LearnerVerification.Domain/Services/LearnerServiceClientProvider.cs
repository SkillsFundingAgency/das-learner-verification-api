﻿using System.ServiceModel;
using SFA.DAS.LearnerVerification.Domain.Factories;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class LearnerServiceClientProvider<T> : ILearnerServiceClientProvider<T>
    {
        private readonly IClientTypeFactory<T> _factory;

        public LearnerServiceClientProvider(IClientTypeFactory<T> factory)
        {
            _factory = factory;
        }

        public T GetServiceAsync()
        {
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.UseDefaultWebProxy = true;

                var service = _factory.Create(binding);

                return service;
            }
            catch (Exception ex)
            {
                //TODO: Implement logging?
                throw;
            }
        }

        //TODO: do we need to close client in Dispose or similar?
    }
}