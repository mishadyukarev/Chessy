using ECS;

namespace Game.Game
{
    public sealed class CellUnitAbilityCooldownE : EntityAbstract
    {
        readonly AbilityTypes _ability;
        ref AmountC CooldownRef => ref Ent.Get<AmountC>();
        public AmountC Cooldown => Ent.Get<AmountC>();

        public bool HaveCooldown => Cooldown.Amount > 0;

        internal CellUnitAbilityCooldownE(in AbilityTypes ability, in EcsWorld gameW) : base(gameW)
        {
            _ability = ability;
        }

        internal void SetNew()
        {
            CooldownRef.Amount = 0;
        }
        internal void Shift(in CellUnitAbilityCooldownE cooldownE_from)
        {
            CooldownRef = cooldownE_from.Cooldown;
            cooldownE_from.CooldownRef.Amount = 0;
        }

        public void SetAfterAbility()
        {
            CooldownRef.Amount = CellUnitAbilityCooldownValues.NeedAfterAbility(_ability);
        }
        public void TakeAfterUpdate()
        {
            CooldownRef.Amount--;
        }
        public void SyncRpc(in int cooldown)
        {
            CooldownRef.Amount = cooldown;
        }
    }
}