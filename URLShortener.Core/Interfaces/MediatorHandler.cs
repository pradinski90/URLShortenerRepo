using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Messaging;

namespace URLShortener.Core.Interfaces
{
    public interface IMediatorHandler
    {
    //    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
    //where TNotification : INotification;  From DOC
        Task PublishEvent<T>(T @event) where T : Event;

        Task<TCommandRespose> SendCommand<TCommand, TCommandRespose>(TCommand command)
            where TCommand : Command<TCommandRespose>;

        Task<TQueryResponse> SendQuery<TQuery, TQueryResponse>(TQuery query)
            where TQuery : Query<TQueryResponse>;
    }
}
