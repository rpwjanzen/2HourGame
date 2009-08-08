using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    public delegate void Notifation();

    class GameObject : GameComponent
    {
        public virtual Vector2 Position { get; set; }
        public virtual float Rotation { get; set; } // in radians

        public float XRadius { get; set; }
        public float YRadius { get; set; }
        public float Width { get { return XRadius * 2; } }
        public float Height { get { return YRadius * 2; } }
        public bool IsCircle {
            get { return this.XRadius == this.YRadius; }
        }

        public float Scale;

        public Vector2 Origin { get; private set; }

        public Game game;

        public event Notifation GameObjectRemoved;

        public GameObject(Game game, Vector2 position, string contentName, float scale)
            : base(game)
        {
            this.Position = position;
            this.game = game;
            this.Scale = scale;

            Origin = ((ITextureManager)game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(contentName, Scale);

            XRadius = Origin.X * Scale;
            YRadius = Origin.Y * Scale;
        }

        /// <summary>
        /// This must be called when you remove a game object that has a view so that the associated view also gets removed.
        /// </summary>
        public void removeGameObject()
        {
            GameObjectRemoved();
        }
    }
}
