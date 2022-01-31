using ECS;

namespace Game.Game
{
    public sealed class CellUnitStepE : EntityAbstract
    {
        public ref AmountC Steps => ref Ent.Get<AmountC>();

        public bool HaveMax(in CellUnitMainE unitElseE) => Steps.Amount >= MaxAmountSteps(unitElseE);
        public int MaxAmountSteps(in CellUnitMainE cellUnitElse) => CellUnitStepValues.MaxAmountSteps(cellUnitElse.UnitTC.Unit, false); 

        public CellUnitStepE(in EcsWorld gameW) : base(gameW) { }

        public void SetMax(in CellUnitMainE unitElseE_from) => Steps.Amount = MaxAmountSteps(unitElseE_from);
        public void Shift(in CellUnitStepE stepE_from)
        {
            Steps = stepE_from.Steps;
            stepE_from.Steps.Amount = 0;
        }
    }
}
