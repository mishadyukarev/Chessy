namespace Chessy.Model.Component
{
    public struct ButtonsAbilitiesUnitC
    {
        readonly AbilityTypes[] _uniqueButtons;

        public byte[] AbilityTypesClone
        {
            get
            {
                var abilities = new byte[_uniqueButtons.Length];
                for (var i = 0; i < _uniqueButtons.Length; i++)
                {
                    abilities[i] = (byte)_uniqueButtons[i];
                }
                return abilities;
            }
        }
        public AbilityTypes Ability(in ButtonTypes button) => _uniqueButtons[(byte)button];

        internal ButtonsAbilitiesUnitC(in AbilityTypes[] uniqueButtons) => _uniqueButtons = uniqueButtons;

        internal void SetAbility(in ButtonTypes button, in AbilityTypes ability) => _uniqueButtons[(byte)button] = ability;
        internal void Sync(in byte[] abilityTs)
        {
            for (var i = 0; i < abilityTs.Length; i++)
            {
                _uniqueButtons[i] = (AbilityTypes)abilityTs[i];
            }
        }
    }
}