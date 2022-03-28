using Chessy.Common.Component;
using UnityEngine;

namespace Chessy.Game.View.Entity
{
    public struct UnitVE
    {
        public readonly GameObjectVC ParenGOC;
        public readonly SpriteRendererVC SelectedSRC;
        public readonly SpriteRendererVC NotSelectedSRC;

        public UnitVE(in GameObject parentGO, in SpriteRenderer selectedSR, in SpriteRenderer notSelectedSR)
        {
            ParenGOC = new GameObjectVC(parentGO);
            SelectedSRC = new SpriteRendererVC(selectedSR);
            NotSelectedSRC = new SpriteRendererVC(notSelectedSR);
        }
    }
}