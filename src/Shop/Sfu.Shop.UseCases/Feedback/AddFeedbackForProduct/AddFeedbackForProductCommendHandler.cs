using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Feedback.AddFeedbackForProduct;


/// <summary>
/// Handler for <see cref="AddFeedbackForProductCommand"/>
/// </summary>
internal class AddFeedbackForProductCommendHandler : AsyncRequestHandler<AddFeedbackForProductCommand>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AddFeedbackForProductCommendHandler(AppDbContext dbContext, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    protected override async Task Handle(AddFeedbackForProductCommand request, CancellationToken cancellationToken)
    {
        if (!loggedUserAccessor.IsAuthenticated())
        {
            throw new ForbiddenException("You are not authorized");
        }

        var feedback = new Domain.Entities.Feedback()
        {
            FeedbackUserId = loggedUserAccessor.GetCurrentUserId(),
            Text = request.Text,
            Estimation = request.Estimation,
            ProductId = request.ProductId,
            CreatedAt = DateTime.UtcNow
        };

        await dbContext.Feedbacks.AddAsync(feedback, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}