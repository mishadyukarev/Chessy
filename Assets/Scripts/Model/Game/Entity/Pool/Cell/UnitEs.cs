using Chessy.Common;
using Chessy.Game.Model.Entity.Cell.Unit;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Entity.Model.Cell.Unit
{
    public struct UnitEs
    {
        public bool NeedUpdateView;

        readonly AbilityTC[] _uniqueButtons;
        readonly CooldownC[] _abilities;
        readonly ForPlayerE[] _visibles;
        readonly StepsC[] _needStepsForShift;

        public readonly IdxsCellsC SimpleAttack;
        public readonly IdxsCellsC UniqueAttack;

        public readonly IdxsCellsC ForArson;
        public readonly IdxsCellsC ForShift;

        public UnitMainE MainE;
        public StatsE StatsE;
        public EffectsE EffectsE;
        public MainToolWeaponE MainToolWeaponE;
        public ExtraToolWeaponE ExtraToolWeaponE;
        public WhoLastDiedHereE WhoLastDiedHereE;
        public CellUnitExtractE ExtractE;


        public ref StepsC NeedSteps(in byte idx_cell) => ref _needStepsForShift[idx_cell];

        public ref AbilityTC Ability(in ButtonTypes button) => ref _uniqueButtons[(byte)button - 1];
        public ref CooldownC CoolDownC(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public ref ForPlayerE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player];


        internal UnitEs(in bool b) : this()
        {
            _uniqueButtons = new AbilityTC[(byte)ButtonTypes.End - 1];
            _abilities = new CooldownC[(byte)AbilityTypes.End - 1];
            _visibles = new ForPlayerE[(byte)PlayerTypes.End];
            _needStepsForShift = new StepsC[StartValues.CELLS];

            SimpleAttack = new IdxsCellsC(new HashSet<byte>());
            UniqueAttack = new IdxsCellsC(new HashSet<byte>());

            ForArson = new IdxsCellsC(new HashSet<byte>());
            ForShift = new IdxsCellsC(new HashSet<byte>());
        }
    }
}