using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Application.Tenants.GetTenantUser;

public class GetUserTenantQueryHandler(
    ITenantRepository tenantRepository,
    IUserRepository userRepository) : IQueryHandler<GetTenantUserQuery, IEnumerable<UserResponse>>
{
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetTenantUserQuery request, CancellationToken cancellationToken)
    {
        var Tenant = await _tenantRepository.GetByIdAsync(request.TenantId, cancellationToken)
            ?? throw new NotFoundException($"Tenant with id {request.TenantId} not found.");
        IEnumerable<User> users = await _userRepository.GetByTenantIdAsync(request.TenantId, cancellationToken);
        if (!users.Any())
            throw new NotFoundException($"No users found for tenant with id {request.TenantId}.");
        IEnumerable<UserResponse> userResponses = users.Select(user => new UserResponse
        {
            Id = user.Id,
            TenantID = user.TenantId,
            RelativeId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        }).ToList();
        return Result.Success(userResponses);
    }
}
