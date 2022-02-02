using ECS;

namespace Game.Game
{
    public sealed class ScoutHeroCooldownE : EntityAbstract
    {
        ref AmountC CooldownRef => ref Ent.Get<AmountC>();
        public AmountC Cooldown => Ent.Get<AmountC>();

        public bool HaveCooldown => Cooldown.Amount > 0;

        internal ScoutHeroCooldownE(in EcsWorld gameW) : base(gameW)
        {
        }

        public void SetCooldownAfterKill(in UnitTypes unit)
        {
            CooldownRef.Amount = ScoutHeroCooldownValues.AfterKill(unit);
        }
        public void UpdateCooldown()
        {
            CooldownRef.Amount--;
        }
        public void SyncRpc(in int cooldown) => CooldownRef.Amount = cooldown;
    }
}