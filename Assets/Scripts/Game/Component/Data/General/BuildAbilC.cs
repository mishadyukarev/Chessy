using System.Collections.Generic;

namespace Game.Game
{
    public struct BuildAbilC
    {
        private static Dictionary<BuildButtonTypes, BuildAbilTypes> _abilities;

        public static BuildAbilTypes AbilityType(BuildButtonTypes buildBut) => _abilities[buildBut];


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


        public static void SetAbility(BuildButtonTypes buildBut, BuildAbilTypes ability) => _abilities[buildBut] = ability;
    }
}