using ECS;

namespace Game.Game
{
    public struct CurrentIdxE
    {
        static Entity _ent;

        public static ref IdxC IdxC => ref _ent.Get<IdxC>();

        public static bool IsStartDirectToCell => IdxC.Idx == default;

        public CurrentIdxE(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new IdxC());
        }
    }
}