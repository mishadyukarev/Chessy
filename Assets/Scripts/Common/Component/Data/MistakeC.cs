namespace Chessy.Game.Entity.Model
{
    public struct MistakeC
    {
        public MistakeTypes MistakeT;
        public float Timer;

        public void Set(in MistakeTypes mistakeT, in float timer)
        {
            MistakeT = mistakeT;
            Timer = timer;
        }
    }
}
