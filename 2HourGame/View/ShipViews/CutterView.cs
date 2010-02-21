using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class CutterView : ShipView
    {
        //TODO: SET HEALTH (health manager?)

        public CutterView(Game game, Color shipOutlineColor, Color color, SpriteBatch spriteBatch, IShip ship)
            : base(game, shipOutlineColor, Content.CutterHull, color, spriteBatch, ship)
        {
        
        }

        protected override void loadTextures()
        {
            gunwale = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[Content.CutterGunwale];
            rigging = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[Content.CutterRigging];
        }
    }
}
