using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct StepUnitC : IUnitCell
    {
        readonly byte _idx;


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
        int UpgradeSteps => UnitUpgC.Steps(Unit, Level, Owner);
        bool HaveEffect(UnitStatTypes stat) => Unit<EffectsC>(_idx).Have(stat);
        EnvC EnvC => Environment<EnvC>(_idx);
        TrailC TrailC => Trail<TrailC>(_idx);

        public int MaxAmountSteps => UnitValues.MaxAmountSteps(Unit, HaveEffect(UnitStatTypes.Steps), UpgradeSteps);
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public int StepsForDoing(DirectTypes dir_cur)
        {
            var needSteps = 1;

            if (EnvC.Have(EnvTypes.AdultForest))
            {
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.AdultForest);
                if (TrailC.Have(dir_cur.Invert())) needSteps -= 1;
            }

            if (EnvC.Have(EnvTypes.Hill))
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public bool HaveStepsForDoing(DirectTypes dir_cur) => Steps >= StepsForDoing(dir_cur);

        public int NeedSteps(UniqueAbilTypes uniq)
        {
            return 0;

            switch (uniq)
            {
                case UniqueAbilTypes.CircularAttack:
                    break;

                case UniqueAbilTypes.BonusNear:
                    break;

                case UniqueAbilTypes.FirePawn:
                    break;

                case UniqueAbilTypes.PutOutFirePawn:
                    break;
                case UniqueAbilTypes.Seed:
                    break;
                case UniqueAbilTypes.FireArcher:
                    break;
                case UniqueAbilTypes.ChangeCornerArcher:
                    break;
                case UniqueAbilTypes.GrowAdultForest:
                    break;
                case UniqueAbilTypes.StunElfemale:
                    break;
                case UniqueAbilTypes.ChangeDirWind:
                    break;
                case UniqueAbilTypes.End:
                    break;
                default:
                    break;
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

        internal StepUnitC(in byte idx) => _idx = idx;


        public void SetMaxSteps() => Unit<StepC>(_idx).Set(MaxAmountSteps);
        public void Reset() => Unit<StepC>(_idx).Reset();

        public void TakeStepsForDoing(DirectTypes dir_cur) => Unit<StepC>(_idx).Take(StepsForDoing(dir_cur));
        public void TakeForBuild() => Unit<StepC>(_idx).Take();
        public void Take(UniqueAbilTypes uniq) => Unit<StepC>(_idx).Take(NeedSteps(uniq));
        public void Take(BuildTypes build) => Unit<StepC>(_idx).Take(NeedSteps(build));
        public void TakeMin() => Unit<StepC>(_idx).Take(Min);

        public void Sync(in int steps) => Unit<StepC>(_idx).Steps = steps;
    }
}