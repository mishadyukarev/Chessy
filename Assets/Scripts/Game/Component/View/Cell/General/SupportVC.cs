using UnityEngine;

namespace Game.Game
{
    public struct SupportVC : IElseCellV
    {
        private SpriteRenderer _supVis_SR;

        public SupportVC(GameObject cell_GO)
        {
            var parentGO = cell_GO.transform.Find("SupportVision").gameObject;
            _supVis_SR = parentGO.GetComponent<SpriteRenderer>();
        }

        public void EnableSR(SupVisTypes supVis)
        {
            _supVis_SR.enabled = true;
            _supVis_SR.color = ColorsValues.Color(supVis);
        }
        public void DisableSR() => _supVis_SR.enabled = false;
    }
}
