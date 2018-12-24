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
            public string Status;
            public string positionAct;
            public string velocityAct;
            // Cyclic set FBs
            public bool ActiveCyc;
            public bool ErrorCyc;
            public bool CyclicSetActive;
            public bool CommandAborted;
            public bool CommandBusy;
            public string StatusIdCyc;

            // Params
            public string velocitySet;
            public string distanceSet;
            public string positionSet;
            public string accelerationSet;
            public string decelerationSet;
            public string positionCycSet;
            public string velocityCycSet;
            public string torqueCycSet;
        };

        public Stany stanOsi = new Stany();
        public bool wczytal = false;

        public async void DefaultCmd(UaTcpSessionChannel sessionChannel, string txt)
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

        public async void MoveVelocityOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveVelocityOff")
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

        public async void MoveAdditiveOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveAdditiveOff")
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

        public async void MoveAbsoluteOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.MoveAbsoluteOff")
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

        public async void UpdateCyclicSet(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.Update")
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
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.VelocityAct"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.CyclicSet.Active"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.CyclicSet.CyclicSetActive"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.CyclicSet.Error"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.CyclicSet.CommandBusy"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.CyclicSet.CommandAborted"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.CyclicSet.StatusID"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.Velocity"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.Distance"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.Position"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.Acceleration"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.Deceleration"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.CycParams.Velocity"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.CycParams.Torque"),
                                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Output.States.SingleAxis.Axis_0.Params.CycParams.Position"),
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
            if (readResponse.Results[12].Value.ToString().Equals("True"))
            {
                stanOsi.ActiveCyc = true;
            }
            else stanOsi.ActiveCyc = false;
            if (readResponse.Results[13].Value.ToString().Equals("True"))
            {
                stanOsi.CyclicSetActive = true;
            }
            else stanOsi.CyclicSetActive = false;
            if (readResponse.Results[14].Value.ToString().Equals("True"))
            {
                stanOsi.ErrorCyc = true;
            }
            else stanOsi.ErrorCyc = false;
            if (readResponse.Results[15].Value.ToString().Equals("True"))
            {
                stanOsi.CommandBusy = true;
            }
            else stanOsi.CommandBusy = false;
            if (readResponse.Results[16].Value.ToString().Equals("True"))
            {
                stanOsi.CommandAborted = true;
            }
            else stanOsi.CommandAborted = false;

            stanOsi.Status = readResponse.Results[9].Value.ToString();
            stanOsi.StatusIdCyc = readResponse.Results[17].Value.ToString();

            stanOsi.positionAct = readResponse.Results[10].Value.ToString();
            stanOsi.velocityAct = readResponse.Results[11].Value.ToString();

            stanOsi.velocitySet = readResponse.Results[18].Value.ToString();
            stanOsi.distanceSet = readResponse.Results[19].Value.ToString();
            stanOsi.positionSet = readResponse.Results[20].Value.ToString();
            stanOsi.accelerationSet = readResponse.Results[21].Value.ToString();
            stanOsi.decelerationSet = readResponse.Results[22].Value.ToString();
            stanOsi.velocityCycSet = readResponse.Results[23].Value.ToString();
            stanOsi.torqueCycSet = readResponse.Results[24].Value.ToString();
            stanOsi.positionCycSet = readResponse.Results[25].Value.ToString();
            wczytal = true;
        }


        public async void CyclicPositionSetOn(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.CyclicPostionOn")
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

        public async void CyclicPositionSetOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.CyclicPostionOff")
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

        public async void CyclicTorqueSetOn(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.CyclicTorqueOn")
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

        public async void CyclicTorqueSetOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.CyclicTorqueOff")
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

        public async void CyclicVelocitySetOn(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.CyclicVelocityOn")
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

        public async void CyclicVelocitySetOff(UaTcpSessionChannel sessionChannel)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse("ns=6;s=::AxisCtrl:_AxisCtrl.Input.Commands.SingleAxis.A1.TorqControl.CyclicVelocityOff")
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



        // Send parameter
        public async void SendInput(UaTcpSessionChannel sessionChannel, float param, string name)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse(name)
               };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = param;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public async void SendInput(UaTcpSessionChannel sessionChannel, double param, string name)
        {

            var nodeIds = new[]
            {
                  NodeId.Parse(name)
               };

            RegisterNodesResponse registerNodesResponse = null;

            var registerNodesRequest = new RegisterNodesRequest
            {
                NodesToRegister = nodeIds
            };
            registerNodesResponse = await sessionChannel.RegisterNodesAsync(registerNodesRequest);

            Object rue = param;

            DataValue dataValues = new DataValue(rue);

            var writeRequest = new WriteRequest
            {
                NodesToWrite = (registerNodesResponse?.RegisteredNodeIds ?? nodeIds)
              .Select(n => new WriteValue { NodeId = n, AttributeId = AttributeIds.Value, Value = dataValues }).ToArray()
            };

            await sessionChannel.WriteAsync(writeRequest);
        }

        public void SendParams(UaTcpSessionChannel sessionChannel, string vel, string dis, string pos, string acc, string decc)
        {
            SendInput(sessionChannel, float.Parse(vel), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.Velocity");
            SendInput(sessionChannel, float.Parse(dis), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.Distance");
            SendInput(sessionChannel, float.Parse(pos), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.Position");
            SendInput(sessionChannel, float.Parse(acc), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.Acceleration");
            SendInput(sessionChannel, float.Parse(decc), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.Deceleration");
            Update(sessionChannel);
        }

        public void SendCycParams(UaTcpSessionChannel sessionChannel, string vel, string torq, string pos)
        {
            SendInput(sessionChannel, double.Parse(pos), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.CyclicSetParameter.Position");
            SendInput(sessionChannel, double.Parse(torq), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.CyclicSetParameter.Torque");
            SendInput(sessionChannel, double.Parse(vel), "ns=6;s=::AxisCtrl:_AxisCtrl.Input.Parameters.CyclicSetParameter.Velocity");
            UpdateCyclicSet(sessionChannel);
        }


    }
}