namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryTakeAdultForestResourcesM(in float taking, in byte cellIdx)
        {
            if (_environmentCs[cellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                _environmentCs[cellIdx].ResourcesRef(EnvironmentTypes.AdultForest) -= taking;

                if (!_environmentCs[cellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _environmentCs[cellIdx].Set(EnvironmentTypes.AdultForest, 0);
                    TrySeedNewYoungForestOnCell(cellIdx);
                    _e.TryDestroyAllTrailsOnCell(cellIdx);
                }
            }
        }
    }
}