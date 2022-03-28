using Chessy.Common;
using Chessy.Game.Model.Entity.Cell.Unit;
using Chessy.Game.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Entity.Model.Cell.Unit
{
    public sealed class UnitEs
    {
        public bool NeedUpdateView;

        readonly AbilityTC[] _uniqueButtons = new AbilityTC[(byte)ButtonTypes.End - 1];
        readonly CooldownC[] _abilities = new CooldownC[(byte)AbilityTypes.End - 1];
        readonly ForPlayerE[] _visibles = new ForPlayerE[(byte)PlayerTypes.End];
        readonly StepsC[] _needStepsForShift = new StepsC[StartValues.CELLS];

        public readonly IdxsCellsC SimpleAttack = new IdxsCellsC(new HashSet<byte>());
        public readonly IdxsCellsC UniqueAttack = new IdxsCellsC(new HashSet<byte>());

        public readonly IdxsCellsC ForArson = new IdxsCellsC(new HashSet<byte>());
        public readonly IdxsCellsC ForShift = new IdxsCellsC(new HashSet<byte>());

        public readonly UnitMainE MainE = new UnitMainE();
        public readonly StatsE StatsE = new StatsE();
        public readonly EffectsE EffectsE = new EffectsE();
        public readonly MainToolWeaponE MainToolWeaponE = new MainToolWeaponE();
        public readonly ExtraToolWeaponE ExtraToolWeaponE = new ExtraToolWeaponE();
        public readonly WhoLastDiedHereE WhoLastDiedHereE = new WhoLastDiedHereE();
        public readonly CellUnitExtractE ExtractE = new CellUnitExtractE();


        public ref StepsC NeedSteps(in byte idx_cell) => ref _needStepsForShift[idx_cell];

        public ref AbilityTC Ability(in ButtonTypes button) => ref _uniqueButtons[(byte)button - 1];
        public ref CooldownC CoolDownC(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public ref ForPlayerE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player];
    }
}