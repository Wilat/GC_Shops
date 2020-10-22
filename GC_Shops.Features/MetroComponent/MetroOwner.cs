using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.MetroComponent
{
    public class MetroOwner
    {
        private MetroOwner() { }

        public MetroOwner(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            MetroOwnerId = Guid.NewGuid();
            Name = name;
        }

        public Guid MetroOwnerId { get; private set; }
        public string Name { get; private  set; }
    }
}
