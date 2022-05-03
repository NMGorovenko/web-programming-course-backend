using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Feedback.UpdateFeedback;

/// <summary>
/// Handler for <see cref="UpdateFeedbackCommand"/>.
/// </summary>
public class UpdateFeedbackCommandHandler : AsyncRequestHandler<UpdateFeedbackCommand>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateFeedbackCommandHandler(AppDbContext dbContext, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    
    /// <inheritdoc />
    protected override async Task Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        if (!loggedUserAccessor.IsAuthenticated())
        {
            throw new ForbiddenException("You are not authorized");
        }

        var feedback = await dbContext.Feedbacks
            .AsNoTracking()
            .GetAsync(feedback => feedback.Id == request.FeedbackId, cancellationToken);

        if (loggedUserAccessor.GetCurrentUserId() != feedback.FeedbackUserId)
        {
            throw new ForbiddenException("You are can't remove this feedback");
        }

        feedback = feedback with
        {
            Text = request.Text,
            Estimation = request.Estimation
        };
        
        dbContext.Feedbacks.Update(feedback);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
