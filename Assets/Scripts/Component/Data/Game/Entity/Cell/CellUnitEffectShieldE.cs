using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitEffectShieldE : CellUnitEffectE
    {
        ref AmountC AmountCRef => ref Ent.Get<AmountC>();

        public int Shield
        {
            get => AmountCRef.Amount;
            internal set => AmountCRef.Amount = value;
        }
        public bool HaveShieldEffect => Shield > 0;

        internal CellUnitEffectShieldE(in byte idx, in EcsWorld gameW) : base(EffectTypes.Shield, idx, gameW) { }

        internal void Shift(in CellUnitEffectShieldE shieldE_from)
        {
            AmountCRef.Amount = shieldE_from.Shield;
            shieldE_from.AmountCRef.Amount = 0;
        }
        public void Set(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.ActiveAroundBonusSnowy:
                    AmountCRef.Amount = 1;
                    break;

                case AbilityTypes.DirectWave:
                    AmountCRef.Amount = 1;
                    break;

                default: throw new Exception();
            }
        }
        public void Take(in int taking = 1)
        {
            AmountCRef.Amount -= taking;
        }
        public void Reset()
        {
            AmountCRef.Amount = 0;
        }
    }
}