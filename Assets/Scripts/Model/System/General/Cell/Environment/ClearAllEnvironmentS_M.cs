namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void ClearAllEnvironment(in byte cellIdx)
        {
            _e.WaterOnCellC(cellIdx).Resources = 0;
            _e.YoungForestC(cellIdx).Resources = 0;
            _e.AdultForestC(cellIdx).Resources = 0;
            _e.HillC(cellIdx).Resources = 0;
            _e.MountainC(cellIdx).Resources = 0;
        }
    }
}