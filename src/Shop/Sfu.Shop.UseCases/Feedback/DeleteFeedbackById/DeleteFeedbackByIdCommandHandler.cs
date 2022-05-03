using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Feedback.DeleteFeedbackById;

/// <summary>
/// Handler for <see cref="DeleteFeedbackByIdCommand"/>.
/// </summary>
public class DeleteFeedbackByIdCommandHandler : AsyncRequestHandler<DeleteFeedbackByIdCommand>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteFeedbackByIdCommandHandler(AppDbContext dbContext, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    
    /// <inheritdoc />
    protected override async Task Handle(DeleteFeedbackByIdCommand request, CancellationToken cancellationToken)
    {
        if (!loggedUserAccessor.IsAuthenticated())
        {
            throw new ForbiddenException("You are not authorized");
        }

        var feedback = await dbContext.Feedbacks
            .GetAsync(feedback => feedback.Id == request.FeedbackId, cancellationToken);

        if (loggedUserAccessor.GetCurrentUserId() != feedback.FeedbackUserId)
        {
            throw new ForbiddenException("You are can't remove this feedback");
        }

        dbContext.Feedbacks.Remove(feedback);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
