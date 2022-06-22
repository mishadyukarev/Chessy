using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game
{
    static partial class BuildingSystems
    {
        internal static void Attack(this EntitiesModelGame e, in byte cellIdxForAttack, in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (e.HaveBuildingOnCell(cellIdxForAttack))
            {
                e.BuildingHpC(cellIdxForAttack).Health -= damage;

                if (!e.BuildingHpC(cellIdxForAttack).IsAlive())
                {
                    if (e.BuildingOnCellT(cellIdxForAttack).Is(BuildingTypes.City))
                    {
                        e.WinnerPlayerT = whoKiller.NextPlayer();
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).PawnInfoE.MaxAvailablePawns--;
                    //}


                    e.SetBuildingOnCellT(cellIdxForAttack, BuildingTypes.None);
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}