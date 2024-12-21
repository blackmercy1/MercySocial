using MercySocial.Application.Common.Repository;
using MercySocial.Domain.MessageAggregate;
using MercySocial.Domain.MessageAggregate.ValueObjects;

namespace MercySocial.Application.Messages.Repository;

public interface IMessageRepository : IRepository<Message, MessageId>;