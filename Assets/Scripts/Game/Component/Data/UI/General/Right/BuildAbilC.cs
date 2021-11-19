using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildAbilC
    {
        private static Dictionary<BuildButtonTypes, BuildAbilTypes> _abilities;

        public BuildAbilC(bool needNew) : this()
        {
            if (needNew)
            {
                _abilities = new Dictionary<BuildButtonTypes, BuildAbilTypes>();

                _abilities.Add(BuildButtonTypes.First, default);
                _abilities.Add(BuildButtonTypes.Second, default);
                _abilities.Add(BuildButtonTypes.Third, default);
            }
        }

        public static BuildAbilTypes AbilityType(BuildButtonTypes buildButType) => _abilities[buildButType];
        public static void SetAbilityType(BuildButtonTypes buildButType, BuildAbilTypes abilityType) => _abilities[buildButType] = abilityType;
    }
}