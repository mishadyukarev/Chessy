using ECS;

namespace Game.Game
{
    public struct SunSidesE
    {
        static Entity _sunSide;

        public static ref SunSideTC SunSideTC => ref _sunSide.Get<SunSideTC>();

        public SunSidesE(in EcsWorld gameW)
        {
            _sunSide = gameW.NewEntity()
                .Add(new SunSideTC(SunSideTypes.Dawn));
        }
    }
}