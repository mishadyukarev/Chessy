using ECS;

namespace Game.Game
{
    public sealed class SunSidesE : EntityAbstract
    {
        public ref SunSideTC SunSideTC => ref Ent.Get<SunSideTC>();

        public SunSidesE(in SunSideTypes sunSide, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SunSideTC(sunSide));
        }
    }
}