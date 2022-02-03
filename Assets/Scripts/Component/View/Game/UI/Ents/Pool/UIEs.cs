using ECS;
using Game.Common;

namespace Game.Game
{
    public readonly struct UIEs
    {
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;

        internal UIEs(in EcsWorld gameW)
        {
            RightEs = new RightUIEs(gameW);
            CenterEs = new CenterUIEs(gameW);

            ///Left
            var leftZone = CanvasC.FindUnderCurZone("LeftZone").transform;
            new EntityLeftCityUIPool(gameW, leftZone);
            new EntityLeftEnvUIPool(gameW, leftZone);


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