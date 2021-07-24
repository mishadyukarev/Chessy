using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountUnitsInGameComponent
    {
        private Dictionary<bool, List<int[]>> _amountUnitsInGame;

        internal Dictionary<bool, List<int[]>> AmountUnitsInGame => _amountUnitsInGame;

        internal AmountUnitsInGameComponent(Dictionary<bool, List<int[]>> dict)
        {
            _amountUnitsInGame = dict;
            _amountUnitsInGame.Add(true, new List<int[]>());
            _amountUnitsInGame.Add(false, new List<int[]>());
        }
    }
}
