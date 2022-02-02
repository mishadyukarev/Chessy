using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitStatStepE : CellEntityAbstract
    {
        ref AmountC StepsRef => ref Ent.Get<AmountC>();
        public AmountC Steps => Ent.Get<AmountC>();

        public bool HaveSteps => Steps.Amount > 0;
        public bool Have(in AbilityTypes ability) => StepsRef.Amount >= CellUnitStatStepValues.NeedSteps(ability);
        public bool Have(in ConditionUnitTypes cond) => StepsRef.Amount >= CellUnitStatStepValues.NeedSteps(cond);
        public bool Have(in ToolWeaponTypes tw) => StepsRef.Amount >= CellUnitStatStepValues.NeedSteps(tw);
        public bool HaveMax(in CellUnitMainE unitElseE) => Steps.Amount >= MaxAmountSteps(unitElseE);
        public int MaxAmountSteps(in CellUnitMainE cellUnitElse) => CellUnitStatStepValues.MaxAmountSteps(cellUnitElse.UnitTC.Unit, false); 

        internal CellUnitStatStepE(in byte idx, in EcsWorld gameW) : base(idx, gameW) { }

        public void SetMax(in CellUnitMainE unitElseE_from) => StepsRef.Amount = MaxAmountSteps(unitElseE_from);
        public void Shift(in CellUnitStatStepE stepE_from)
        {
            StepsRef = stepE_from.Steps;
            stepE_from.StepsRef.Amount = 0;
        }
        public void SetStepsAfterAttack()
        {
            StepsRef.Amount = 0;
        }
        public void Take(in AbilityTypes ability)
        {
            StepsRef.Amount -= CellUnitStatStepValues.NeedSteps(ability);
        }
        public void Take(in RpcMasterTypes rpc)
        {
            StepsRef.Amount -= CellUnitStatStepValues.NeedSteps(rpc);
        }
        public void Take(in ConditionUnitTypes cond)
        {
            StepsRef.Amount -= CellUnitStatStepValues.NeedSteps(cond);
        }
        public void Take(in ToolWeaponTypes tw)
        {
            StepsRef.Amount -= CellUnitStatStepValues.NeedSteps(tw);
        }
        public void TakeForShift(in byte idx_to, in Entities es)
        {
            if (!es.CellWorker.TryGetDirect(Idx, idx_to, out var dir)) throw new Exception();
            StepsRef.Amount -= es.UnitEs(idx_to).MainE.StepsForShiftOrAttack(dir, es.EnvironmentEs(idx_to), es.TrailEs(idx_to));
        }
    }
}
