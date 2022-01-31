using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        readonly CellUnitStunEs[] _stuns;
        readonly CellUnitDefendEffectE[] _defendEffect;
        readonly CellUnitMainE[] _main;
        readonly CellUnitTWE[] _toolWeapons;
        readonly Dictionary<ButtonTypes, CellUnitAbilityButtonsE[]> _uniqueButtons;
        readonly Dictionary<AbilityTypes, CellUnitCooldownAbilityE[]> _cooldownUniques;
        readonly Dictionary<PlayerTypes, CellUnitVisibleE[]> _cellUnitVisibles;


        public CellUnitMainE Main(in byte idx) => _main[idx];
        public CellUnitStunEs Stun(in byte idx) => _stuns[idx];
        public CellUnitDefendEffectE DefendEffect(in byte idx) => _defendEffect[idx];
        public CellUnitTWE ToolWeapon(in byte idx) => _toolWeapons[idx];
        public CellUnitAbilityButtonsE AbilityButton(in ButtonTypes button, in byte idx) => _uniqueButtons[button][idx];
        public CellUnitCooldownAbilityE CooldownAbility(in AbilityTypes ability, in byte idx) => _cooldownUniques[ability][idx];
        public CellUnitVisibleE VisibleE(in PlayerTypes player, in byte idx) => _cellUnitVisibles[player][idx];

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


        public CellUnitEs(in EcsWorld gameW)
        {
            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitAbilityButtonsE[]>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitAbilityButtonsE[CellStartValues.ALL_CELLS_AMOUNT]);
            }


            _cooldownUniques = new Dictionary<AbilityTypes, CellUnitCooldownAbilityE[]>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _cooldownUniques.Add(ability, new CellUnitCooldownAbilityE[CellStartValues.ALL_CELLS_AMOUNT]);
            }


            _cellUnitVisibles = new Dictionary<PlayerTypes, CellUnitVisibleE[]>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _cellUnitVisibles[player] = new CellUnitVisibleE[CellStartValues.ALL_CELLS_AMOUNT];
            }


            _stuns = new CellUnitStunEs[CellStartValues.ALL_CELLS_AMOUNT];
            _defendEffect = new CellUnitDefendEffectE[CellStartValues.ALL_CELLS_AMOUNT];
            _main = new CellUnitMainE[CellStartValues.ALL_CELLS_AMOUNT];
            _toolWeapons = new CellUnitTWE[CellStartValues.ALL_CELLS_AMOUNT];


            for (byte idx = 0; idx < _stuns.Length; idx++)
            {
                _stuns[idx] = new CellUnitStunEs(idx, gameW);
                _defendEffect[idx] = new CellUnitDefendEffectE(gameW);
                _main[idx] = new CellUnitMainE(idx, gameW);
                _toolWeapons[idx] = new CellUnitTWE(gameW);

                foreach (var item in _uniqueButtons) _uniqueButtons[item.Key][idx] = new CellUnitAbilityButtonsE(gameW);
                foreach (var item in _cooldownUniques) _cooldownUniques[item.Key][idx] = new CellUnitCooldownAbilityE(item.Key, gameW);
                foreach (var item in _cellUnitVisibles) _cellUnitVisibles[item.Key][idx] = new CellUnitVisibleE(gameW);
            }


            StatEs = new CellUnitStatEs(gameW);
        }

        public void Shift(byte idx_from, in byte idx_to, in Entities ents)
        {
            var statEs = ents.UnitStatUpgradesEs;
            var whereUnitsEs = ents.WhereUnitsEs;
            var cellEs = ents.CellEs;


            whereUnitsEs.WhereUnit(Main(idx_from), idx_from).HaveUnit.Have = false;

            Main(idx_to).Shift(Main(idx_from));
            StatEs.Hp(idx_to).Shift(StatEs.Hp(idx_from));
            StatEs.Step(idx_to).Shift(StatEs.Step(idx_from));
            StatEs.Water(idx_to).Shift(StatEs.Water(idx_from));
            Stun(idx_to).Shift(Stun(idx_from));

            ToolWeapon(idx_to).Set(ToolWeapon(idx_from));
            foreach (var abilityT in CooldownKeys) CooldownAbility(abilityT, idx_to).Shift(CooldownAbility(abilityT, idx_from));

            if (cellEs.EnvironmentEs.AdultForest(idx_from).HaveEnvironment)
            {
                cellEs.TrailEs.Trail(cellEs.GetDirect(idx_from, idx_to), idx_from).SetNew();
            }
            if (cellEs.EnvironmentEs.AdultForest(idx_to).HaveEnvironment)
            {
                cellEs.TrailEs.Trail(cellEs.GetDirect(idx_from, idx_to).Invert(), idx_to).SetNew();
            }

            if (cellEs.RiverEs.River(idx_to).RiverTC.HaveRiver)
            {
                StatEs.Water(idx_to).SetMax(Main(idx_to), statEs);
            }

            whereUnitsEs.WhereUnit(Main(idx_to), idx_to).HaveUnit.Have = true;
        }
    }
}