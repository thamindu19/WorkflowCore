﻿using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WC01HelloWorld.Steps {
    public class GoodbyeWorld : StepBody
    {
        private ILogger _logger;

        public GoodbyeWorld(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GoodbyeWorld>();
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            _logger.LogInformation("Hi there!");
            return ExecutionResult.Next();
        }
    }
}
