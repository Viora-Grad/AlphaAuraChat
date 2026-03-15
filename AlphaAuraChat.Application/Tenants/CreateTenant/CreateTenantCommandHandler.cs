using AlphaAuraChat.Application.Abstractions.Cipher;
using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;
using AlphaAuraChat.Domain.Tenants.Services;
using AlphaAuraChat.Domain.Users;
using DomainTenant = AlphaAuraChat.Domain.Tenants.Tenant;


namespace AlphaAuraChat.Application.Tenants.CreateTenant;


/// <summary>
/// Handles the creation of a new tenant in the system.
/// This handler generates a unique tenant key, creates the tenant aggregate,
/// and persists it using the tenant repository.
///</summary>

public class CreateTenantCommandHandler(
    ITenantRepository tenantRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ICipher cipher,
    IKeyGenerationService generateKeyService,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateTenantCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var owner = await userRepository.GetByIdAsync(request.tenantOwner.Id, cancellationToken)
            ?? throw new NotFoundException($"User with id {request.tenantOwner.Id} not found");
        var key = generateKeyService.GenerateKey();
        var encryptedKey = cipher.Encrypt(key);
        var newTenant = DomainTenant.Create(request.name, request.contanct, owner, encryptedKey, dateTimeProvider.UtcNow);
        tenantRepository.Add(newTenant);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(newTenant.Id);
    }
}