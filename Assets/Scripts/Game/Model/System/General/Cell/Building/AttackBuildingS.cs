using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackBuildingS : SystemModelGameAbs
    {
        internal AttackBuildingS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Attack(in byte cell_0, in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (e.BuildingTC(cell_0).HaveBuilding)
            {
                e.BuildingHpC(cell_0).Health -= damage;

                if (!e.BuildingHpC(cell_0).IsAlive)
                {
                    if (e.BuildingTC(cell_0).Is(BuildingTypes.City))
                    {
                        e.WinnerC.Player = e.NextPlayer(whoKiller).Player;
                    }

                    else if (e.BuildingTC(cell_0).Is(BuildingTypes.Farm))
                    {
                        e.FertilizeC(cell_0).Resources = 0;
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).MaxAvailablePawns--;
                    //}


                    e.BuildingTC(cell_0).Building = BuildingTypes.None;
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}