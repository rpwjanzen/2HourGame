using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    static class GameComponentCollectionExtensions
    {
        public static void AddRange<T>(this GameComponentCollection collection, IEnumerable<T> items) where T : IGameComponent
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
            
    }
}
