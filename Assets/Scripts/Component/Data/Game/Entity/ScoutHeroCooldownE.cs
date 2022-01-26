using ECS;

namespace Game.Game
{
    public sealed class ScoutHeroCooldownE : EntityAbstract
    {
        public ref AmountC Cooldown => ref Ent.Get<AmountC>();

        public ScoutHeroCooldownE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}