namespace Chessy.Game.Model.Entity
{
    public struct HealthTrailC
    {
        readonly float[] _healths;

        internal float[] Healths => (float[])_healths.Clone();
        public ref float Health(in DirectTypes dir) => ref _healths[(byte)dir - 1];
        public bool IsAlive(in DirectTypes dirT) => Health(dirT) > 0;

        internal HealthTrailC(in float[] health) => _healths = health;

        internal void Sync(in float[] healths)
        {
            for (var i = 0; i < healths.Length; i++) _healths[i] = healths[i];
        }
    }
}