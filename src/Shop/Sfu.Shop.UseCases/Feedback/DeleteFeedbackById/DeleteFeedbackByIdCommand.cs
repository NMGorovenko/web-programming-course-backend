using MediatR;

namespace Sfu.Shop.UseCases.Feedback.DeleteFeedbackById;

/// <summary>
/// Delete feedback by Id command.
/// </summary>
/// <param name="FeedbackId">Feedback Id.</param>
public record DeleteFeedbackByIdCommand(Guid FeedbackId) : IRequest;
