using System.Collections.Generic;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct RightUniqueDataUIC
    {
        private static Dictionary<UniqueButtonTypes, AbilityTypes> _uniqueMovings;

        public RightUniqueDataUIC(bool needNew) : this()
        {
            if (needNew)
            {
                _uniqueMovings = new Dictionary<UniqueButtonTypes, AbilityTypes>();
                _uniqueMovings.Add(UniqueButtonTypes.First, default);
                _uniqueMovings.Add(UniqueButtonTypes.Second, default);
                _uniqueMovings.Add(UniqueButtonTypes.Third, default);
            }
        }

        public static AbilityTypes AbilityType(UniqueButtonTypes uniqueButtonType) => _uniqueMovings[uniqueButtonType];
        public static void SetAbilityType(UniqueButtonTypes uniqButType, AbilityTypes abilityType) => _uniqueMovings[uniqButType] = abilityType;
    }
}