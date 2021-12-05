using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct StepUnitWC : IUnitCell
    {
        readonly byte _idx;

        const int MIN = 1;

        UnitTypes Unit
        {
            get => Unit<UnitC>(_idx).Unit;
            set => Unit<UnitC>(_idx).Unit = value;
        }
        LevelTypes Level
        {
            get => Unit<LevelC>(_idx).Level;
            set => Unit<LevelC>(_idx).Level = value;
        }
        PlayerTypes Owner
        {
            get => Unit<OwnerC>(_idx).Owner;
            set => Unit<OwnerC>(_idx).Owner = value;
        }
        int Steps
        {
            get => Unit<StepC>(_idx).Steps;
            set => Unit<StepC>(_idx).Steps = value;
        }
        EnvC EnvC => Environment<EnvC>(_idx);
        TrailC TrailC => Trail<TrailC>(_idx);

        public int MaxAmountSteps => UnitValues.MaxAmountSteps(Unit, Unit<EffectsC>(_idx).Have(UnitStatTypes.Steps), UnitUpgC.Steps(Unit, Level, Owner));
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public int StepsForDoing(in byte idx_to)
        {
            var needSteps = 1;

            if (EnvC.Have(EnvTypes.AdultForest))
            {
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.AdultForest);
                if (TrailC.Have(CellSpaceC.GetDirect(_idx, idx_to).Invert())) needSteps -= 1;
            }

            if (EnvC.Have(EnvTypes.Hill))
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public bool HaveStepsForDoing(in byte idx_to) => Steps >= StepsForDoing(idx_to);

        public int NeedSteps(UniqueAbilTypes uniq)
        {
            switch (uniq)
            {
                case UniqueAbilTypes.CircularAttack: return MIN;
                case UniqueAbilTypes.BonusNear: return MIN;
                case UniqueAbilTypes.FirePawn: return MIN;
                case UniqueAbilTypes.PutOutFirePawn: return MIN;
                case UniqueAbilTypes.Seed: return MIN;
                case UniqueAbilTypes.FireArcher: return MIN;
                case UniqueAbilTypes.ChangeCornerArcher: return MIN;
                case UniqueAbilTypes.GrowAdultForest: return MIN;
                case UniqueAbilTypes.StunElfemale: return MIN;
                case UniqueAbilTypes.ChangeDirWind: return MIN;
                default: throw new Exception();
            }
        }
        public int NeedSteps(BuildTypes build)
        {
            return 1;
        }
        public int Min => 1;

        public bool Have(UniqueAbilTypes uniq) => Steps >= NeedSteps(uniq);
        public bool Have(BuildTypes build) => Steps >= NeedSteps(build);
        public bool HaveMin => Steps >= Min;

        internal StepUnitWC(in byte idx) => _idx = idx;


        public void SetMaxSteps() => Unit<StepC>(_idx).Set(MaxAmountSteps);
        public void Reset() => Unit<StepC>(_idx).Reset();

        public void TakeStepsForDoing(in byte idx_to) => Unit<StepC>(_idx).Take(StepsForDoing(idx_to));
        public void TakeForBuild() => Unit<StepC>(_idx).Take();
        public void Take(UniqueAbilTypes uniq) => Unit<StepC>(_idx).Take(NeedSteps(uniq));
        public void Take(BuildTypes build) => Unit<StepC>(_idx).Take(NeedSteps(build));
        public void TakeMin() => Unit<StepC>(_idx).Take(Min);

        public void Sync(in int steps) => Unit<StepC>(_idx).Steps = steps;
    }
}