namespace Chessy.Model
{
    public sealed class ResourcesInInventoryC
    {
        readonly float[] _resources;

        public ref float Resources(in ResourceTypes resourceT) => ref _resources[(byte)resourceT];

        internal ResourcesInInventoryC()
        {
            _resources = new float[(byte)ResourceTypes.End];
        }
        internal void Set(in ResourceTypes resourceT, in float resources) => _resources[(byte)resourceT] = resources;
        internal void Subtract(in ResourceTypes resourceT, in float subtraction) => _resources[(byte)resourceT] -= subtraction;
        internal void Add(in ResourceTypes resourceT, in float adding) => _resources[(byte)resourceT] += adding;
        internal void Dispose()
        {
            for (var i = 0; i < _resources.Length; i++) _resources[i] = 0;
        }
    }
}