using AutoMapper;
using Sfu.Shop.Domain.Entities;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.UseCases.Common.Dtos.Feedback;
using Sfu.Shop.UseCases.Common.Dtos.Products;
using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Products;

/// <summary>
/// Product profile.
/// </summary>
public class ProductsProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(productDto => productDto.FeedbackScore,
                option => option.MapFrom(product => product.Feedback.Average(f => f.Estimation)))
            .ForMember(productDto => productDto.AmountFeedbacks,
                option => option.MapFrom(product => product.Feedback.Count()));
        
        CreateMap<Domain.Entities.Feedback, FeedbackDto>();
        CreateMap<User, UserDto>();
    }
}