using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountUnitsInGameComponent
    {
        internal Dictionary<bool, List<int[]>> AmountUnitsInGame { get; private set; }

        internal AmountUnitsInGameComponent(Dictionary<bool, List<int[]>> dict)
        {
            AmountUnitsInGame = dict;
            AmountUnitsInGame.Add(true, new List<int[]>());
            AmountUnitsInGame.Add(false, new List<int[]>());
        }
    }
}
