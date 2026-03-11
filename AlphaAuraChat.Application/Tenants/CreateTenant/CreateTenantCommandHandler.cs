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
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IKeyGenerationService _generateKeyService = generateKeyService;
    private readonly ICipher _cipher = cipher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDateTimeProvider _DateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var owner = await _userRepository.GetByIdAsync(request.tenantOwner.Id, cancellationToken)
            ?? throw new NotFoundException($"User with id {request.tenantOwner.Id} not found");
        var key = _generateKeyService.GenerateKey();
        var encryptedKey = _cipher.Encrypt(key);
        var newTenant = DomainTenant.Create(request.name, request.contanct, owner, encryptedKey, _DateTimeProvider.UtcNow);
        _tenantRepository.Add(newTenant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(newTenant.Id);
    }
}