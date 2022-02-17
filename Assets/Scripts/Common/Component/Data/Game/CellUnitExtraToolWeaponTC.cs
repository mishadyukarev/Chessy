//using System;

//namespace Game.Game
//{
//    public class CellUnitExtraToolWeaponTC : ToolWeaponTC
//    {
//        public void SetNew(in ToolWeaponTypes tw, in LevelTypes level, in LevelTC levTC, in CellUnitExtraShieldProtectionC shieldPC)
//        {
//            ToolWeapon = tw;
//            levTC.Level = level;

//            if (tw == ToolWeaponTypes.Shield)
//            {
//                switch (level)
//                {
//                    case LevelTypes.First:
//                        shieldPC.Protection = 0.1f;
//                        break;

//                    case LevelTypes.Second:
//                        shieldPC.Protection = 0.3f;
//                        break;

//                    default: throw new Exception();
//                }
//            }
//        }
//    }
//}