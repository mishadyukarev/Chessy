namespace Game.Game
{
    public struct StepC
    {
        public float Steps;

        public bool HaveSteps => Steps > 0;

        public void Add(in float adding = 0.1f) => Steps += adding;
        public void Take(in float taking = 0.1f) => Steps -= taking;
    }
}