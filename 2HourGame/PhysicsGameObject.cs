using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

namespace _2HourGame
{
    class PhysicsGameObject : GameObject
    {
        PhysicsSimulator physicsSimulator;

        public PhysicsGameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, AnimatedTextureInfo animatedTextureInfo
            , EffectManager effectManger, float zIndex)
            : base(game, initialPosition, contentName, boundsMultiplyer, color, spriteBatch, animatedTextureInfo, effectManger, zIndex)
        {

            this.physicsSimulator = physicsSimulator;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            physicsSimulator.Add(this.body);

            physicsSimulator.Add(geometry);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
