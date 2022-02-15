using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitExtraToolWeaponE : EntityAbstract
    {
        public ref ToolWeaponTC ToolWeaponTC => ref Ent.Get<ToolWeaponTC>();
        public ref LevelTC LevelTC => ref Ent.Get<LevelTC>();
        public ref ShieldProtectionC ProtectionShieldC => ref Ent.Get<ShieldProtectionC>();

        internal CellUnitExtraToolWeaponE(in EcsWorld gameW) : base(gameW) { }

        public void SetNew(in ToolWeaponTypes tw, in LevelTypes level)
        {
            ToolWeaponTC.ToolWeapon = tw;
            LevelTC.Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        ProtectionShieldC.Protection = 0.1f;
                        break;

                    case LevelTypes.Second:
                        ProtectionShieldC.Protection = 0.3f;
                        break;

                    default: throw new Exception();
                }
            }
        }
        public void BreakShield(in float taking)
        {
            if (!ToolWeaponTC.Is(ToolWeaponTypes.Shield)) throw new Exception();
            if (!ProtectionShieldC.HaveProtection) throw new Exception();

            ProtectionShieldC.Protection -= taking;

            if (!ProtectionShieldC.HaveProtection) ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
        }
        public void Reset()
        {
            ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
            LevelTC.Level = LevelTypes.None;
            ProtectionShieldC.Protection = 0;
        }

    }
}