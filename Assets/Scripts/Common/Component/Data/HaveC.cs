namespace Chessy.Game
{
    public class HaveC
    {
        public bool Have;

        public HaveC() { }
        public HaveC(in bool have) => Have = have;
    }
}