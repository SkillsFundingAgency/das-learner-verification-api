using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;



namespace SFA.DAS.LearnerVerification.Services.Factories
{
    public class LoggingBehavior : IEndpointBehavior
    {
        public LoggingMessageInspector MessageInspector { get; }
        public LoggingBehavior(LoggingMessageInspector messageInspector)
        {
            MessageInspector = messageInspector;
        }
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(MessageInspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
