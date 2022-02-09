using ECS;
using Game.Common;

namespace Game.Game
{
    public readonly struct LeftUIEs
    {
        public readonly LeftCityUIEs CityEs;
        public readonly LeftEnvironmentUIEs EnvironmentEs;

        internal LeftUIEs(in EcsWorld gameW)
        {
            ///Left
            var leftZone = CanvasC.FindUnderCurZone("Left+").transform;
            CityEs = new LeftCityUIEs(gameW, leftZone);
            EnvironmentEs = new LeftEnvironmentUIEs(gameW, leftZone);
        }
    }
}