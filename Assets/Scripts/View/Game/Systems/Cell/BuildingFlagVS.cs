using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game
{
    sealed class BuildingFlagVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _flagSRC;

        internal BuildingFlagVS(in SpriteRendererVC flagSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _flagSRC = flagSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.BuildingTC(_currentCell).HaveBuilding)
            {
                _needActive = true;
                _flagSRC.SR.color = _e.BuildingPlayerTC(_currentCell).Is(PlayerTypes.First) ? Color.blue : Color.red;
            }

            _flagSRC.SetActive(_needActive);
        }
    }
}