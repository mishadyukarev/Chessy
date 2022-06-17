using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        readonly EntitiesModelGame _eMG;
        readonly SystemsModelGame _sMG;

        internal SystemsModelGameForUI(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            _eMG = eMG;
            _sMG = sMG;
        }
    }
}