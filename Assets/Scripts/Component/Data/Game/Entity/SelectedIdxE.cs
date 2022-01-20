using ECS;

namespace Game.Game
{
    public struct SelectedIdxE
    {
        static Entity _ent;

        public static ref IdxC IdxC => ref _ent.Get<IdxC>();

        public static bool IsSelCell => IdxC.Idx != 0;

        public SelectedIdxE(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new IdxC(0));
        }
    }
}