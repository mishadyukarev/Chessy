using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    sealed class TryChangeDirectionWindMS : SystemModel
    {
        internal TryChangeDirectionWindMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {

        }

        internal void TryChange()
        {
            if (UnityEngine.Random.Range(0f, 1f) > UpdateValues.PERCENT_FOR_CHANGING_WIND) eMG.WeatherE.WindC.Speed = UnityEngine.Random.Range(1, 4);
        }
    }
}