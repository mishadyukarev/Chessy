using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellEnvironViewCom
    {
        private Dictionary<EnvirTypes, SpriteRenderer> _cellEnviron_SRs;

        public CellEnvironViewCom(GameObject cell)
        {
            _cellEnviron_SRs = new Dictionary<EnvirTypes, SpriteRenderer>();


            var parentGO = cell.transform.Find("Environments").gameObject;

            var sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvirTypes.Fertilizer, sr);


            sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvirTypes.YoungForest, sr);


            sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvirTypes.AdultForest, sr);


            sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvirTypes.Hill, sr);


            sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvirTypes.Mountain, sr);
        }

        private void ActiveSR(EnvirTypes environmentType, bool isEnabled) => _cellEnviron_SRs[environmentType].enabled = isEnabled;

        public void EnableSR(EnvirTypes environmentType) => ActiveSR(environmentType, true);
        public void DisableSR(EnvirTypes environmentType) => ActiveSR(environmentType, false);
    }
}
