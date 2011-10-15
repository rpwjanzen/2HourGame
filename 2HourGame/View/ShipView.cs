using System;
using System.Linq;
using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class ShipView : GameObjectView
    {
        private readonly CannonView LeftCannonView;
        private readonly CannonView RightCannonView;
        private readonly HealthBarView healthBarView;
        private readonly Ship ship;
        private readonly Color shipOutlineColor;
        private Texture2D gunwale;
        private Texture2D rigging;

        public ShipView(World world, Color shipOutlineColor, string contentName, Color color, Ship ship,
                        TextureManager tm, AnimationManager am)
            : base(
                world, contentName, color, ship, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipHull), tm, am
                )
        {
            this.shipOutlineColor = shipOutlineColor;
            this.ship = ship;

            ship.Died += ship_Died;
            ship.GoldLoaded += ship_GoldLoaded;
            ship.Damaged += ship_Damaged;

            healthBarView = new HealthBarView(world, ship, tm, am);
            LeftCannonView = new CannonView(world, ship.LeftCannons.First(), Color.White, tm, am);
            RightCannonView = new CannonView(world, ship.RightCannons.First(), Color.White, tm, am);
        }

        private void ship_Damaged(object sender, DamagedEventArgs e)
        {
            AnimationManager.PlayAnimation(Animation.BoatHitByCannon, e.DamagePosition);
        }

        private void ship_GoldLoaded(object sender, EventArgs e)
        {
            AnimationManager.PlayAnimation(Animation.GetGold, ship.Position);
        }

        public override void LoadContent(ContentManager content)
        {
            gunwale = TextureManager["shipGunwale"];
            rigging = TextureManager["shipRigging"];

            healthBarView.LoadContent(content);
            LeftCannonView.LoadContent(content);
            RightCannonView.LoadContent(content);

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ship.IsAlive)
            {
                spriteBatch.Draw(gunwale, GameObject.Position, null, shipOutlineColor, GameObject.Rotation, base.Origin,
                                 base.Scale, SpriteEffects.None,
                                 ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGunwale));
                spriteBatch.Draw(rigging, GameObject.Position, null, Color.White, GameObject.Rotation, base.Origin,
                                 base.Scale, SpriteEffects.None,
                                 ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipRigging));

                healthBarView.Draw(gameTime, spriteBatch);
                LeftCannonView.Draw(gameTime, spriteBatch);
                RightCannonView.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime, spriteBatch);
        }

        private void ship_Died(object sender, EventArgs e)
        {
            AnimationManager.PlayAnimation(Animation.ShipSinking, ship.Position);
            AnimationManager.PlayAnimation(Animation.FloatingCrate, ship.Position);
        }
    }
}