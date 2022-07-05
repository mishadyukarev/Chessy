namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryDestroyAdultForest(in byte cellIdx)
        {
            if (_e.AdultForestC(cellIdx).HaveAnyResources)
            {
                _e.AdultForestC(cellIdx).Resources = 0;

                TrySeedNewYoungForestOnCell(cellIdx);
                _e.TryDestroyAllTrailsOnCell(cellIdx);
            }
        }
    }
}