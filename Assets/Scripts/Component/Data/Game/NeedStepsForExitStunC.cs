namespace Game.Game
{
    public struct NeedStepsForExitStunC : IUnitCellE
    {
        public int Steps;

        public bool IsStunned => Steps > 0;
    }
}