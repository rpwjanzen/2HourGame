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

        public float XRadius { get; set; }
        public float YRadius { get; set; }
        public float Width { get { return XRadius * 2; } }
        public float Height { get { return YRadius * 2; } }

        public float Scale { get; set; }

        public Vector2 Origin { get; private set; }

        public event EventHandler GameObjectRemoved;
        public GameObject(Game game, Vector2 position, string contentName, float scale)
            : base(game)
        {
            this.Position = position;
            this.Scale = scale;

            // why is the scale here 1 and not scale???
            Origin = ((ITextureManager)game.Services.GetService(typeof(ITextureManager))).getTextureCentre(contentName, 1);

            XRadius = Origin.X * Scale;
            YRadius = Origin.Y * Scale;

            Game.Components.ComponentRemoved += new EventHandler<GameComponentCollectionEventArgs>(Components_ComponentRemoved);
        }

        void Components_ComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            if (e.GameComponent == this)
            {
                RaiseGameObjectRemovedEvent();
            }
        }

        /// <summary>
        /// Occurs when this game object is removed from the Game's components
        /// </summary>
        void RaiseGameObjectRemovedEvent()
        {
            GameObjectRemoved(this, EventArgs.Empty);
        }
    }
}
