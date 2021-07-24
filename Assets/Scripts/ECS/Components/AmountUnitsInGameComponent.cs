using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountUnitsInGameComponent
    {
        private Dictionary<bool, int> _amountUnitsInGame;

        internal void StartFill()
        {
            _amountUnitsInGame = new Dictionary<bool, int>();

            _amountUnitsInGame.Add(true, default);
            _amountUnitsInGame.Add(false, default);
        }

        internal int GetAmountUnitsInGame(bool key) => _amountUnitsInGame[key];
        internal void SetAmountUnitsInGane(bool key, int value) => _amountUnitsInGame[key] = value;
        internal void AddAmountUnitsInGame(bool key, int adding = 1) => _amountUnitsInGame[key] += adding;
        internal void TakeAmountUnitsInGame(bool key, int taking = 1) => _amountUnitsInGame[key] -= taking;
    }
}
