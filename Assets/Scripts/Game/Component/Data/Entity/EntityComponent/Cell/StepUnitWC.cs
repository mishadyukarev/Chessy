using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct StepUnitWC : IUnitCell
    {
        readonly byte _idx;
        readonly StepUnitValues _values;

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

        public int MaxAmountSteps => _values.MaxAmountSteps(Unit, Unit<EffectsC>(_idx).Have(UnitStatTypes.Steps), UnitUpgC.Steps(Unit, Level, Owner));
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public int StepsForDoing(in byte idx_to)
        {
            var idx_from = _idx;

            var needSteps = 1;

            if (Environment<EnvC>(idx_to).Have(EnvTypes.AdultForest))
            {
                needSteps += _values.NeedAmountSteps(EnvTypes.AdultForest);
                if (Trail<TrailC>(idx_to).Have(CellSpaceC.GetDirect(idx_from, idx_to).Invert())) needSteps -= 1;
            }

            if (Environment<EnvC>(idx_to).Have(EnvTypes.Hill))
                needSteps += _values.NeedAmountSteps(EnvTypes.Hill);

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
            return MIN;
        }

        public bool Have(UniqueAbilTypes uniq) => Steps >= NeedSteps(uniq);
        public bool Have(BuildTypes build) => Steps >= NeedSteps(build);
        public bool HaveMin => Steps >= MIN;

        internal StepUnitWC(in byte idx, in StepUnitValues stepValues)
        {
            _idx = idx;
            _values = stepValues;
        }

        public void SetMaxSteps() => Unit<StepC>(_idx).Set(MaxAmountSteps);
        public void Reset() => Unit<StepC>(_idx).Reset();

        public void TakeStepsForDoing(in byte idx_to) => Unit<StepC>(_idx).Take(StepsForDoing(idx_to));
        public void TakeForBuild() => Unit<StepC>(_idx).Take();
        public void Take(UniqueAbilTypes uniq) => Unit<StepC>(_idx).Take(NeedSteps(uniq));
        public void Take(BuildTypes build) => Unit<StepC>(_idx).Take(NeedSteps(build));
        public void TakeMin() => Unit<StepC>(_idx).Take(MIN);

        public void Sync(in int steps) => Unit<StepC>(_idx).Steps = steps;
    }
}