using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Application.Users.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository) : ICommandHandler<CreateCustomerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var tenant = await tenantRepository.GetByIdAsync(request.TenantId, cancellationToken)
            ?? throw new NotFoundException($"Tenant with id {request.TenantId} not found.");

        var newCustomer = Customer.Create(request.FirstName, request.LastName, tenant.Id, request.RelativeId);
        customerRepository.Add(newCustomer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(newCustomer.Id);
    }
}
