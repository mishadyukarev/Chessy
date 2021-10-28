//using System.Collections.Generic;

//namespace Scripts.Game
//{
//    public struct GiveTakeDataUICom
//    {
//        private Dictionary<GiveTWTypes, ToolWeaponTypes> _curToolWeap;

//        public GiveTakeDataUICom(bool needNew) : this()
//        {
//            if (needNew)
//            {
//                _curToolWeap = new Dictionary<GiveTWTypes, ToolWeaponTypes>();

//                _curToolWeap.Add(GiveTWTypes.Pick, ToolWeaponTypes.Pick);
//                _curToolWeap.Add(GiveTWTypes.Sword, ToolWeaponTypes.Sword);
//                _curToolWeap.Add(GiveTWTypes.Shield, ToolWeaponTypes.Shield);
//            }
//        }

//        public ToolWeaponTypes CurTWType(GiveTWTypes giveTWType) => _curToolWeap[giveTWType];
//        public void SetCurTWType(GiveTWTypes giveTWType, ToolWeaponTypes toolWeaponType) => _curToolWeap[giveTWType] = toolWeaponType;
//    }
//}