using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TryDryWetCellsMS : SystemModel
    {
        internal TryDryWetCellsMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryDry()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.FertilizeC(cellIdx0).HaveAnyResources)
                {
                    eMG.FertilizeC(cellIdx0).Resources -= EnvironmentValues.DRY_FERTILIZE_DURING_UPDATE_TAKING;
                }
            }
        }
    }
}