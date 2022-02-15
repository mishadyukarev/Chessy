namespace Game.Game
{
    public sealed class CellUnitStepsC : StepsC
    {
        public bool HaveForAbility(in AbilityTypes ability) => Steps >= CellUnitStatStepValues.NeedForAbility(ability);

    }
}