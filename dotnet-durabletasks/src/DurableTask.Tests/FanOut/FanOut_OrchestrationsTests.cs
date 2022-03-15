using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DurableTask.Tests
{
    [TestClass]
    public class FanOut_OrchestrationsTests
    {
        private readonly ILogger<FanOut_OrchestrationsTests> logItem;

        public FanOut_OrchestrationsTests()
        {
            logItem = LoggerFactory.CreateLogger<FanOut_OrchestrationsTests>();
        }

        [TestMethod]
        public void FanOut_OrchestrationTest()
        {
            try
            {
                // Test Orchestration
            }
            catch (Exception ex)
            {
                logItem.LogError(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}

