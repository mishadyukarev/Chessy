using ECS;
using Game.Common;

namespace Game.Game
{
    public readonly struct EntitiesUI
    {
        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;

        public LeftCityUIEs LeftCityEs => LeftEs.CityEs;
        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;
        public LeftMarketUIEs LeftMarketEs => LeftEs.MarketEs;
        public LeftSmelterUIEs LeftSmelterEs => LeftEs.SmelterEs;

        internal EntitiesUI(in EcsWorld gameW)
        {
            LeftEs = new LeftUIEs(gameW);
            RightEs = new RightUIEs(gameW);
            CenterEs = new CenterUIEs(gameW);


            ///Up
            var upZone = CanvasC.FindUnderCurZone("UpZone").transform;
            new EconomyUpUIE(gameW, upZone);
            new UpSunsUIEs(gameW, upZone);

            ///Down
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;
            new DownToolWeaponUIEs(gameW, downZone);
            new UIEntDownDoner(gameW, downZone);
            new DownUpgradeUIE(gameW, downZone);
            new DownPawnUIE(gameW, downZone);
            new DownScoutUIEs(gameW, downZone);
            new DownHeroUIE(gameW, downZone);
        }
    }
}