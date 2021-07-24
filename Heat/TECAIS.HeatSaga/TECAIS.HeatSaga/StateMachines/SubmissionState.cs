using Automatonymous;
using MassTransit.Saga;
using System;

namespace TECAIS.HeatSaga.StateMachines
{
    public class SubmissionState : SagaStateMachineInstance
    {
        public Guid CorrelationId  { get; set; }
        public string CurrentState { get; set; }
        //needed for redis
        public string CustomerAddress { get; set; }
        public double Price { get; set; }
        public double HeatConsumption { get; set; }
        public double PublicCharging { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
    }
}
