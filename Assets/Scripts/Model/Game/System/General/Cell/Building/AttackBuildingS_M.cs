using Chessy.Game.Extensions;
using System;

namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void Attack(in byte cellIdxForAttack, in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (_e.HaveBuildingOnCell(cellIdxForAttack))
            {
                _e.BuildingHpC(cellIdxForAttack).Health -= damage;

                if (!_e.BuildingHpC(cellIdxForAttack).IsAlive())
                {
                    if (_e.BuildingOnCellT(cellIdxForAttack).Is(BuildingTypes.City))
                    {
                        _e.WinnerPlayerT = whoKiller.NextPlayer();
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).PawnInfoE.MaxAvailablePawns--;
                    //}


                    _e.SetBuildingOnCellT(cellIdxForAttack, BuildingTypes.None);
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}