using Automatonymous;
using Automatonymous.Binders;
using MassTransit;
using ModelContracts;
using SagaContracts;
using Serilog;
using System;
using System.Threading.Tasks;

namespace TECAIS.WaterSaga.StateMachines
{
    public class WaterSubmissionStateMachine : MassTransitStateMachine<SubmissionState>
    {
        public WaterSubmissionStateMachine() { 

                //persisting the state -> create a new instance if it does not exsist
                Event(() => OnWaterSubmitted, x => x.CorrelateById(m => m.Message.Id));
                Event(() => OnWaterPriced, x => x.CorrelateById(m => m.Message.Id));
                Event(() => OnWaterCharged, x => x.CorrelateById(m => m.Message.Id));
                Event(() => OnWaterAccounted, x => x.CorrelateById(m => m.Message.Id));
                Event(() => OnWaterFaulted, x => x.CorrelateById(m => m.Message.Message.Id));

                InstanceState(x => x.CurrentState);

                //first state - final state (last state) When onOrderSubmitted go to state Submitted
                Initially(
                    When(OnWaterSubmitted)
                    .Then(c => c.Instance.CustomerAddress = c.Data.CustomerAddress)
                    .Then(c => c.Instance.TimeOfMeasurement = c.Data.TimeOfMeasurement)
                    .Then(c => c.Instance.WaterConsumption = c.Data.WaterConsumption)
                    .Then(c => callwaterpricecommand(c))
                    .TransitionTo(Submitted));

                During(Submitted,
                    When(OnWaterPriced).
                    Then(c => c.Instance.Price = c.Data.Price).
                    Then(c => callwaterChargingComnmand(c)).
                    TransitionTo(Priced));

                During(Priced,
                    When(OnWaterCharged)
                    .Then(c => c.Instance.PublicCharging = c.Data.PublicCharging)
                    .Then(c => callwaterAccoutingComnmand(c))
                    .TransitionTo(Charged));

                During(Charged,
                    When(OnWaterAccounted)
                    .Then(c => callwaterCompletedCommand(c))
                    .TransitionTo(Accounted)
                    .Then(c => callwaterCompletedCommand(c))
                    .Finalize());
            }

            private void callwaterCompletedCommand(BehaviorContext<SubmissionState, WaterSubmissionAccounted> c)
            {
                Console.WriteLine(c.Instance.CurrentState);
            }

            private void callwaterAccoutingComnmand(BehaviorContext<SubmissionState, WaterSubmissionCharged> c)
            {
                var context = c.CreateConsumeContext();
                context.Publish<AccountingCommand>(new
                {
                    Address = c.Instance.CustomerAddress,
                    TimeOfMeasurement = c.Instance.TimeOfMeasurement,
                    Consumption = c.Instance.WaterConsumption,
                    Price = c.Instance.Price,
                    TaxPrice = c.Instance.PublicCharging
                });

                Console.WriteLine("callwaterAccoutingComnmand" + c.Data.Id);
                Console.WriteLine(c.Instance.CurrentState);
            }

            private void callwaterChargingComnmand(BehaviorContext<SubmissionState, WaterSubmissionPriced> c)
            {
                var context = c.CreateConsumeContext();
                context.Publish<PublicChargingCommand>(new
                {
                    WaterConsumption = c.Data.Price
                });
                Console.WriteLine("callwaterAccoutingComnmand" + c.Data.Id);
                Console.WriteLine(c.Instance.CurrentState);
            }

            private void callwaterpricecommand(BehaviorContext<SubmissionState, WaterSubmissionsSubmitted> c)
            {
                var context = c.CreateConsumeContext();
                context.Publish<WaterPriceCommand>(new
                {
                    WaterConsumption = c.Data.WaterConsumption
                });
                Console.WriteLine("callwaterAccoutingComnmand" + c.Data.Id);
                Console.WriteLine(c.Instance.CurrentState);
            }

            public State Submitted { get; private set; }
            public State Priced { get; private set; }
            public State Charged { get; private set; }
            public State Accounted { get; private set; }
            public State Faulted { get; private set; }

            //define that we can handle det different events for the message
            public Event<WaterSubmissionsSubmitted> OnWaterSubmitted { get; private set; }
            public Event<WaterSubmissionPriced> OnWaterPriced { get; private set; }
            public Event<WaterSubmissionCharged> OnWaterCharged { get; private set; }
            public Event<WaterSubmissionAccounted> OnWaterAccounted { get; private set; }
            public Event<Fault<WaterSubmissionFaulted>> OnWaterFaulted { get; private set; }
       

    }
}
