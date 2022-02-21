using ECS;
using Game.Common;

namespace Game.Game
{
    public class EntitiesViewUI
    {
        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;
        public readonly DownUIEs DownEs;

        public LeftCityUIEs LeftCityEs => LeftEs.CityEs;
        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;
        public LeftMarketUIEs LeftMarketEs => LeftEs.MarketEs;
        public LeftSmelterUIEs LeftSmelterEs => LeftEs.SmelterEs;

        public EntitiesViewUI(in EcsWorld gameW)
        {
            LeftEs = new LeftUIEs(gameW);
            RightEs = new RightUIEs(gameW);
            CenterEs = new CenterUIEs(gameW);
            DownEs = new DownUIEs(gameW);

            ///Up
            var upZone = CanvasC.FindUnderCurZone("UpZone").transform;
            new EconomyUpUIE(gameW, upZone);
            new UpSunsUIEs(gameW, upZone);
        }
    }
}