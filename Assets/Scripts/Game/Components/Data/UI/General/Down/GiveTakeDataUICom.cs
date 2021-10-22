//using System.Collections.Generic;

//namespace Scripts.Game
//{
//    public struct GiveTakeDataUICom
//    {
//        private Dictionary<GiveTWTypes, ToolWeaponTypes> _curToolWeap;

//        internal GiveTakeDataUICom(bool needNew) : this()
//        {
//            if (needNew)
//            {
//                _curToolWeap = new Dictionary<GiveTWTypes, ToolWeaponTypes>();

//                _curToolWeap.Add(GiveTWTypes.Pick, ToolWeaponTypes.Pick);
//                _curToolWeap.Add(GiveTWTypes.Sword, ToolWeaponTypes.Sword);
//                _curToolWeap.Add(GiveTWTypes.Shield, ToolWeaponTypes.Shield);
//            }
//        }

//        internal ToolWeaponTypes CurTWType(GiveTWTypes giveTWType) => _curToolWeap[giveTWType];
//        internal void SetCurTWType(GiveTWTypes giveTWType, ToolWeaponTypes toolWeaponType) => _curToolWeap[giveTWType] = toolWeaponType;
//    }
//}