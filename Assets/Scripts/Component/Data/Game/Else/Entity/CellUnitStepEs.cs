using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitStepEs
    {
        static Entity[] _hps;
        static StepUnitValues _values;

        public static ref C Steps<C>(in byte idx) where C : struct, ICellUnitStepE => ref _hps[idx].Get<C>();



        public static int MaxAmountSteps(in byte idx) => _values.MaxAmountSteps(CellUnitEs.Unit<UnitTC>(idx).Unit, CellUnitEs.Unit<HaveEffectC>(UnitStatTypes.Steps, idx).Have/*, UnitUpgC.Steps(Unit, Level, Owner)*/);
        public static bool HaveMaxSteps(in byte idx) => Steps<AmountC>(idx).Amount >= MaxAmountSteps(idx);
        public static int StepsForDoing(in byte idx_from, in byte idx_to)
        {
            var needSteps = 1;

            if (CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_to).Have)
            {
                needSteps += _values.NeedAmountSteps(EnvTypes.AdultForest);
                if (CellTrailEs.Trail<TrailCellEC>(idx_to).Have(CellSpaceC.GetDirect(idx_from, idx_to).Invert())) needSteps -= 1;
            }

            if (CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_to).Have)
                needSteps += _values.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public static bool HaveStepsForDoing(in byte idx_from, in byte idx_to) => Steps<AmountC>(idx_from).Amount >= StepsForDoing(idx_from, idx_to);

        public static int NeedSteps(in UniqueAbilityTypes uniq)
        {
            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: return 1;
                case UniqueAbilityTypes.BonusNear: return 1;
                case UniqueAbilityTypes.FirePawn: return 1;
                case UniqueAbilityTypes.PutOutFirePawn: return 1;
                case UniqueAbilityTypes.Seed: return 1;
                case UniqueAbilityTypes.FireArcher: return 1;
                case UniqueAbilityTypes.ChangeCornerArcher: return 1;
                case UniqueAbilityTypes.GrowAdultForest: return 1;
                case UniqueAbilityTypes.StunElfemale: return 1;
                case UniqueAbilityTypes.ChangeDirWind: return 1;
                default: throw new Exception();
            }
        }

        public static bool Have(in byte idx, in UniqueAbilityTypes uniq) => Steps<AmountC>(idx).Amount >= NeedSteps(uniq);
        public static bool Have(in byte idx, in BuildingTypes build) => Steps<AmountC>(idx).Amount >= NeedSteps(build);
        public static bool HaveMin(in byte idx) => Steps<AmountC>(idx).Amount >= 1;


        public CellUnitStepEs(in EcsWorld gameW)
        {
            _hps = new Entity[CellValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _hps.Length; idx++)
            {
                _hps[idx] = gameW.NewEntity()
                    .Add(new AmountC(idx));
            }

            _values = new StepUnitValues();
        }


        public static void SetMaxSteps(in byte idx) => Steps<AmountC>(idx).Amount = _values.MaxAmountSteps(CellUnitEs.Unit<UnitTC>(idx).Unit, false);

        public static void TakeStepsForDoing(in byte idx_from, in byte idx_to) => Steps<AmountC>(idx_from).Take(StepsForDoing(idx_from, idx_to));
        public static void TakeForBuild(in byte idx) => Steps<AmountC>(idx).Take();
        public static void Take(in byte idx, in UniqueAbilityTypes uniq) => Steps<AmountC>(idx).Take(NeedSteps(uniq));
        public static void Take(in byte idx, in BuildingTypes build) => Steps<AmountC>(idx).Take(NeedSteps(build));
        public static void TakeMin(in byte idx) => Steps<AmountC>(idx).Take();
        public static int NeedSteps(BuildingTypes build)
        {
            return 1;
        }
    }

    public interface ICellUnitStepE { }
}
