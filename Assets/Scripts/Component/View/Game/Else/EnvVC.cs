//using System.Collections.Generic;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct EnvVC : IEnvCellV
//    {
//        private Dictionary<EnvTypes, SpriteRenderer> _cellEnviron_SRs;

//        public EnvVC(GameObject cell)
//        {
//            _cellEnviron_SRs = new Dictionary<EnvTypes, SpriteRenderer>();



//        }

//        private void ActiveSR(EnvTypes environmentType, bool isEnabled) => _cellEnviron_SRs[environmentType].enabled = isEnabled;

//        public void EnableSR(EnvTypes environmentType) => ActiveSR(environmentType, true);
//        public void DisableSR(EnvTypes environmentType) => ActiveSR(environmentType, false);
//    }
//}
