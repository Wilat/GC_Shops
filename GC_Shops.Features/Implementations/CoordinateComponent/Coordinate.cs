using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GC_Shops.Features.Implementations.CoordinateComponent
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public Coordinate() { }

        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool Equals([AllowNull] Coordinate other) =>
            other != null
            && (
                ReferenceEquals(this, other)
                || X == other.X && Y == other.Y && Z == other.Z);

        public override bool Equals(object obj) =>
            obj is Coordinate coordinate && Equals(coordinate);
    }
}
