using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        public readonly CellUnitMainE MainE;
        public readonly CellUnitTWE ToolWeaponE;

        readonly Dictionary<ButtonTypes, CellUnitAbilityButtonsE> _uniqueButtons;
        readonly Dictionary<AbilityTypes, CellUnitCooldownAbilityE> _cooldownUniques;
        readonly Dictionary<PlayerTypes, CellUnitVisibleE> _cellUnitVisibles;
        public CellUnitAbilityButtonsE AbilityButton(in ButtonTypes button) => _uniqueButtons[button];
        public CellUnitCooldownAbilityE CooldownAbility(in AbilityTypes ability) => _cooldownUniques[ability];
        public CellUnitVisibleE VisibleE(in PlayerTypes player) => _cellUnitVisibles[player];

        public HashSet<AbilityTypes> CooldownKeys
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _cooldownUniques) keys.Add(item.Key);
                return keys;
            }
        }

        public readonly CellUnitStatEs StatEs;
        public readonly CellUnitEffectEs EffectEs;


        public CellUnitEs(in byte idx, in EcsWorld gameW)
        {
            MainE = new CellUnitMainE(idx, gameW);
            ToolWeaponE = new CellUnitTWE(gameW);

            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitAbilityButtonsE>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitAbilityButtonsE(gameW));
            }

            _cooldownUniques = new Dictionary<AbilityTypes, CellUnitCooldownAbilityE>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _cooldownUniques.Add(ability, new CellUnitCooldownAbilityE(ability, gameW));
            }

            _cellUnitVisibles = new Dictionary<PlayerTypes, CellUnitVisibleE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _cellUnitVisibles[player] = new CellUnitVisibleE(gameW);
            }


            StatEs = new CellUnitStatEs(idx, gameW);
            EffectEs = new CellUnitEffectEs(idx, gameW);
        }
    }
}