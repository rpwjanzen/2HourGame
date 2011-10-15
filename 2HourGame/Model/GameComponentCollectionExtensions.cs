using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal static class GameComponentCollectionExtensions
    {
        public static void AddRange<T>(this GameComponentCollection collection, IEnumerable<T> items)
            where T : IGameComponent
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }
    }
}