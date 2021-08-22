using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellEnvironViewCom
    {
        private Dictionary<EnvironmentTypes, SpriteRenderer> _cellEnviron_SRs;

        internal CellEnvironViewCom(GameObject cell)
        {
            _cellEnviron_SRs = new Dictionary<EnvironmentTypes, SpriteRenderer>();


            var parentGO = cell.transform.Find("Environments").gameObject;

            var sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvironmentTypes.Fertilizer, sr);


            sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvironmentTypes.YoungForest, sr);


            sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvironmentTypes.AdultForest, sr);


            sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvironmentTypes.Hill, sr);


            sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();
            _cellEnviron_SRs.Add(EnvironmentTypes.Mountain, sr);
        }

        private void ActiveSR(EnvironmentTypes environmentType, bool isEnabled) => _cellEnviron_SRs[environmentType].enabled = isEnabled;

        internal void EnableSR(EnvironmentTypes environmentType) => ActiveSR(environmentType, true);
        internal void DisableSR(EnvironmentTypes environmentType) => ActiveSR(environmentType, false);
    }
}
