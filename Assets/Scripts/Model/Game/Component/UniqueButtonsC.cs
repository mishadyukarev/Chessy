using Chessy.Common;

namespace Chessy.Game.Model.Component
{
    public struct UniqueButtonsC
    {
        readonly AbilityTypes[] _uniqueButtons;

        public AbilityTypes Ability(in ButtonTypes button) => _uniqueButtons[(byte)button - 1];

        internal UniqueButtonsC(in AbilityTypes[] uniqueButtons) => _uniqueButtons = uniqueButtons;

        internal void SetAbility(in ButtonTypes button, in AbilityTypes ability) => _uniqueButtons[(byte)button - 1] = ability;
    }
}