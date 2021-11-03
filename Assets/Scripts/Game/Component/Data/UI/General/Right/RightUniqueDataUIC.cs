using System.Collections.Generic;

namespace Scripts.Game
{
    public struct RightUniqueDataUIC
    {
        private static Dictionary<UniqueButtonTypes, UniqueAbilTypes> _uniqueMovings;

        public RightUniqueDataUIC(bool needNew) : this()
        {
            if (needNew)
            {
                _uniqueMovings = new Dictionary<UniqueButtonTypes, UniqueAbilTypes>();
                _uniqueMovings.Add(UniqueButtonTypes.First, default);
                _uniqueMovings.Add(UniqueButtonTypes.Second, default);
                _uniqueMovings.Add(UniqueButtonTypes.Third, default);
            }
        }

        public static UniqueAbilTypes AbilityType(UniqueButtonTypes uniqueButtonType) => _uniqueMovings[uniqueButtonType];
        public static void SetAbilityType(UniqueButtonTypes uniqButType, UniqueAbilTypes abilityType) => _uniqueMovings[uniqButType] = abilityType;
    }
}