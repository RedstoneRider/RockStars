using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RockStars.Modals;

namespace RockStars.Comparators
{
    public class SongComparator : IEqualityComparer<Song>
    {
        public bool Equals(Song x, Song y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Song obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
