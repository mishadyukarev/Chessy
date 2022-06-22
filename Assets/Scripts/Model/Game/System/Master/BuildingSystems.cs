using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    sealed partial class BuildingSystems : SystemModel
    {
        internal BuildingSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }
    }
}