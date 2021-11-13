using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct EnvVC
    {
        private Dictionary<EnvTypes, SpriteRenderer> _cellEnviron_SRs;

        public EnvVC(GameObject cell)
        {
            _cellEnviron_SRs = new Dictionary<EnvTypes, SpriteRenderer>();


            var parentGO = cell.transform.Find("Environments").gameObject;

            var sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvTypes.Fertilizer, sr);


            sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvTypes.YoungForest, sr);


            sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvTypes.AdultForest, sr);


            sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvTypes.Hill, sr);


            sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvTypes.Mountain, sr);
        }

        private void ActiveSR(EnvTypes environmentType, bool isEnabled) => _cellEnviron_SRs[environmentType].enabled = isEnabled;

        public void EnableSR(EnvTypes environmentType) => ActiveSR(environmentType, true);
        public void DisableSR(EnvTypes environmentType) => ActiveSR(environmentType, false);
    }
}
