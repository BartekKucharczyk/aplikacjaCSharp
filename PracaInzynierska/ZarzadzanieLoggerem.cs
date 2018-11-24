using System;
using System.Linq;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace PracaInzynierska
{
    class ZarzadzanieLoggerem
    {
        public string endStateCmd = "";

        public string[] logList;
        public string[] logDesc;
        public string[] logTime;
        public string[] logSeverity;
        public bool wczytano = false;

        public string nameOfLogbook = "$$arlogsys";

        public async void StartReadLog(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Input.Commands.REL_START")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = true;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public async void Acknowledge(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Input.Commands.ACK")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = true;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public async void ReadEndStateCmd(UaTcpSessionChannel session)
        {
            var nodeIds = new[] { NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Output.States.EndStateCmd")};

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await session.RegisterNodesAsync(registerNodesRequest);

            var readRequest = new ReadRequest
            {
                NodesToRead = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
                .Select(n => new ReadValueId { NodeId = n, AttributeId = AttributeIds.Value }).ToArray()
            };

            var readResponse = await session.ReadAsync(readRequest).ConfigureAwait(false);
            endStateCmd = readResponse.Results[0].Value.ToString();
        }

        public async void ReadLoggerLists(UaTcpSessionChannel session)
        {
            var nodeIds = new[] { NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Output.States.LogList"),
                                  NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Output.States.LogTime"),
                                  NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Output.States.LogDesc"),
                                  NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Output.States.LogSeverity")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await session.RegisterNodesAsync(registerNodesRequest);

            var readRequest = new ReadRequest
            {
                NodesToRead = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
                .Select(n => new ReadValueId { NodeId = n, AttributeId = AttributeIds.Value }).ToArray()
            };

            var readResponse = await session.ReadAsync(readRequest).ConfigureAwait(false);

            logList = readResponse.Results[0].Value.ToString().Split(',');
            logTime = readResponse.Results[1].Value.ToString().Split(',');
            logDesc = readResponse.Results[2].Value.ToString().Split(',');
            logSeverity = readResponse.Results[3].Value.ToString().Split(',');
            wczytano = true;
        }

        public async void SendNameOfLogBook(UaTcpSessionChannel sessionChannel,string nameLogbook)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::LoggerTask:_LoggerHandling.Input.Parameters.NameOfLogbook")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = nameLogbook;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public bool getReadInfo()
        {
            return wczytano;
        }

        public void setReadInfo(bool state)
        {
            wczytano = state;
        }
    }
}