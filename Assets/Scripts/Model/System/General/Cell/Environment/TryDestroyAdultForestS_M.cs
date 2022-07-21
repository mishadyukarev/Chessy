namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryDestroyAdultForest(in byte cellIdx)
        {
            if (_environmentCs[cellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                _environmentCs[cellIdx].Set(EnvironmentTypes.AdultForest, 0);

                TrySeedNewYoungForestOnCell(cellIdx);
                _e.TryDestroyAllTrailsOnCell(cellIdx);
            }
        }
    }
}