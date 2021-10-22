using System;
using UnityEngine;

namespace Scripts.Game
{
    internal struct CellSupViewComponent
    {
        private SpriteRenderer _supVis_SR;

        internal CellSupViewComponent(GameObject cell_GO)
        {
            var parentGO = cell_GO.transform.Find("SupportVision").gameObject;
            _supVis_SR = parentGO.GetComponent<SpriteRenderer>();
        }

        internal void SetColor(SupVisTypes supVisType)=> _supVis_SR.color = ColorsValues.Color(supVisType);

        internal void EnableSR() => _supVis_SR.enabled = true;
        internal void DisableSR() => _supVis_SR.enabled = false;
    }
}
