using MercySocial.Application.Messages.Repository;
using MercySocial.Domain.MessageAggregate;
using MercySocial.Domain.MessageAggregate.ValueObjects;
using MercySocial.Infrastructure.Common.Repository;
using MercySocial.Infrastructure.Data;

namespace MercySocial.Infrastructure.Messages.Repository;

public class MessageRepository : EntityRepository<Message,MessageId,Guid>,IMessageRepository
{
    public MessageRepository(
        ApplicationDbContext dbContext) : base(
        dbContext)
    { }
}