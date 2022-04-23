using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Auth.GetMe;

/// <summary>
/// Get me query.
/// </summary>
public record GetMeQuery() : IRequest<UserDto>;