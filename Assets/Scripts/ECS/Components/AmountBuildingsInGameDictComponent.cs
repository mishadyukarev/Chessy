using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountBuildingsInGameDictComponent
    {
        internal Dictionary<bool, int> _amountBuildingsInGameDict;

        internal void StartFill()
        {
            _amountBuildingsInGameDict = new Dictionary<bool, int>();

            _amountBuildingsInGameDict.Add(true, default);
            _amountBuildingsInGameDict.Add(false, default);
        }

        internal int AmountBuildingsInGame(bool key) => _amountBuildingsInGameDict[key];
        internal void SetAmountBuildingsInGame(bool key, int value) => _amountBuildingsInGameDict[key] = value;
        internal int ResetAmountBuildingsInGame(bool key) => _amountBuildingsInGameDict[key] = default;
        internal int AddAmountBuildingsInGame(bool key, int adding = 1) => _amountBuildingsInGameDict[key] += adding;
        internal int TakeAmountBuildingsInGame(bool key, int taking = 1) => _amountBuildingsInGameDict[key] -= taking;
    }
}
