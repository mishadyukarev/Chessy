using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class CellUnitStepE : EntityAbstract
    {
        public ref AmountC Steps => ref Ent.Get<AmountC>();

        public bool HaveMax(in CellUnitElseE unitElseE) => Steps.Amount >= MaxAmountSteps(unitElseE);
        public int MaxAmountSteps(in CellUnitElseE cellUnitElse) => CellUnitStepValues.MaxAmountSteps(cellUnitElse.UnitC.Unit, false);
        public int StepsForShiftOrAttack(in DirectTypes dirMove, in CellEnvironmentE[] envEs, in CellTrailE[] trailsEs)
        {
            var needSteps = 1;

            foreach (var cellEnvE in envEs)
            {
                if (cellEnvE.EnvironmentC.Is(EnvironmentTypes.AdultForest, EnvironmentTypes.Hill))
                {
                    if (cellEnvE.Resources.Have)
                    {
                        needSteps += CellUnitStepValues.NeedAmountSteps(cellEnvE.EnvironmentC.Environment);

                        foreach (var trailE in trailsEs)
                        {
                            if (trailE.Health.Have)
                            {
                                if (trailE.DirectTC.Direct == dirMove.Invert())
                                {
                                    --needSteps;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return needSteps;
        }


        public CellUnitStepE(in EcsWorld gameW) : base(gameW) { }

        public void SetMax(in CellUnitElseE unitElseE) => Steps.Amount = MaxAmountSteps(unitElseE);
    }
}
