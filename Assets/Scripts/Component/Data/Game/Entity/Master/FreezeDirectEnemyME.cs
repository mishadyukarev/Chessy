using ECS;

namespace Game.Game
{
    public sealed class FreezeDirectEnemyME : EntityAbstract
    {
        public ref IdxFromToC IdxFromToC => ref Ent.Get<IdxFromToC>();

        public FreezeDirectEnemyME(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}