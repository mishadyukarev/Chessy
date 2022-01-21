﻿using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitStepEs
    {
        static Entity[] _hps;

        public static ref AmountC Steps(in byte idx) => ref _hps[idx].Get<AmountC>();



        public static int MaxAmountSteps(in byte idx) => StepUnitValues.MaxAmountSteps(CellUnitEs.Unit(idx).Unit, CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx).Have/*, UnitUpgC.Steps(Unit, Level, Owner)*/);
        public static bool HaveMaxSteps(in byte idx) => Steps(idx).Amount >= MaxAmountSteps(idx);
        public static int StepsForDoing(in byte idx_from, in byte idx_to)
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
        public static bool HaveStepsForDoing(in byte idx_from, in byte idx_to) => Steps(idx_from).Amount >= StepsForDoing(idx_from, idx_to);

        public static int NeedSteps(in UniqueAbilityTypes uniq)
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

        public static bool Have(in byte idx, in UniqueAbilityTypes uniq) => Steps(idx).Amount >= NeedSteps(uniq);
        public static bool HaveForBuilding(in byte idx, in BuildingTypes build) => Steps(idx).Amount >= NeedSteps(build);
        public static bool HaveMin(in byte idx) => Steps(idx).Amount >= 1;

        public CellUnitStepEs(in EcsWorld gameW)
        {
            _hps = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _hps.Length; idx++)
            {
                _hps[idx] = gameW.NewEntity()
                    .Add(new AmountC(idx));
            }
        }


        public static void SetMaxSteps(in byte idx) => Steps(idx).Amount = StepUnitValues.MaxAmountSteps(CellUnitEs.Unit(idx).Unit, false);

        public static void TakeStepsForDoing(in byte idx_from, in byte idx_to) => Steps(idx_from).Take(StepsForDoing(idx_from, idx_to));
        public static void TakeForBuild(in byte idx) => Steps(idx).Take();
        public static void Take(in byte idx, in UniqueAbilityTypes uniq) => Steps(idx).Take(NeedSteps(uniq));
        public static void Take(in byte idx, in BuildingTypes build) => Steps(idx).Take(NeedSteps(build));
        public static void TakeMin(in byte idx) => Steps(idx).Take();
        public static int NeedSteps(BuildingTypes build)
        {
            return 1;
        }
    }
}
