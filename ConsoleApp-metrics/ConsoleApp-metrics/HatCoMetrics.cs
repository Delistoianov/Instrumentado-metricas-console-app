using System.Diagnostics.Metrics;

namespace ConsoleApp_metrics
{
    public class HatCoMetrics
    {
        private readonly Counter<int> _hatsSold;

        public HatCoMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create("HatCo.Store");
            _hatsSold = meter.CreateCounter<int>("hatco.store.hats_sold");
        }

        public void HatsSold(int quantity)
        {
            _hatsSold.Add(quantity);
        }
    }
}
