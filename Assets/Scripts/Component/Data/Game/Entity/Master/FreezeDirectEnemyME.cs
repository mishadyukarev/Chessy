using ECS;

namespace Game.Game
{
    public struct FreezeDirectEnemyME
    {
        static Entity _ent;

        public static ref IdxFromToC IdxFromToC => ref _ent.Get<IdxFromToC>();

        public FreezeDirectEnemyME(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new IdxFromToC());
        }
    }
}