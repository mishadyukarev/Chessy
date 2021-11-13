using UnityEngine;

namespace Chessy.Game
{
    public struct SupportVC
    {
        private SpriteRenderer _supVis_SR;

        public SupportVC(GameObject cell_GO)
        {
            var parentGO = cell_GO.transform.Find("SupportVision").gameObject;
            _supVis_SR = parentGO.GetComponent<SpriteRenderer>();
        }

        public void SetColor(SupVisTypes supVisType) => _supVis_SR.color = ColorsValues.Color(supVisType);

        public void EnableSR() => _supVis_SR.enabled = true;
        public void DisableSR() => _supVis_SR.enabled = false;
    }
}
