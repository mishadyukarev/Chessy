namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void ClearAllEnvironment(in byte cellIdx)
        {
            _eMG.FertilizeC(cellIdx).Resources = 0;
            _eMG.YoungForestC(cellIdx).Resources = 0;
            _eMG.AdultForestC(cellIdx).Resources = 0;
            _eMG.HillC(cellIdx).Resources = 0;
            _eMG.MountainC(cellIdx).Resources = 0;
        }
    }
}