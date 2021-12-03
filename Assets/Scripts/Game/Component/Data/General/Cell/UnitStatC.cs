using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct UnitStatC : IUnitStatCell
    {
        readonly byte _idx;

        public bool NeedWater => UnitStat<WaterC>(_idx).Water <= 100 * 0.4f;
        public int MaxWater(float upgPerc) => (int)(100 + 100 * upgPerc);
        public bool HaveMaxWater(float upgPerc) => UnitStat<WaterC>(_idx).Water >= MaxWater(upgPerc);

        public bool HaveMaxSteps(UnitTypes unit, bool haveEff, float upgPerc) => UnitStat<StepC>(_idx).Steps >= UnitValues.MaxAmountSteps(unit, haveEff, upgPerc);
        public int StepsForDoing(EnvC cellEnvC, DirectTypes dir_cur, TrailC trailC)
        {
            var needSteps = 1;

            if (cellEnvC.Have(EnvTypes.AdultForest))
            {
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.AdultForest);
                if (trailC.Have(dir_cur.Invert())) needSteps -= 1;
            }

            if (cellEnvC.Have(EnvTypes.Hill))
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public bool HaveStepsForDoing(EnvC cellEnvC, DirectTypes dir_cur, TrailC trailC) => UnitStat<StepC>(_idx).Steps >= StepsForDoing(cellEnvC, dir_cur, trailC);


        internal UnitStatC(in byte idx) : this()
        {
            _idx = idx;
        }


        public void SetMaxWater(float upgPerc) => UnitStat<WaterC>(_idx).Set(MaxWater(upgPerc));
        public void ExecuteThirsty(UnitTypes unitType)
        {
            float percent = 0;
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: percent = 0.4f; break;
                case UnitTypes.Pawn: percent = 0.5f; break;
                case UnitTypes.Archer: percent = 0.5f; break;
                case UnitTypes.Scout: percent = 0.5f; break;
                case UnitTypes.Elfemale: percent = 0.5f; break;
                default: throw new Exception();
            }

            UnitStat<HpC>(_idx).Take((int)(HpC.MAX_HP * percent));
        }
        public void TakeWater() => UnitStat<WaterC>(_idx).TakeWater((int)(100 * 0.15f));

        public void SetMaxSteps(UnitTypes unit, bool haveEff, float upgPerc)
        {
            UnitStat<StepC>(_idx).Set(UnitValues.MaxAmountSteps(unit, haveEff, upgPerc));
        }
        public void TakeStepsForDoing(EnvC cellEnvC, DirectTypes dir_cur, TrailC trailC)
        {
            UnitStat<StepC>(_idx).Take(StepsForDoing(cellEnvC, dir_cur, trailC));
        }
        public void Sync(in int hp, in int steps, in int water)
        {
            UnitStat<HpC>(_idx).Set(hp);
            UnitStat<StepC>(_idx).Set(steps);
            UnitStat<WaterC>(_idx).Set(water);
        }
    }
}