using Automatonymous;
using Automatonymous.Binders;
using MassTransit;
using ModelContracts;
using SagaContracts;
using Serilog;
using System;
using System.Threading.Tasks;

namespace TECAIS.ElectricitySaga.StateMachines
{
    public class ElectricitySubmissionStateMachine : MassTransitStateMachine<SubmissionState>
    {
        public ElectricitySubmissionStateMachine()
        {
            //persisting the state -> create a new instance if it does not exsist
            Event(() => OnElectricitySubmitted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnElectricityPriced, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnElectricityCharged, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnElectricityAccounted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnElectricityFaulted, x => x.CorrelateById(m => m.Message.Message.Id));

            InstanceState(x => x.CurrentState);

            //first state - final state (last state) When onOrderSubmitted go to state Submitted
            Initially(
                When(OnElectricitySubmitted)
                .Then(c => c.Instance.CustomerAddress = c.Data.CustomerAddress)
                .Then(c => c.Instance.TimeOfMeasurement = c.Data.TimeOfMeasurement)
                .Then(c => c.Instance.ElectricityConsumption = c.Data.ElectricityConsumption)
                .Then(c => callelectricitypricecommand(c))
                .TransitionTo(Submitted));

            During(Submitted,
                When(OnElectricityPriced).
                Then(c => c.Instance.Price = c.Data.Price).
                Then(c => callelectricityChargingComnmand(c)).
                TransitionTo(Priced));

            During(Priced,
                When(OnElectricityCharged)
                .Then(c => c.Instance.PublicCharging = c.Data.PublicCharging)
                .Then(c => callelectricityAccoutingComnmand(c))
                .TransitionTo(Charged));

            During(Charged,
                When(OnElectricityAccounted)
                .Then(c => callelectricityCompletedCommand(c))
                .TransitionTo(Accounted)
                .Then(c => callelectricityCompletedCommand(c))
                .Finalize());
        }

        private void callelectricityCompletedCommand(BehaviorContext<SubmissionState, ElectricitySubmissionAccounted> c)
        {
            Console.WriteLine(c.Instance.CurrentState);
        }

        private void callelectricityAccoutingComnmand(BehaviorContext<SubmissionState, ElectricitySubmissionCharged> c)
        {
            var context = c.CreateConsumeContext();
            context.Publish<AccountingCommand>(new
            {
                Address = c.Instance.CustomerAddress,
                TimeOfMeasurement = c.Instance.TimeOfMeasurement,
                Consumption = c.Instance.ElectricityConsumption,
                Price = c.Instance.Price,
                TaxPrice = c.Instance.PublicCharging
            });

            Console.WriteLine("callelectricityAccoutingComnmand" + c.Data.Id);
            Console.WriteLine(c.Instance.CurrentState);
        }

        private void callelectricityChargingComnmand(BehaviorContext<SubmissionState, ElectricitySubmissionPriced> c)
        {
            var context = c.CreateConsumeContext();
            context.Publish<PublicChargingCommand>(new
            {
                ElectricityConsumption = c.Data.Price
            });
            Console.WriteLine("callelectricityAccoutingComnmand" + c.Data.Id);
            Console.WriteLine(c.Instance.CurrentState);
        }

        private void callelectricitypricecommand(BehaviorContext<SubmissionState, ElectricitySubmissionSubmitted> c)
        {
            var context = c.CreateConsumeContext();
            context.Publish<ElectricityPriceCommand>(new
            {
                ElectricityConsumption = c.Data.ElectricityConsumption
            });
            Console.WriteLine("callelectricityAccoutingComnmand" + c.Data.Id);
            Console.WriteLine(c.Instance.CurrentState);
        }

        public State Submitted { get; private set; }
        public State Priced { get; private set; }
        public State Charged { get; private set; }
        public State Accounted { get; private set; }
        public State Faulted { get; private set; }

        //define that we can handle det different events for the message
        public Event<ElectricitySubmissionSubmitted> OnElectricitySubmitted { get; private set; }
        public Event<ElectricitySubmissionPriced> OnElectricityPriced { get; private set; }
        public Event<ElectricitySubmissionCharged> OnElectricityCharged { get; private set; }
        public Event<ElectricitySubmissionAccounted> OnElectricityAccounted { get; private set; }
        public Event<Fault<ElectricitySubmissionFaulted>> OnElectricityFaulted { get; private set; }
    }
}
