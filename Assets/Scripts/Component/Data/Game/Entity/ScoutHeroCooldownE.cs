using ECS;

namespace Game.Game
{
    public sealed class ScoutHeroCooldownE : EntityAbstract
    {
        public ref AmountC Cooldown => ref Ent.Get<AmountC>();

        public bool HaveCooldown => Cooldown.Amount > 0;

        public ScoutHeroCooldownE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}