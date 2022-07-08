namespace Chessy.Model
{
    public struct ResourcesInInventoryC
    {
        readonly float[] _resources;

        public float Resources(in ResourceTypes resourceT) => _resources[(byte)resourceT];

        internal ResourcesInInventoryC(in float[] resources) => _resources = resources;

        internal void Set(in ResourceTypes resourceT, in float resources) => _resources[(byte)resourceT] = resources;
        internal void Subtract(in ResourceTypes resourceT, in float subtraction) => _resources[(byte)resourceT] -= subtraction;
        internal void Add(in ResourceTypes resourceT, in float adding) => _resources[(byte)resourceT] += adding;
        internal void Dispose()
        {
            for (var i = 0; i < _resources.Length; i++) _resources[i] = 0;
        }
    }
}