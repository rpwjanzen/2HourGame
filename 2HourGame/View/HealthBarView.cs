using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class HealthBarView : ActorView
    {
        GameObject gameObject;
        Vector2 offset;
        ProgressBar progressBar;

        public HealthBarView(World world, GameObject gameObject)
            :base(gameObject, world)
        {
            this.gameObject = gameObject;
            offset = new Vector2(0, 35);

            progressBar = new ProgressBar();
            progressBar.FillColor = Color.Green;
            progressBar.EmptyColor = Color.Red;
        }

        public override void LoadContent(ContentManager content)
        {
            progressBar.LoadContent((ITextureManager)Game.Services.GetService(typeof(ITextureManager)));

            base.LoadContent(content);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (gameObject.IsDamaged && gameObject.IsAlive)
            {
                progressBar.Progress = gameObject.HealthPercentage;
                progressBar.Position = gameObject.Position + offset;
                progressBar.Draw(spriteBatch);
            }
        }

    }
}
