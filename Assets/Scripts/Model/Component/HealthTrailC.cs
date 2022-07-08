namespace Chessy.Model
{
    public struct HealthTrailC
    {
        readonly float[] _healths;

        internal float[] Healths => (float[])_healths.Clone();
        public ref float Health(in DirectTypes dir) => ref _healths[(byte)dir];
        public bool IsAlive(in DirectTypes dirT) => Health(dirT) > 0;

        internal HealthTrailC(in float[] health) => _healths = health;

        internal void Set(in DirectTypes dirT, in float health) => _healths[(byte)dirT] = health;

        internal void Sync(in float[] healths)
        {
            for (var i = 0; i < healths.Length; i++) _healths[i] = healths[i];
        }

        internal void Dispose()
        {
            for (var i = 0; i < _healths.Length; i++) _healths[i] = 0;

        }
    }
}