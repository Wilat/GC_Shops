using GC_Shops.Features.Implementations.CoordinateComponent;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GC_Shops.Features.Implementations.MetroComponent
{
    public class ControlBlock : IEquatable<ControlBlock>
    {
        private ControlBlock() { }

        public ControlBlock(int id, string name, Coordinate coordinate, IDictionary<MapDirection, ControlBlock> boundedCB)
        {
            ControlBlockId = id;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Coordinate = coordinate ?? throw new ArgumentNullException(nameof(coordinate));

            foreach (var (direction, cb) in boundedCB)
            {
                SetCb(direction, cb);
            }
        }

        public int ControlBlockId { get; private set; }
        public string Name { get; private set; }

        public Coordinate Coordinate { get; private set; }

        public ControlBlock SouthCb { get; private set; }
        public ControlBlock NorthCb { get; private set; }
        public ControlBlock EastCb { get; private set; }
        public ControlBlock WestCb { get; private set; }

        public void SetCb(MapDirection directions, ControlBlock cb)
        {
            switch (directions)
            {
                case MapDirection.North:
                    NorthCb = cb;
                    break;
                case MapDirection.East:
                    EastCb = cb;
                    break;
                case MapDirection.South:
                    SouthCb = cb;
                    break;
                case MapDirection.West:
                    WestCb = cb;
                    break;
            }
        }

        public bool Equals([AllowNull] ControlBlock other) =>
            other != null
            && (
                ReferenceEquals(this, other) 
                || ControlBlockId == other.ControlBlockId
                || (ControlBlockId == default || other.ControlBlockId == default) && Coordinate.Equals(other.Coordinate));

        public override bool Equals(object obj) =>
            obj is ControlBlock cb && Equals(cb);
    }
}
