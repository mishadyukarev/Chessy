﻿using Chessy.Game.Model.Entity;
using Chessy.Game.System;
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

            if (_e.BuildingOnCellT(_currentCell).HaveBuilding())
            {
                _needActive = true;
                _flagSRC.SR.color = _e.BuildingPlayerT(_currentCell).Is(PlayerTypes.First) ? Color.blue : Color.red;
            }

            _flagSRC.SetActive(_needActive);
        }
    }
}