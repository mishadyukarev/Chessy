using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    public struct IsSettedBuildingDictComponent
    {
        private Dictionary<bool, bool> _isSettedBuilding;

        internal void StartFill()
        {
            _isSettedBuilding = new Dictionary<bool, bool>();

            _isSettedBuilding.Add(true, default);
            _isSettedBuilding.Add(false, default);
        }

        internal bool IsSettedBuilding(bool key) => _isSettedBuilding[key];
        internal void SetIsSettedBuilding(bool key, bool value) => _isSettedBuilding[key] = value;
    }
}
