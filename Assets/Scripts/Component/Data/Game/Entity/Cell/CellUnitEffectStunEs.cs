using ECS;

namespace Game.Game
{
    public sealed class CellUnitEffectStunEs : CellUnitEffectE
    {
        ref AmountC StunCRef => ref Ent.Get<AmountC>();

        public int Stun
        {
            get => StunCRef.Amount;
            set => StunCRef.Amount = value;
        }
        public bool IsStunned => Stun > 0;

        internal CellUnitEffectStunEs(in byte idx, in EcsWorld gameW) : base(EffectTypes.Stun, idx, gameW) { }

        internal void Shift(in CellUnitEffectStunEs stunE_from)
        {
            StunCRef.Amount = stunE_from.Stun;
            stunE_from.StunCRef.Amount = 0;
        }

        public void Reset() => StunCRef.Amount = 0;
        public void ExecuteAfterTrainingDoner() => StunCRef.Amount -= 2;
        public void ExecuteAfterWithFriendDoner() => StunCRef.Amount -= 2;
        public void SyncRpc(in int forExitStun) => StunCRef.Amount = forExitStun;

        public void Set(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.StunElfemale:
                    StunCRef.Amount = 4;
                    break;

                case AbilityTypes.ActiveAroundBonusSnowy:
                    StunCRef.Amount = 2;
                    break;

                case AbilityTypes.DirectWave:
                    StunCRef.Amount = 2;
                    break;

                default: throw new System.Exception();
            }
        }
        public void SetAfterAttackFrozenArrow()
        {
            StunCRef.Amount = 2;
        }
    }
}