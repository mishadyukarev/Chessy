using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitTWE : EntityAbstract
    {
        ref ToolWeaponTC ToolWeaponTCRef => ref Ent.Get<ToolWeaponTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        ref AmountC ProtectionRef => ref Ent.Get<AmountC>();

        public ToolWeaponTC ToolWeaponTC => Ent.Get<ToolWeaponTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();
        public AmountC Protection =>Ent.Get<AmountC>();

        public CellUnitTWE(in EcsWorld gameW) : base(gameW) { }

        internal void Set(in CellUnitTWE twE)
        {
            ToolWeaponTCRef = twE.ToolWeaponTC;
            LevelTCRef = twE.LevelTC;
            ProtectionRef = twE.Protection;
        }
        public void SetNew(in ToolWeaponTypes tw, in LevelTypes level)
        {
            ToolWeaponTCRef.ToolWeapon = tw;
            LevelTCRef.Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        ProtectionRef.Amount = 1;
                        break;

                    case LevelTypes.Second:
                        ProtectionRef.Amount = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
        public void SetNew(in (ToolWeaponTypes, LevelTypes) tw) => SetNew(tw.Item1, tw.Item2);

        public void BreakShield(in int taking = 1)
        {
            if (!ToolWeaponTC.IsShield) throw new Exception();
            if (!Protection.Have) throw new Exception();

            ProtectionRef.Amount -= taking;

            if (!Protection.Have) ToolWeaponTCRef.ToolWeapon = ToolWeaponTypes.None;
        }

        public void Reset()
        {
            ToolWeaponTCRef.ToolWeapon = ToolWeaponTypes.None;
            LevelTCRef.Level = LevelTypes.None;
            ProtectionRef.Amount = 0;
        }
    }
}