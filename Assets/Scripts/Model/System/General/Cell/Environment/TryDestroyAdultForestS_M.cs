namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryDestroyAdultForest(in byte cellIdx)
        {
            if (_e.AdultForestC(cellIdx).HaveAnyResources)
            {
                _e.AdultForestC(cellIdx).Resources = 0;

                TrySeedNewYoungForestOnCell(cellIdx);
                TryDestroyAllTrailsOnCell(cellIdx);
            }
        }
    }
}