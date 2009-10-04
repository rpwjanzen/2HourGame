using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class GameObject : GameComponent, IGameObject
    {
        public virtual Vector2 Position { get; set; }
        public virtual float Rotation { get; set; } // in radians

        public float HalfWidth
        {
            get { return Width / 2.0f; }
        }

        public float HalfHeight
        {
            get { return Height / 2.0f; }
        }

        public float Width { get; private set; }
        public float Height { get; private set; }

        /// <summary>
        /// Occurs when this game object is removed from the Game's components
        /// </summary>
        public event EventHandler GameObjectRemoved;

        public GameObject(Game game, Vector2 position, float width, float height)
            : base(game)
        {
            this.Position = position;

            Width = width;
            Height = height;

            Game.Components.ComponentRemoved += Components_ComponentRemoved;
        }

        void Components_ComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            if (e.GameComponent == this)
            {
                RaiseGameObjectRemovedEvent();
            }
        }

        void RaiseGameObjectRemovedEvent()
        {
            if (GameObjectRemoved != null)
            {
                GameObjectRemoved(this, EventArgs.Empty);
            }
        }
    }
}
