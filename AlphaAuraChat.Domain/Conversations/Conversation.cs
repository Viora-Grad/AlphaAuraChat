using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Conversations.Internal;

namespace AlphaAuraChat.Domain.Conversations;

public class Conversation : Entity
{
    public Guid TenantId { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? AissgnedAgentId { get; private set; }
    public BlockInfo BlockInfo { get; private set; } = default!;
    public DateTime CreatedAtUtc { get; private set; }

    private Conversation(Guid id, Guid tenantId, Guid clientId, Guid agentId, BlockInfo block, DateTime createdAtUtc) : base(id)
    {
        TenantId = tenantId;
        CustomerId = clientId;
        ClientId = agentId;
        BlockInfo = block;
        CreatedAtUtc = createdAtUtc;
    }

    private Conversation() : base() { } // for EfCore
}
