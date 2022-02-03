using ECS;

namespace Game.Game
{
    public sealed class CellUnitEffectStunEs : CellUnitEffectE
    {
        ref AmountC ForExitStunRef => ref Ent.Get<AmountC>();
        public AmountC ForExitStun => Ent.Get<AmountC>();

        public bool IsStunned => ForExitStun.Amount > 0;

        internal CellUnitEffectStunEs(in byte idx, in EcsWorld gameW) : base(EffectTypes.Stun, idx, gameW) { }

        internal void Shift(in CellUnitEffectStunEs stunE_from)
        {
            ForExitStunRef.Amount = stunE_from.ForExitStun.Amount;
            stunE_from.ForExitStunRef.Amount = 0;
        }

        public void Reset() => ForExitStunRef.Amount = 0;
        public void ExecuteAfterTrainingDoner() => ForExitStunRef.Amount -= 2;
        public void ExecuteAfterWithFriendDoner() => ForExitStunRef.Amount -= 2;
        public void SyncRpc(in int forExitStun) => ForExitStunRef.Amount = forExitStun;

        public void Set(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.StunElfemale:
                    ForExitStunRef.Amount = 4;
                    break;

                case AbilityTypes.ActiveAroundBonusSnowy:
                    ForExitStunRef.Amount = 2;
                    break;

                case AbilityTypes.DirectWave:
                    ForExitStunRef.Amount = 2;
                    break;

                default: throw new System.Exception();
            }
        }
        public void SetAfterAttackFrozenArrow()
        {
            ForExitStunRef.Amount = 2;
        }
    }
}