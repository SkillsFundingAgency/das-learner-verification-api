using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace SFA.DAS.LearnerVerification.Services.Factories
{
    public class LoggingMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        private ILogger<LoggingMessageInspector> _logger { get; set; }

        public LoggingMessageInspector(ILogger<LoggingMessageInspector> logger)
        {
            _logger = logger;
        }
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            using (var buffer = reply.CreateBufferedCopy(int.MaxValue))
            {
                var document = GetDocument(buffer.CreateMessage());
                //Logger.LogTrace(document.OuterXml);
                _logger.LogError($"Received response:\n {document.OuterXml}");

                reply = buffer.CreateMessage();
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            using (var buffer = request.CreateBufferedCopy(int.MaxValue))
            {
                var document = GetDocument(buffer.CreateMessage());
                _logger.LogError($"Sending request:\n {document.OuterXml}");

                request = buffer.CreateMessage();
                return null;
            }
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            using (var buffer = request.CreateBufferedCopy(int.MaxValue))
            {
                var document = GetDocument(buffer.CreateMessage());
                _logger.LogError($"Received request:\n {document.OuterXml}");

                request = buffer.CreateMessage();
                return null;
            }
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            using (var buffer = reply.CreateBufferedCopy(int.MaxValue))
            {
                var document = GetDocument(buffer.CreateMessage());
                //Logger.LogTrace(document.OuterXml);
                _logger.LogError($"Sending response:\n {document.OuterXml}");

                reply = buffer.CreateMessage();
            }
            
        }

        private XmlDocument GetDocument(Message request)
        {
            XmlDocument document = new XmlDocument();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // write request to memory stream
                XmlWriter writer = XmlWriter.Create(memoryStream);
                request.WriteMessage(writer);
                writer.Flush();
                memoryStream.Position = 0;

                // load memory stream into a document
                document.Load(memoryStream);
            }

            return document;
        }
    }
}
