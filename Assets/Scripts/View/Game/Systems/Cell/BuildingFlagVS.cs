using UnityEngine;

namespace Chessy.Game.System.View
{
    public struct BuildingFlagVS
    {
        bool _needActive;

        public void Sync(in SpriteRendererVC srC, in byte idx_0, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            _needActive = false;

            if (e.BuildingEs(idx_0).BuildingTC.HaveBuilding)
            {
                _needActive = true;
                srC.SR.color = e.BuildingPlayerTC(idx_0).Is(PlayerTypes.First) ? Color.blue : Color.red;
            }

            srC.SetActive(_needActive);
        }
    }
}