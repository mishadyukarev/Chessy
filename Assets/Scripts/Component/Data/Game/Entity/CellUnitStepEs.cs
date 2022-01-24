using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitStepEs : CellAbstractEs
    {
        public ref AmountC Steps(in byte idx) => ref Cells[idx].Get<AmountC>();

        public int MaxAmountSteps(in byte idx) => StepUnitValues.MaxAmountSteps(CellUnitEs.Unit(idx).Unit, CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx).Have/*, UnitUpgC.Steps(Unit, Level, Owner)*/);
        public bool HaveMaxSteps(in byte idx) => Steps(idx).Amount >= MaxAmountSteps(idx);
        public int StepsForDoing(in byte idx_from, in byte idx_to)
        {
            var needSteps = 1;

            if (CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx_to).Have)
            {
                needSteps += StepUnitValues.NeedAmountSteps(EnvironmentTypes.AdultForest);
                if (CellTrailEs.Health(CellSpaceSupport.GetDirect(idx_from, idx_to).Invert(), idx_to).Have) needSteps -= 1;
            }

            if (CellEnvironmentEs.Resources(EnvironmentTypes.Hill, idx_to).Have)
                needSteps += StepUnitValues.NeedAmountSteps(EnvironmentTypes.Hill);

            return needSteps;
        }
        public bool HaveStepsForDoing(in byte idx_from, in byte idx_to) => Steps(idx_from).Amount >= StepsForDoing(idx_from, idx_to);

        public int NeedSteps(in UniqueAbilityTypes uniq)
        {
            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: return 1;
                case UniqueAbilityTypes.BonusNear: return 1;
                case UniqueAbilityTypes.FirePawn: return 1;
                case UniqueAbilityTypes.PutOutFirePawn: return 1;
                case UniqueAbilityTypes.Seed: return 1;
                case UniqueAbilityTypes.FireArcher: return 2;
                case UniqueAbilityTypes.ChangeCornerArcher: return 1;
                case UniqueAbilityTypes.GrowAdultForest: return 1;
                case UniqueAbilityTypes.StunElfemale: return 1;
                case UniqueAbilityTypes.ChangeDirectionWind: return 1;
                case UniqueAbilityTypes.IceWall: return 1;
                default: throw new Exception();
            }
        }

        public bool Have(in byte idx, in UniqueAbilityTypes uniq) => Steps(idx).Amount >= NeedSteps(uniq);
        public bool HaveForBuilding(in byte idx, in BuildingTypes build) => Steps(idx).Amount >= NeedSteps(build);
        public bool HaveMin(in byte idx) => Steps(idx).Amount >= 1;

        public CellUnitStepEs(in EcsWorld gameW) : base(gameW) { }


        public void SetMaxSteps(in byte idx) => Steps(idx).Amount = StepUnitValues.MaxAmountSteps(CellUnitEs.Unit(idx).Unit, false);

        public void TakeStepsForDoing(in byte idx_from, in byte idx_to) => Steps(idx_from).Take(StepsForDoing(idx_from, idx_to));
        public void TakeForBuild(in byte idx) => Steps(idx).Take();
        public void Take(in byte idx, in UniqueAbilityTypes uniq) => Steps(idx).Take(NeedSteps(uniq));
        public void Take(in byte idx, in BuildingTypes build) => Steps(idx).Take(NeedSteps(build));
        public void TakeMin(in byte idx) => Steps(idx).Take();
        public int NeedSteps(BuildingTypes build)
        {
            return 1;
        }
    }
}
