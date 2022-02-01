using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitEffectShieldE : EntityAbstract
    {
        ref AmountC AmountCRef => ref Ent.Get<AmountC>();
        public ref AmountC AmountC => ref Ent.Get<AmountC>();

        public bool HaveShieldEffect => AmountC.Amount > 0;

        internal CellUnitEffectShieldE(in EcsWorld gameW) : base(gameW) { }

        internal void Shift(in CellUnitEffectShieldE shieldE_from)
        {
            AmountCRef.Amount = shieldE_from.AmountC.Amount;
            shieldE_from.AmountCRef.Amount = 0;
        }
        public void Set(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.ActiveIceWall:
                    AmountCRef.Amount++;
                    break;
                default: throw new Exception();
            }
        }
        public void Take(in int taking = 1)
        {
            AmountCRef.Amount -= taking;
        }
    }
}