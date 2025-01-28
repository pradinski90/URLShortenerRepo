using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Interfaces.Messaging;

namespace URLShortener.Core.Messaging
{
    public abstract class Query<TResponse> : Message, IRequest<TResponse>, IQuery
    {
    }
}
