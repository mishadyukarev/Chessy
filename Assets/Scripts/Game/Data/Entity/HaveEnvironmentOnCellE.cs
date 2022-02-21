using ECS;

namespace Game.Game
{
    public sealed class HaveEnvironmentOnCellE : EntityAbstract
    {
        public ref HaveC HaveEnv => ref Ent.Get<HaveC>();

        public HaveEnvironmentOnCellE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}