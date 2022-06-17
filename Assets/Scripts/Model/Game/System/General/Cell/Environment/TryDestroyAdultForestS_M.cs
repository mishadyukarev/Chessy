namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryDestroyAdultForest(in byte cellIdx)
        {
            if (_eMG.AdultForestC(cellIdx).HaveAnyResources)
            {
                _eMG.AdultForestC(cellIdx).Resources = 0;

                TrySeedNewYoungForestOnCell(cellIdx);
                TryDestroyAllTrailsOnCell(cellIdx);
            }
        }
    }
}