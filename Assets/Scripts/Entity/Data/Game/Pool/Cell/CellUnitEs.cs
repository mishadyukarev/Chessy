using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        readonly Dictionary<ButtonTypes, CellUnitAbilityButtonsE> _uniqueButtons;
        readonly Dictionary<AbilityTypes, CellUnitAbilityE> _abilities;
        readonly Dictionary<PlayerTypes, CellUnitVisibleE> _cellUnitVisibles;

        public CellUnitAbilityButtonsE AbilityButton(in ButtonTypes button) => _uniqueButtons[button];
        public CellUnitAbilityE Ability(in AbilityTypes ability) => _abilities[ability];
        public CellUnitVisibleE VisibleE(in PlayerTypes player) => _cellUnitVisibles[player];

        public readonly CellUnitE UnitE;
        public readonly CellUnitMainToolWeaponE MainToolWeaponE;
        public readonly CellUnitExtraToolWeaponE ExtraToolWeaponE;
        public readonly CellUnitWhoLastDiedHereE WhoLastDiedHereE;

        public HashSet<AbilityTypes> CooldownKeys
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _abilities) keys.Add(item.Key);
                return keys;
            }
        }


        internal CellUnitEs(in CellEs cellEs, in byte idx, in EcsWorld gameW)
        {
            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitAbilityButtonsE>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitAbilityButtonsE(gameW));
            }

            _abilities = new Dictionary<AbilityTypes, CellUnitAbilityE>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _abilities.Add(ability, new CellUnitAbilityE(ability, cellEs, gameW));
            }

            _cellUnitVisibles = new Dictionary<PlayerTypes, CellUnitVisibleE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _cellUnitVisibles[player] = new CellUnitVisibleE(gameW);
            }

            UnitE = new CellUnitE(cellEs, gameW);
            MainToolWeaponE = new CellUnitMainToolWeaponE(gameW);
            ExtraToolWeaponE = new CellUnitExtraToolWeaponE(gameW);
            WhoLastDiedHereE = new CellUnitWhoLastDiedHereE(cellEs, gameW);
        }
    }
}