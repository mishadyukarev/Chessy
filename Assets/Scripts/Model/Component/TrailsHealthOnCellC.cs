namespace Chessy.Model
{
    public struct TrailsHealthOnCellC
    {
        internal readonly float[] Healths;

        internal float[] HealthsCopy => (float[])Healths.Clone();
        public ref float Health(in DirectTypes dir) => ref Healths[(byte)dir];
        public bool IsAlive(in DirectTypes dirT) => Health(dirT) > 0;
        public bool HaveAnyTrail
        {
            get
            {
                for (var i = 0; i < Healths.Length; i++)
                {
                    if (Healths[i] > 0) return true;
                }
                return false;
            }
        }

        internal TrailsHealthOnCellC(in float[] health) => Healths = health;

        internal void Set(in DirectTypes dirT, in float health) => Healths[(byte)dirT] = health;

        internal void Dispose()
        {
            for (var i = 0; i < Healths.Length; i++) Healths[i] = 0;

        }
    }
}