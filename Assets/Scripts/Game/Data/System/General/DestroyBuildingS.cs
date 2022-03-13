using System;

namespace Chessy.Game.System.Model
{
    public struct DestroyBuildingS
    {
        public DestroyBuildingS(in float damage, in PlayerTypes whoKiller, in byte idx_0, in EntitiesModel e)
        {
            if (damage <= 0) throw new Exception();

            if (e.BuildingTC(idx_0).HaveBuilding)
            {
                e.BuildHpC(idx_0).Health -= damage;

                if (!e.BuildHpC(idx_0).IsAlive)
                {
                    if (e.BuildingTC(idx_0).Is(BuildingTypes.City))
                    {
                        e.WinnerC.Player = e.NextPlayer(whoKiller).Player;
                    }

                    else if (e.BuildingTC(idx_0).Is(BuildingTypes.Farm))
                    {
                        e.FertilizeC(idx_0).Resources = 0;
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).MaxAvailablePawns--;
                    //}


                    e.BuildingTC(idx_0).Building = BuildingTypes.None;
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}