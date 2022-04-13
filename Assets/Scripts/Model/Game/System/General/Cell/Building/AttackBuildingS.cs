using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class AttackBuildingS : SystemModel
    {
        internal AttackBuildingS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Attack(in byte cell_0, in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (eMG.BuildingTC(cell_0).HaveBuilding)
            {
                eMG.BuildingHpC(cell_0).Health -= damage;

                if (!eMG.BuildingHpC(cell_0).IsAlive)
                {
                    if (eMG.BuildingTC(cell_0).Is(BuildingTypes.City))
                    {
                        eMG.WinnerPlayerTC.PlayerT = whoKiller.NextPlayer();
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).PawnInfoE.MaxAvailablePawns--;
                    //}


                    eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}