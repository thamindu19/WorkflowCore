using Microsoft.AspNetCore.Mvc;
using WorkflowCore.Interface;

[ApiController]
[Route("[controller]")]
public class WorkflowController : ControllerBase
{
    private readonly IWorkflowHost _workflowHost;

    public WorkflowController(IWorkflowHost workflowHost)
    {
        _workflowHost = workflowHost;
    }

    [HttpPost("{workflowId}")]
    public async Task<IActionResult> Post(string workflowId)
    {
        var workflowInstanceId = await _workflowHost.StartWorkflow(workflowId);
        return Ok(workflowInstanceId);
    }
}