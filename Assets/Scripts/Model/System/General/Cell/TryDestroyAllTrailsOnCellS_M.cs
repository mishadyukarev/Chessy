namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryDestroyAllTrailsOnCell(in byte cellIdx)
        {
            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                if (TrailHealthC(cellIdx).IsAlive(dirT))
                {
                    TrailHealthC(cellIdx).Health(dirT) = 0;
                }
            }
        }
    }
}