using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct UnitsInStandartConditionComponent
    {
        internal Dictionary<bool, List<int[]>> UnitsInNone { get; private set; }
        internal Dictionary<bool, List<int[]>> UnitsInProtect { get; private set; }
        internal Dictionary<bool, List<int[]>> UnitsInRelax { get; private set; }

        internal UnitsInStandartConditionComponent((Dictionary<bool, List<int[]>>, Dictionary<bool, List<int[]>>, Dictionary<bool, List<int[]>>) dicts)
        {
            UnitsInNone = dicts.Item1;
            UnitsInProtect = dicts.Item2;
            UnitsInRelax = dicts.Item3;


            UnitsInNone.Add(true, new List<int[]>());
            UnitsInNone.Add(false, new List<int[]>());

            UnitsInProtect.Add(true, new List<int[]>());
            UnitsInProtect.Add(false, new List<int[]>());

            UnitsInRelax.Add(true, new List<int[]>());
            UnitsInRelax.Add(false, new List<int[]>());
        }
    }
}
