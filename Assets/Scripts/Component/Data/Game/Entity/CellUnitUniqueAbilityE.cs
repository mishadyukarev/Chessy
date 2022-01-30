using ECS;

namespace Game.Game
{
    public sealed class CellUnitUniqueAbilityE : EntityAbstract
    {
        public ref AmountC Cooldown => ref Ent.Get<AmountC>();

        public CellUnitUniqueAbilityE(in EcsWorld gameW) : base(gameW) { }

        public void Shift(in CellUnitUniqueAbilityE cooldownE_from)
        {
            Cooldown = cooldownE_from.Cooldown;
            cooldownE_from.Cooldown.Reset();
        }
    }
}