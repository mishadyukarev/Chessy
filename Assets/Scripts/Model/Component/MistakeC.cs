namespace Chessy.Model
{
    public sealed class MistakeC
    {
        public MistakeTypes MistakeT { get; internal set; }
        public float Timer { get; internal set; }

        internal void Dispose()
        {
            MistakeT = default;
            Timer = default;
        }
    }
}