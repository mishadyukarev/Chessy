using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellKingViewComponent
    {
        private SpriteRenderer _king_SR;

        internal CellKingViewComponent(GameObject unitsZone_GO)
        {
            _king_SR = unitsZone_GO.GetComponent<SpriteRenderer>();
        }
    }
}
