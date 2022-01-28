using ECS;
using Game.Common;

namespace Game.Game
{
    public struct UIEs
    {
        public RightUIEs RightEs { get; private set; }

        public UIEs(in EcsWorld gameW)
        {
            RightEs = new RightUIEs(gameW);

            ///Left
            var leftZone = CanvasC.FindUnderCurZone("LeftZone").transform;
            new EntityLeftCityUIPool(gameW, leftZone);
            new EntityLeftEnvUIPool(gameW, leftZone);

            ///Center
            var centerZone = CanvasC.FindUnderCurZone("CenterZone").transform;
            new EntityCenterUIPool(gameW, centerZone);
            new CenterHerosUIE(gameW, centerZone);
            new CenterFriendUIE(gameW, centerZone);
            new CenterUpgradeUIE(gameW, centerZone);
            new CenterHintUIE(gameW, centerZone);
            new CenterSelectorUIE(gameW, centerZone);
            new CenterKingUIE(gameW, centerZone);
            new MistakeUIE(gameW, centerZone);

            ///Up
            var upZone = CanvasC.FindUnderCurZone("UpZone").transform;
            new EconomyUpUIE(gameW, upZone);
            new UpSunsUIEs(gameW, upZone);

            ///Down
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;
            new DownToolWeaponUIEs(gameW, downZone);
            new UIEntDownDoner(gameW, downZone);
            new UIEntDownUpgrade(gameW, downZone);
            var takeUnitZone = downZone.Find("TakeUnitZone");
            new PawnArcherDownUIE(gameW, takeUnitZone);
            new UIEntDownScout(gameW, takeUnitZone);
            new DownHeroUIE(gameW, takeUnitZone);
        }
    }
}