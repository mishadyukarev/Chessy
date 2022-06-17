using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void Clear(in byte cell_0)
        {
            _eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;
        }
    }
}