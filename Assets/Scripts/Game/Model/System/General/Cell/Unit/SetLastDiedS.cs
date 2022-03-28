using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.System.Model
{
    sealed class SetLastDiedS
    {
        readonly WhoLastDiedHereE _whoLastDiedHereE;
        readonly UnitMainE _unitMainE;

        internal SetLastDiedS(in WhoLastDiedHereE whoLastDiedHereE, in UnitMainE unitMainE)
        {
            _whoLastDiedHereE = whoLastDiedHereE;
            _unitMainE = unitMainE;
        }

        internal void Set()
        {
            _whoLastDiedHereE.UnitTC = _unitMainE.UnitTC;
            _whoLastDiedHereE.LevelTC = _unitMainE.LevelTC;
            _whoLastDiedHereE.PlayerTC = _unitMainE.PlayerTC;
        }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT)
        {
            _whoLastDiedHereE.UnitTC.Unit = unitT;
            _whoLastDiedHereE.LevelTC.Level = levelT;
            _whoLastDiedHereE.PlayerTC.Player = playerT;
        }
    }
}