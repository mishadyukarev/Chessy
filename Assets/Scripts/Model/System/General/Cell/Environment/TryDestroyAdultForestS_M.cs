namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryDestroyAdultForest(in byte cellIdx)
        {
            if (environmentCs[cellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
            {
                environmentCs[cellIdx].Set(EnvironmentTypes.AdultForest, 0);

                TrySeedNewYoungForestOnCell(cellIdx);
                TryDestroyAllTrailsOnCell(cellIdx);
            }
        }
    }
}