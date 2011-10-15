using System;
using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class ActorView
    {
        public ActorView(Actor actor, World world, TextureManager textureManager, AnimationManager animationManager)
        {
            Actor = actor;
            World = world;
            TextureManager = textureManager;
            AnimationManager = animationManager;

            Actor.Spawned += Actor_Spawned;
            Actor.Died += Actor_Died;
        }

        protected Actor Actor { get; private set; }
        protected World World { get; private set; }

        protected TextureManager TextureManager { get; private set; }
        protected AnimationManager AnimationManager { get; private set; }

        private void Actor_Spawned(object sender, EventArgs e)
        {
            World.NewActorViews.Add(this);
        }

        private void Actor_Died(object sender, EventArgs e)
        {
            World.GarbageActorViews.Add(this);
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}