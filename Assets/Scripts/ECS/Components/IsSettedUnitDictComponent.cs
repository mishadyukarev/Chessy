using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct IsSettedUnitDictComponent
    {
        private Dictionary<bool, bool> _isSettedUnitDict;

        internal void StartFill()
        {
            _isSettedUnitDict = new Dictionary<bool, bool>();

            _isSettedUnitDict.Add(true, false);
            _isSettedUnitDict.Add(false, false);
        }

        internal bool IsSettedUnit(bool key) => _isSettedUnitDict[key];
        internal void SetIsSettedUnit(bool key, bool value) => _isSettedUnitDict[key] = value;
    }
}
