using Chessy.Common;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Entity.Cell.Unit
{
    public struct UnitEs
    {
        readonly AbilityTC[] _uniqueButtons;
        readonly CooldownC[] _abilities;
        readonly ForPlayerE[] _visibles;
        readonly StepsC[] _needStepsForShift;
        readonly Dictionary<AttackTypes, IdxsCellsC> _forAttack;

        public IdxsCellsC ForArson;
        public IdxsCellsC ForShift;

        public CellUnitMainE MainE;
        public StatsE StatsE;
        public CellUnitEffectsE EffectsE;
        public CellUnitMainToolWeaponE MainToolWeaponE;
        public ExtraToolWeaponE ExtraToolWeaponE;
        public WhoLastDiedHereE WhoLastDiedHereE;
        public CellUnitExtractE ExtractE;


        public IdxsCellsC ForAttack(in AttackTypes attack) => _forAttack[attack];
        public ref StepsC NeedSteps(in byte idx_cell) => ref _needStepsForShift[idx_cell];

        public ref AbilityTC Ability(in ButtonTypes button) => ref _uniqueButtons[(byte)button - 1];
        public ref CooldownC CoolDownC(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public ref ForPlayerE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player];


        internal UnitEs(in byte[] xy) : this()
        {
            _uniqueButtons = new AbilityTC[(byte)ButtonTypes.End - 1];

            _abilities = new CooldownC[(byte)AbilityTypes.End - 1];

            _visibles = new ForPlayerE[(byte)PlayerTypes.End];
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _visibles[(byte)player] = new ForPlayerE();
            }

            ForArson = new IdxsCellsC(new HashSet<byte>());
            ForShift = new IdxsCellsC(new HashSet<byte>());

            _needStepsForShift = new StepsC[StartValues.CELLS];
            _forAttack = new Dictionary<AttackTypes, IdxsCellsC>();

            _forAttack[AttackTypes.Simple] = new IdxsCellsC(new HashSet<byte>());
            _forAttack[AttackTypes.Unique] = new IdxsCellsC(new HashSet<byte>());
        }

        public void Set(in UnitEs unitE)
        {
            MainE = unitE.MainE;
            EffectsE = unitE.EffectsE;
            StatsE = unitE.StatsE;
            MainToolWeaponE = unitE.MainToolWeaponE;
            ExtraToolWeaponE = unitE.ExtraToolWeaponE;

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                Ability(buttonT) = unitE.Ability(buttonT);
            }
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                CoolDownC(abilityT) = unitE.CoolDownC(abilityT);
            }
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                ForPlayer(playerT) = unitE.ForPlayer(playerT);
            }
        }
    }
}