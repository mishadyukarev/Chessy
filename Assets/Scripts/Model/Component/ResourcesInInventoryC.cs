namespace Chessy.Model
{
    public sealed class ResourcesInInventoryC
    {
        public readonly double[] ResourcesArray;

        internal ref double ResourcesRef(in ResourceTypes resourceT) => ref ResourcesArray[(byte)resourceT];
        public double Resources(in ResourceTypes resourceT) => ResourcesArray[(byte)resourceT];

        internal ResourcesInInventoryC()
        {
            ResourcesArray = new double[(byte)ResourceTypes.End];
        }
        internal void Set(in ResourceTypes resourceT, in double resources) => ResourcesArray[(byte)resourceT] = resources;
        internal void Subtract(in ResourceTypes resourceT, in double subtraction) => ResourcesArray[(byte)resourceT] -= subtraction;
        internal void Add(in ResourceTypes resourceT, in double adding) => ResourcesArray[(byte)resourceT] += adding;
        internal void Dispose()
        {
            for (var i = 0; i < ResourcesArray.Length; i++) ResourcesArray[i] = 0;
        }
    }
}