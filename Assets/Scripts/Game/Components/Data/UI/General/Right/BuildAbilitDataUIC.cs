using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public struct BuildAbilitDataUIC
    {
        private static Dictionary<BuildButtonTypes, AbilityTypes> _abilities;

        public BuildAbilitDataUIC(bool needNew) : this()
        {
            if (needNew)
            {
                _abilities = new Dictionary<BuildButtonTypes, AbilityTypes>();

                _abilities.Add(BuildButtonTypes.First, default);
                _abilities.Add(BuildButtonTypes.Second, default);
                _abilities.Add(BuildButtonTypes.Third, default);
            }
        }

        public static AbilityTypes AbilityType(BuildButtonTypes buildButType) => _abilities[buildButType];
        public static void SetAbilityType(BuildButtonTypes buildButType, AbilityTypes abilityType) => _abilities[buildButType] = abilityType;
    }
}