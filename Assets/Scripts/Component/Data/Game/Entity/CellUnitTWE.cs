using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitTWE : EntityAbstract
    {
        public ref ToolWeaponTC ToolWeapon => ref Ent.Get<ToolWeaponTC>();
        public ref LevelTC LevelTW => ref Ent.Get<LevelTC>();
        public ref AmountC Protection => ref Ent.Get<AmountC>();

        public CellUnitTWE(in EcsWorld gameW) : base(gameW) { }

        public void Shift(in CellUnitTWE twE_from)
        {
            ToolWeapon = twE_from.ToolWeapon;
            LevelTW = twE_from.LevelTW;
            Protection = twE_from.Protection;
        }
        public void SetNew(in ToolWeaponTypes tw, in LevelTypes level)
        {
            ToolWeapon.ToolWeapon = tw;
            LevelTW.Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        Protection.Amount = 1;
                        break;

                    case LevelTypes.Second:
                        Protection.Amount = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
        public void SetNew(in (ToolWeaponTypes, LevelTypes) tw) => SetNew(tw.Item1, tw.Item2);

        public void BreakShield(in int taking = 1)
        {
            if (!ToolWeapon.IsShield) throw new Exception();
            if (!Protection.Have) throw new Exception();

            Protection.Take(taking);

            if (!Protection.Have) ToolWeapon.Reset();
        }

        public void Reset()
        {
            ToolWeapon.Reset();
            LevelTW.Reset();
            Protection.Reset();
        }
    }
}