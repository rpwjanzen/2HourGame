using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2HourGame.View.GameServices;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View {
    class CannonBallView : GameObjectView {
        public CannonBallView(CannonBall cannonBall, World world, TextureManager tm, AnimationManager am)
            : base(world, "cannonBall", Color.White, cannonBall, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonBall), tm, am) {
        }
    }
}
