using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoffeeFix.Console.Tests
{
    [TestClass]
    public class TelemetryProcessorTests
    {
        [TestMethod]
        public void ProcessRemoval_NoMatch()
        {
            var telemetry = new EventTelemetry("This does not Match");

            var next = new NextProcessor();
            var sut = new MyTelemetryProcessor(next);

            sut.Process(telemetry);

            Assert.IsTrue(next.WasTriggered, "The next processor should be triggered!");
        }

        [TestMethod]
        public void ProcessRemoval_Matched()
        {            
            var telemetry = new EventTelemetry("CoffeeFix.Console has started!");

            var next = new NextProcessor();
            var sut = new MyTelemetryProcessor(next);
            
            sut.Process(telemetry);

            Assert.IsFalse(next.WasTriggered, "The next processor should not be triggered!");
        }
    }

    public class NextProcessor : ITelemetryProcessor
    {
        public bool WasTriggered { get; set; }

        public void Process(ITelemetry item)
        {
            WasTriggered = true;
        }
    }

    public class StringTelementryItem : ITelemetry
    {
        public DateTimeOffset Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TelemetryContext Context => throw new NotImplementedException();

        public IExtension Extension { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sequence { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ITelemetry DeepClone()
        {
            throw new NotImplementedException();
        }

        public void Sanitize()
        {
            throw new NotImplementedException();
        }

        public void SerializeData(ISerializationWriter serializationWriter)
        {
            throw new NotImplementedException();
        }
    }
}
