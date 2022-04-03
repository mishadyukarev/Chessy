namespace Chessy.Game.Model.Entity
{
    public struct MistakeE
    {
        public MistakeTC MistakeTC;
        public TimerC TimerC;

        public MistakeTypes MistakeT
        {
            get => MistakeTC.MistakeT;
            set => MistakeTC.MistakeT = value;
        }

        public float Timer
        {
            get => TimerC.Timer;
            set => TimerC.Timer = value;
        }
    }
}