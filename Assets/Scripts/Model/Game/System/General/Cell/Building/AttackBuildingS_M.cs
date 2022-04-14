using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class AttackBuildingS_M : SystemModel
    {
        internal AttackBuildingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Attack(in byte cell, in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (eMG.BuildingTC(cell).HaveBuilding)
            {
                eMG.BuildingHpC(cell).Health -= damage;

                if (!eMG.BuildingHpC(cell).IsAlive)
                {
                    if (eMG.BuildingTC(cell).Is(BuildingTypes.City))
                    {
                        eMG.WinnerPlayerT = whoKiller.NextPlayer();
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).PawnInfoE.MaxAvailablePawns--;
                    //}


                    eMG.BuildingTC(cell).BuildingT = BuildingTypes.None;
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}