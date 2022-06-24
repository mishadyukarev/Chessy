namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryTakeAdultForestResourcesM(in float taking, in byte cellIdx)
        {
            if (_e.AdultForestC(cellIdx).HaveAnyResources)
            {
                _e.AdultForestC(cellIdx).Resources -= taking;

                if (!_e.AdultForestC(cellIdx).HaveAnyResources)
                {
                    _e.AdultForestC(cellIdx).Resources = 0;
                    TrySeedNewYoungForestOnCell(cellIdx);
                    _e.TryDestroyAllTrailsOnCell(cellIdx);
                }
            }
        }
    }
}