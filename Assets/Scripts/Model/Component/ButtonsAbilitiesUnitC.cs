namespace Chessy.Model.Component
{
    public struct ButtonsAbilitiesUnitC
    {
        public readonly AbilityTypes[] UniqueButtonsArray;

        public byte[] AbilityTypesClone
        {
            get
            {
                var abilities = new byte[UniqueButtonsArray.Length];
                for (var i = 0; i < UniqueButtonsArray.Length; i++)
                {
                    abilities[i] = (byte)UniqueButtonsArray[i];
                }
                return abilities;
            }
        }
        internal ref AbilityTypes AbilityRef(in ButtonTypes button) => ref UniqueButtonsArray[(byte)button];
        public AbilityTypes Ability(in ButtonTypes button) => UniqueButtonsArray[(byte)button];

        internal ButtonsAbilitiesUnitC(in AbilityTypes[] uniqueButtons) => UniqueButtonsArray = uniqueButtons;

        internal void SetAbility(in ButtonTypes button, in AbilityTypes ability) => UniqueButtonsArray[(byte)button] = ability;
        internal void Sync(in byte[] abilityTs)
        {
            for (var i = 0; i < abilityTs.Length; i++)
            {
                UniqueButtonsArray[i] = (AbilityTypes)abilityTs[i];
            }
        }
    }
}