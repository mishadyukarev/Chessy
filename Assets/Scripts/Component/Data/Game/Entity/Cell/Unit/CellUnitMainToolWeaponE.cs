using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitMainToolWeaponE : CellEntityAbstract
    {
        ref ToolWeaponTC ToolWeaponTCRef => ref Ent.Get<ToolWeaponTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();


        public ToolWeaponTypes ToolWeapon
        {
            get => ToolWeaponTCRef.ToolWeapon;
            internal set => ToolWeaponTCRef.ToolWeapon = value;
        }
        public LevelTypes Level
        {
            get => LevelTCRef.Level;
            internal set => LevelTCRef.Level = value;
        }

        internal CellUnitMainToolWeaponE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        public void SetNew(in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            ToolWeapon = twT;
            Level = levelT;
        }
    }
}