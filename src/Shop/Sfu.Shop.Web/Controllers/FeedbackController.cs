using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfu.Shop.UseCases.Feedback.DeleteFeedbackById;
using Sfu.Shop.UseCases.Feedback.UpdateFeedback;

namespace Sfu.Shop.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public FeedbackController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    [HttpDelete("{feedbackId}")]
    public async Task Delete(Guid feedbackId, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteFeedbackByIdCommand(feedbackId), cancellationToken);

    [HttpPut("{feedbackId}")]
    public async Task Update(UpdateFeedbackCommand command, CancellationToken cancellationToken) =>
        await mediator.Send(command, cancellationToken);
}
