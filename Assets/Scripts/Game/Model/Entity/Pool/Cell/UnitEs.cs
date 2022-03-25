using Chessy.Common;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Entity.Model.Cell.Unit
{
    public sealed class UnitEs
    {
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
        public readonly CellUnitMainToolWeaponE MainToolWeaponE = new CellUnitMainToolWeaponE();
        public readonly ExtraToolWeaponE ExtraToolWeaponE = new ExtraToolWeaponE();
        public readonly WhoLastDiedHereE WhoLastDiedHereE = new WhoLastDiedHereE();
        public readonly CellUnitExtractE ExtractE = new CellUnitExtractE();


        public ref StepsC NeedSteps(in byte idx_cell) => ref _needStepsForShift[idx_cell];

        public ref AbilityTC Ability(in ButtonTypes button) => ref _uniqueButtons[(byte)button - 1];
        public ref CooldownC CoolDownC(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public ref ForPlayerE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player];

        public void Set(in UnitEs unitE)
        {
            MainE.UnitTC = unitE.MainE.UnitTC;
            MainE.LevelTC = unitE.MainE.LevelTC;
            MainE.PlayerTC = unitE.MainE.PlayerTC;
            MainE.ConditionTC = unitE.MainE.ConditionTC;
            MainE.IsRightArcherC = unitE.MainE.IsRightArcherC;

            EffectsE.StunC = unitE.EffectsE.StunC;
            EffectsE.ShieldEffectC = unitE.EffectsE.ShieldEffectC;
            EffectsE.FrozenArrawC = unitE.EffectsE.FrozenArrawC;
            EffectsE.HaveKingEffect = unitE.EffectsE.HaveKingEffect;

            StatsE.HealthC = unitE.StatsE.HealthC;
            StatsE.StepC = unitE.StatsE.StepC;
            StatsE.WaterC = unitE.StatsE.WaterC;

            MainToolWeaponE.ToolWeaponTC = unitE.MainToolWeaponE.ToolWeaponTC;
            MainToolWeaponE.LevelTC = unitE.MainToolWeaponE.LevelTC;

            ExtraToolWeaponE.ToolWeaponTC = unitE.ExtraToolWeaponE.ToolWeaponTC;
            ExtraToolWeaponE.LevelTC = unitE.ExtraToolWeaponE.LevelTC;
            ExtraToolWeaponE.ProtectionC = unitE.ExtraToolWeaponE.ProtectionC;

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