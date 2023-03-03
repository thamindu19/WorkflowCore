using System.Threading;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WC02JSON;

public class MyStep : StepBodyAsync
{
    public string MyInput { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        await Task.Delay(1000);
        return ExecutionResult.Next();
    }
}
