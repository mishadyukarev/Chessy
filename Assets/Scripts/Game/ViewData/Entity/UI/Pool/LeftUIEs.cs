using Chessy.Common;

namespace Chessy.Game
{
    public struct LeftUIEs
    {
        public readonly LeftCityUIEs CityEs;
        public readonly LeftEnvironmentUIEs EnvironmentEs;
        public readonly LeftMarketUIEs MarketEs;
        public readonly LeftSmelterUIEs SmelterEs;

        internal LeftUIEs(in bool def)
        {
            var leftZone = CanvasC.FindUnderCurZone("Left+").transform;

            CityEs = new LeftCityUIEs(leftZone);
            EnvironmentEs = new LeftEnvironmentUIEs(leftZone);
            MarketEs = new LeftMarketUIEs(leftZone);
            SmelterEs = new LeftSmelterUIEs(leftZone);
        }
    }
}