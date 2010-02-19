using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// THIS CLASS IS PROCEDURALLY MODIFIED DO NOT CHANTE THE CONTENT ENUM OR THE LOADTEXTURES REGION
namespace _2HourGame.View.GameServices
{
    public enum Content
    {
        BoatHitByCannonAnimation,
        BoundingCircle,
        CannonAnimation,
        CannonBall,
        CannonSmokeAnimation,
        CutterGunwale,
        CutterHull,
        CutterRigging,
        Dig,
        FloatingCrate,
        Gold,
        GoldGetAnimation,
        GoldLoseAnimation,
        HealthBar,
        House,
        Island,
        ProgressBar,
        Repair,
        ShipSinking,
        SloopGunwale,
        SloopHull,
        SloopRigging,
        SmallFort,
        SplashAnimation,
        Tower,
        XboxControllerBack,
        XboxControllerButtonA,
        XboxControllerButtonB,
        XboxControllerButtonGuide,
        XboxControllerButtonX,
        XboxControllerButtonY,
        XboxControllerDPad,
        XboxControllerLeftShoulder,
        XboxControllerLeftThumbstick,
        XboxControllerLeftTrigger,
        XboxControllerRightShoulder,
        XboxControllerRightThumbstick,
        XboxControllerRightTrigger,
        XboxControllerSpriteFont,
        XboxControllerStart
    }


    static class TextureReference
    {
        static public void LoadTextures(TextureManager textureManager)
        {
            #region LoadTextures
            textureManager.LoadTexture(Content.BoatHitByCannonAnimation, "ShipImages\\boatHitByCannonAnimation");
            textureManager.LoadTexture(Content.BoundingCircle, "boundingCircle");
            textureManager.LoadTexture(Content.CannonAnimation, "cannonAnimation");
            textureManager.LoadTexture(Content.CannonBall, "cannonBall");
            textureManager.LoadTexture(Content.CannonSmokeAnimation, "cannonSmokeAnimation");
            textureManager.LoadTexture(Content.CutterGunwale, "ShipImages\\Cutter\\cutterGunwale");
            textureManager.LoadTexture(Content.CutterHull, "ShipImages\\Cutter\\cutterHull");
            textureManager.LoadTexture(Content.CutterRigging, "ShipImages\\Cutter\\cutterRigging");
            textureManager.LoadTexture(Content.Dig, "dig");
            textureManager.LoadTexture(Content.FloatingCrate, "floatingCrate");
            textureManager.LoadTexture(Content.Gold, "gold");
            textureManager.LoadTexture(Content.GoldGetAnimation, "goldGetAnimation");
            textureManager.LoadTexture(Content.GoldLoseAnimation, "goldLoseAnimation");
            textureManager.LoadTexture(Content.HealthBar, "healthBar");
            textureManager.LoadTexture(Content.House, "house");
            textureManager.LoadTexture(Content.Island, "island");
            textureManager.LoadTexture(Content.ProgressBar, "progressBar");
            textureManager.LoadTexture(Content.Repair, "repair");
            textureManager.LoadTexture(Content.ShipSinking, "ShipImages\\shipSinking");
            textureManager.LoadTexture(Content.SloopGunwale, "ShipImages\\Sloop\\sloopGunwale");
            textureManager.LoadTexture(Content.SloopHull, "ShipImages\\Sloop\\sloopHull");
            textureManager.LoadTexture(Content.SloopRigging, "ShipImages\\Sloop\\sloopRigging");
            textureManager.LoadTexture(Content.SmallFort, "smallFort");
            textureManager.LoadTexture(Content.SplashAnimation, "splashAnimation");
            textureManager.LoadTexture(Content.Tower, "tower");
            textureManager.LoadTexture(Content.XboxControllerBack, "ControllerImages\\xboxControllerBack");
            textureManager.LoadTexture(Content.XboxControllerButtonA, "ControllerImages\\xboxControllerButtonA");
            textureManager.LoadTexture(Content.XboxControllerButtonB, "ControllerImages\\xboxControllerButtonB");
            textureManager.LoadTexture(Content.XboxControllerButtonGuide, "ControllerImages\\xboxControllerButtonGuide");
            textureManager.LoadTexture(Content.XboxControllerButtonX, "ControllerImages\\xboxControllerButtonX");
            textureManager.LoadTexture(Content.XboxControllerButtonY, "ControllerImages\\xboxControllerButtonY");
            textureManager.LoadTexture(Content.XboxControllerDPad, "ControllerImages\\xboxControllerDPad");
            textureManager.LoadTexture(Content.XboxControllerLeftShoulder, "ControllerImages\\xboxControllerLeftShoulder");
            textureManager.LoadTexture(Content.XboxControllerLeftThumbstick, "ControllerImages\\xboxControllerLeftThumbstick");
            textureManager.LoadTexture(Content.XboxControllerLeftTrigger, "ControllerImages\\xboxControllerLeftTrigger");
            textureManager.LoadTexture(Content.XboxControllerRightShoulder, "ControllerImages\\xboxControllerRightShoulder");
            textureManager.LoadTexture(Content.XboxControllerRightThumbstick, "ControllerImages\\xboxControllerRightThumbstick");
            textureManager.LoadTexture(Content.XboxControllerRightTrigger, "ControllerImages\\xboxControllerRightTrigger");
            textureManager.LoadTexture(Content.XboxControllerSpriteFont, "ControllerImages\\xboxControllerSpriteFont");
            textureManager.LoadTexture(Content.XboxControllerStart, "ControllerImages\\xboxControllerStart");
            #endregion
        }
    }
}
