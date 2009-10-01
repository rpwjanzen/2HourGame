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
            : this(game, position, contentName, scale, ((ITextureManager)game.Services.GetService(typeof(ITextureManager))).getTextureCentre(contentName, 1))
        {
            // why is the scale in getTextureCentre aboce here 1 and not scale???
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        /// <param name="contentName"></param>
        /// <param name="scale"></param>
        /// <param name="origin">Optional, lets you specify an origin thats not the default one (that is based on the texture.)</param>
        public GameObject(Game game, Vector2 position, string contentName, float scale, Vector2 origin)
            : base(game)
        {
            this.Position = position;
            this.Scale = scale;

            this.Origin = origin;

            XRadius = Origin.X * Scale;
            YRadius = Origin.Y * Scale;
        }

        /// <summary>
        /// This must be called when you remove a game object that has a view so that the associated view also gets removed.
        /// </summary>
        public void removeGameObject()
        {
            GameObjectRemoved(this, EventArgs.Empty);
        }
    }
}
