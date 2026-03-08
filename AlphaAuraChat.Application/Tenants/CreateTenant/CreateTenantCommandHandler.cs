using AlphaAuraChat.Application.Abstractions.Cipher;
using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;
using AlphaAuraChat.Domain.Users;
using DomainTenant = AlphaAuraChat.Domain.Tenants.Tenant;
namespace AlphaAuraChat.Application.Tenants.CreateTenant;

public class CreateTenantCommandHandler(
    ITenantRepository tenantRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ICipher cipher,
    KeyGenerationService generateKeyService,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateTenantCommand, Guid>
{
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly KeyGenerationService _generateKeyService = generateKeyService;
    private readonly ICipher _cipher = cipher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDateTimeProvider _DateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var onwer = await _userRepository.GetByIdAsync(request.tenantOwner.Id);
        if (onwer == null)
        {
            throw new NotFoundException($"User with id {request.tenantOwner.Id} not found");
        }
        var key = _generateKeyService.GenerateKey();
        var encryptionKey = _cipher.Encrypt(key);
        var newTenant = DomainTenant.Create(request.name, request.contanct, onwer, encryptionKey, _DateTimeProvider.UtcNow);
        _tenantRepository.Add(newTenant);
        await _unitOfWork.SaveChangesAsync();
        return Result<Guid>.Success(newTenant.Id);
    }
}