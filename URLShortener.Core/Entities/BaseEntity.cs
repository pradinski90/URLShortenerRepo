using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
    }
}
