namespace Chessy.Game.Model.Entity
{
    public struct HealthTrailC
    {
        readonly float[] _healths;
        public ref float Health(in DirectTypes dir) => ref _healths[(byte)dir - 1];
        public bool IsAlive(in DirectTypes dirT) => Health(dirT) > 0;

        internal HealthTrailC(in float[] health) => _healths = health;
    }
}