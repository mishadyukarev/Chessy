using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct AmountUnitsInStandartConditionComponent
    {
        private Dictionary<bool, List<int[]>> _unitsInNone;
        private Dictionary<bool, List<int[]>> _unitsInProtect;
        private Dictionary<bool, List<int[]>> _unitsInRelax;

        internal Dictionary<bool, List<int[]>> UnitsInNone => _unitsInNone;
        internal Dictionary<bool, List<int[]>> UnitsInProtect => _unitsInProtect;
        internal Dictionary<bool, List<int[]>> UnitsInRelax => _unitsInRelax;

        internal void StartFill()
        {
            _unitsInNone = new Dictionary<bool, List<int[]>>();
            _unitsInProtect = new Dictionary<bool, List<int[]>>();
            _unitsInRelax = new Dictionary<bool, List<int[]>>();


            _unitsInNone.Add(true, new List<int[]>());
            _unitsInNone.Add(false, new List<int[]>());

            _unitsInProtect.Add(true, new List<int[]>());
            _unitsInProtect.Add(false, new List<int[]>());

            _unitsInRelax.Add(true, new List<int[]>());
            _unitsInRelax.Add(false, new List<int[]>());
        }
    }
}
