using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    sealed class ClearAllEnvironmentS
    {
        readonly CellEnvironmentEs _envE;

        internal ClearAllEnvironmentS(in CellEnvironmentEs envE) { _envE = envE; }

        internal void Clear()
        {
            _envE.FertilizeC.Resources = 0;
            _envE.AdultForestC.Resources = 0;
            _envE.YoungForestC.Resources = 0;
            _envE.HillC.Resources = 0;
            _envE.MountainC.Resources = 0;
        }
    }
}