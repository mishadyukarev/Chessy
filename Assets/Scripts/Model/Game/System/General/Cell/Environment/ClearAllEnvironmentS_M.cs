namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void ClearAllEnvironment(in byte cellIdx)
        {
            _e.FertilizeC(cellIdx).Resources = 0;
            _e.YoungForestC(cellIdx).Resources = 0;
            _e.AdultForestC(cellIdx).Resources = 0;
            _e.HillC(cellIdx).Resources = 0;
            _e.MountainC(cellIdx).Resources = 0;
        }
    }
}