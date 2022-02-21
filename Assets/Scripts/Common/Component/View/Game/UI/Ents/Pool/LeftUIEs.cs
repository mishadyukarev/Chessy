using ECS;
using Game.Common;

namespace Game.Game
{
    public readonly struct LeftUIEs
    {
        public readonly LeftCityUIEs CityEs;
        public readonly LeftEnvironmentUIEs EnvironmentEs;
        public readonly LeftMarketUIEs MarketEs;
        public readonly LeftSmelterUIEs SmelterEs;

        internal LeftUIEs(in EcsWorld gameW)
        {
            var leftZone = CanvasC.FindUnderCurZone("Left+").transform;

            CityEs = new LeftCityUIEs(leftZone, gameW);
            EnvironmentEs = new LeftEnvironmentUIEs(leftZone, gameW);
            MarketEs = new LeftMarketUIEs(leftZone, gameW);
            SmelterEs = new LeftSmelterUIEs(leftZone, gameW);
        }
    }
}