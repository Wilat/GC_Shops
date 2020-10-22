using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace GC_Shops.Features.Implementations.MetroComponent
{
    public class MetroStation : IEquatable<MetroStation>
    {
        private MetroStation() { }

        public MetroStation(string name, MetroOwner owner)
        {
            MetroStationId = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid MetroStationId { get; private set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; private set; }

        public MetroOwner Owner { get; private set; }

        public ISet<ControlBlock> ControlBlocks { get; private set; }
        public bool Equals([AllowNull] MetroStation other) =>
            other != null
            && (
                ReferenceEquals(this, other) 
                || MetroStationId == other.MetroStationId
                || (MetroStationId == default || other.MetroStationId == default) && Name == other.Name);

        public override bool Equals(object obj) =>
            obj is MetroStation station && Equals(station);
    }
}
