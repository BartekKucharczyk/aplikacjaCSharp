using System;
using System.Linq;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace PracaInzynierska
{
    class ZarzadzanieOsia
    {
      public struct Stany
        {
            public bool Active;
            public bool PowerSt;
            public bool Home;
            public bool Error;
            public bool MoveActive;
            public bool InPosition;
            public bool InVelocity;
            public bool UpdateDone;
            public bool Stopped;
            public int Status;
            public double positionAct;
            public double velocityAct;
        };

        public Stany stanOsi = new Stany();
        public bool wczytal = false;
       
        public async void DefaultCmd(UaTcpSessionChannel sessionChannel,string txt)
        {
            var nodeIds = new[]
            {
               NodeId.Parse(txt)
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

        public async void PowerOn(UaTcpSessionChannel sessionChannel)
        {

               var nodeIds = new[]
               {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.PowerOn")
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

        public async void PowerOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.PowerOff")
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

        public async void Home(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.Home")
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

        public async void MoveVelocity(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveVelocity")
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

        public async void MoveAdditive(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveAdditive")
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

        public async void MoveAbsolute(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveAbsolute")
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

        public async void MoveTorque(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveTorque")
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

        public async void StopOn(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.StopOn")
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

        public async void StopOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.StopOff")
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

        public async void ErrorReset(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.ErrorReset")
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

        public async void Update(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.Update")
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

        public async void JogPositiveOn(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.JogPositive")
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

        public async void JogPositiveOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.JogPositive")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = false;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public async void JogNegativeOn(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.JogNegative")
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

        public async void JogNegativeOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.JogNegative")
            };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = false;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }
    
        public async void ReadStatus(UaTcpSessionChannel session)
        {
            var nodeIds = new[] { NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Active"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.PowerOn"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.IsHomed"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Error"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.MoveActive"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.InPosition"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.InVelocity"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.UpdateDone"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Stopped"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.StatusID"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.PositionAct"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.VelocityAct")
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
           
            if (readResponse.Results[0].Value.ToString().Equals("True"))
            {
                stanOsi.Active = true;
            }
            else stanOsi.Active = false;
            if (readResponse.Results[1].Value.ToString().Equals("True"))
            {
                stanOsi.PowerSt = true;
            }
            else stanOsi.PowerSt = false;
            if (readResponse.Results[2].Value.ToString().Equals("True"))
            {
                stanOsi.Home = true;
            }
            else stanOsi.Home = false;
            if (readResponse.Results[3].Value.ToString().Equals("True"))
            {
                stanOsi.Error = true;
            }
            else stanOsi.Error = false;
            if (readResponse.Results[4].Value.ToString().Equals("True"))
            {
                stanOsi.MoveActive = true;
            }
            else stanOsi.MoveActive = false;
            if (readResponse.Results[5].Value.ToString().Equals("True"))
            {
                stanOsi.InPosition = true;
            }
            else stanOsi.InPosition = false;
            if (readResponse.Results[6].Value.ToString().Equals("True"))
            {
                stanOsi.InVelocity = true;
            }
            else stanOsi.InVelocity = false;
            if (readResponse.Results[7].Value.ToString().Equals("True"))
            {
                stanOsi.UpdateDone = true;
            }
            else stanOsi.UpdateDone = false;
            if (readResponse.Results[8].Value.ToString().Equals("True"))
            {
                stanOsi.Stopped = true;
            }
            else stanOsi.Stopped = false;
            stanOsi.Status = (int) readResponse.Results[9].Value;
            stanOsi.positionAct = (double)readResponse.Results[10].Value;
            stanOsi.velocityAct = (double)readResponse.Results[11].Value;
            wczytal = true;
        }

    }
}