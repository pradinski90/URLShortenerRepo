using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Core.Messaging
{
    public abstract class Event : Message, INotification
    {
    }
}
