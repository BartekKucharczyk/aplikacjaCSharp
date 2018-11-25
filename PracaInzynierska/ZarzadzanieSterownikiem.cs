using System;
using System.Linq;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace PracaInzynierska
{

    class ZarzadzanieSterownikiem
    {

        string plcTime = "";

        public async void StartStopTime(UaTcpSessionChannel sessionChannel,bool vlv)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::Controller:_Controller.Input.GetTime")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = vlv;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public async void ReadTimePLC(UaTcpSessionChannel session)
        {
            var nodeIds = new[] { NodeId.Parse("ns=6;s=::Controller:_Controller.Output.Time") };

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
            plcTime = readResponse.Results[0].Value.ToString();
        }


        public string getTime()
        {
            return plcTime;
        }
    }
}