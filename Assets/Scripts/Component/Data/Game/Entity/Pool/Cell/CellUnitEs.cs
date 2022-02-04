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
        public HashSet<AbilityTypes> CooldownKeys
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _abilities) keys.Add(item.Key);
                return keys;
            }
        }


        public readonly CellUnitMainE MainE;
        public readonly CellUnitLevelE LevelE;
        public readonly CellUnitOwnerE OwnerE;
        public readonly CellUnitConditonE ConditionE;
        public readonly CellUnitCornedE CornedE;
        public readonly CellUnitToolWeaponE ToolWeaponE;
        public readonly CellUnitWhoLastDiedHereE WhoLastDiedHereE;


        public readonly CellUnitStatEs StatEs;
        public readonly CellUnitEffectEs EffectEs;


        public CellUnitEs(in byte idx, in EcsWorld gameW)
        {
            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitAbilityButtonsE>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitAbilityButtonsE(gameW));
            }

            _abilities = new Dictionary<AbilityTypes, CellUnitAbilityE>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _abilities.Add(ability, new CellUnitAbilityE(ability, idx, gameW));
            }

            _cellUnitVisibles = new Dictionary<PlayerTypes, CellUnitVisibleE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _cellUnitVisibles[player] = new CellUnitVisibleE(gameW);
            }


            MainE = new CellUnitMainE(idx, gameW);
            OwnerE = new CellUnitOwnerE(idx, gameW);
            ConditionE = new CellUnitConditonE(idx, gameW);
            LevelE = new CellUnitLevelE(idx, gameW);
            ToolWeaponE = new CellUnitToolWeaponE(idx, gameW);
            CornedE = new CellUnitCornedE(idx, gameW);
            WhoLastDiedHereE = new CellUnitWhoLastDiedHereE(idx, gameW);

            StatEs = new CellUnitStatEs(idx, gameW);
            EffectEs = new CellUnitEffectEs(idx, gameW);
        }
    }
}