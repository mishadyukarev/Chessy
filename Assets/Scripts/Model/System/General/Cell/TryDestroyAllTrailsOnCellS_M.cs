namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryDestroyAllTrailsOnCell(in byte cellIdx)
        {
            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                if (hpTrailCs[cellIdx].IsAlive(dirT))
                {
                    hpTrailCs[cellIdx].Health(dirT) = 0;
                }
            }
        }
    }
}