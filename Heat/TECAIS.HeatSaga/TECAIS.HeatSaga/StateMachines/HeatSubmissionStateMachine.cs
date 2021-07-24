using Automatonymous;
using Automatonymous.Binders;
using MassTransit;
using ModelContracts;
using SagaContracts;
using Serilog;
using System;
using System.Threading.Tasks;

namespace TECAIS.HeatSaga.StateMachines
{
    public class HeatSubmissionStateMachine : MassTransitStateMachine<SubmissionState>
    {
        public HeatSubmissionStateMachine()
        {
            //persisting the state -> create a new instance if it does not exsist
            Event(() => OnHeatSubmitted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnHeatPriced, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnHeatCharged, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnHeatAccounted, x => x.CorrelateById(m => m.Message.Id));
            Event(() => OnHeatFaulted, x => x.CorrelateById(m => m.Message.Message.Id));

            InstanceState(x => x.CurrentState);

            //first state - final state (last state) When onOrderSubmitted go to state Submitted
            Initially(
                When(OnHeatSubmitted)
                .TransitionTo(Submitted),
                When(OnHeatPriced)
                .TransitionTo(Priced),
                When(OnHeatCharged)
                .TransitionTo(Charged),
                When(OnHeatAccounted)
                .TransitionTo(Accounted));

            During(Submitted,
                When(OnHeatSubmitted)
                .Then(c => c.Instance.CustomerAddress = c.Data.CustomerAddress)
                .Then(c => c.Instance.TimeOfMeasurement = c.Data.TimeOfMeasurement)
                .Then(c => c.Instance.HeatConsumption = c.Data.HeatConsumption)
                .Then(c => c.Instance.CorrelationId = c.Data.Id)
                .Then(c => callheatpricecommand(c))
                .TransitionTo(Priced)
                );

            During(Priced,
                When(OnHeatPriced).
                Then(c => c.Instance.Price = c.Data.Price).
                Then(c => callheatChargingComnmand(c)).
                TransitionTo(Charged));

            During(Charged, 
                When(OnHeatCharged)
                .Then(c => c.Instance.PublicCharging = c.Data.PublicCharging)
                .Then(c => callheatAccoutingComnmand(c))
                .TransitionTo(Accounted));

            During(Accounted,
                When(OnHeatAccounted)
                .Then(c => callheatCompletedCommand(c))
                //.TransitionTo(Accounted)
                //.Then(c => callheatCompletedCommand(c))
                .Finalize());

            During(Accounted,
                Ignore(OnHeatSubmitted),
                Ignore(OnHeatCharged));

            During(Priced,
                Ignore(OnHeatSubmitted));

            During(Charged,
                Ignore(OnHeatSubmitted),
                Ignore(OnHeatPriced));
        }

        private void callheatCompletedCommand(BehaviorContext<SubmissionState, HeatSubmissionAccounted> c)
        {
            Console.WriteLine(c.Instance.CurrentState);
        }

        private void callheatAccoutingComnmand(BehaviorContext<SubmissionState, HeatSubmissionCharged> c)
        {
            var context = c.CreateConsumeContext();
            context.Publish<AccountingCommand>(new
            {
                Address = c.Instance.CustomerAddress,
                TimeOfMeasurement = c.Instance.TimeOfMeasurement,
                Consumption = c.Instance.HeatConsumption,
                Price = c.Instance.Price,
                TaxPrice = c.Instance.PublicCharging
            });

            Console.WriteLine("callheatAccoutingComnmand" + c.Data.Id);
            Console.WriteLine(c.Instance.CurrentState);
        }

        private void callheatChargingComnmand(BehaviorContext<SubmissionState, HeatSubmissionPriced> c)
        {
            var context = c.CreateConsumeContext();
            context.Publish<PublicChargingCommand>(new
            {
                HeatConsumption = c.Data.Price
            });
            Console.WriteLine("callheatChargingComnmand" + c.Data.Id);
            Console.WriteLine(c.Instance.CurrentState);
        }

        private void callheatpricecommand(BehaviorContext<SubmissionState, HeatSubmissionSubmitted> c)
        {
            var context = c.CreateConsumeContext();
            context.Publish<HeatPriceCommand>(new
            {
                HeatConsumption = c.Data.HeatConsumption
            });
            Console.WriteLine("callheatPriceComnmand" + c.Data.Id);
            Console.WriteLine(c.Instance.CurrentState);
        }

        public State Submitted { get; private set; }
        public State Priced { get; private set; }
        public State Charged { get; private set; }
        public State Accounted { get; private set; }
        public State Faulted { get; private set; }

        //define that we can handle det different events for the message
        public Event<HeatSubmissionSubmitted> OnHeatSubmitted { get; private set; }
        public Event<HeatSubmissionPriced> OnHeatPriced { get; private set; }
        public Event<HeatSubmissionCharged> OnHeatCharged{ get; private set; }
        public Event<HeatSubmissionAccounted> OnHeatAccounted { get; private set; }
        public Event<Fault<HeatSubmissionFaulted>> OnHeatFaulted { get; private set; }
    }
}
