using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackBuildingS : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        internal AttackBuildingS(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        internal void Attack(in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (_cellEs.BuildEs.MainE.BuildingTC.HaveBuilding)
            {
                _cellEs.BuildEs.MainE.HealthC.Health -= damage;

                if (!_cellEs.BuildEs.MainE.HealthC.IsAlive)
                {
                    if (_cellEs.BuildEs.MainE.BuildingTC.Is(BuildingTypes.City))
                    {
                        eMGame.WinnerC.Player = eMGame.NextPlayer(whoKiller).Player;
                    }

                    else if (_cellEs.BuildEs.MainE.BuildingTC.Is(BuildingTypes.Farm))
                    {
                        _cellEs.EnvironmentEs.FertilizeC.Resources = 0;
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).MaxAvailablePawns--;
                    //}


                    _cellEs.BuildEs.MainE.BuildingTC.Building = BuildingTypes.None;
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}