using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace SFA.DAS.LearnerVerification.Services
{
    public class CustomMessageInspector : IClientMessageInspector
    {
        private readonly X509Certificate2 _certificate;

        public CustomMessageInspector(X509Certificate2 certificate)
        {
            _certificate = certificate;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            // Not needed for this scenario
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var httpRequestMessageProperty = new HttpRequestMessageProperty();

            var certBase64 = Convert.ToBase64String(_certificate.Export(X509ContentType.Cert));
            var certHeaderValue = $"X.509 subject=\"{_certificate.SubjectName.Name}\",value=\"{certBase64}\"";
            httpRequestMessageProperty.Headers.Add("X-ARR-ClientCert", certHeaderValue);

            request.Properties[HttpRequestMessageProperty.Name] = httpRequestMessageProperty;

            return null;
        }
    }

    public class CustomEndpointBehavior : IEndpointBehavior
    {
        private readonly X509Certificate2 _certificate;

        public CustomEndpointBehavior(X509Certificate2 certificate)
        {
            _certificate = certificate;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // Not needed for this scenario
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            var inspector = new CustomMessageInspector(_certificate);
            clientRuntime.ClientMessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // Not needed for this scenario
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // Not needed for this scenario
        }
    }
}