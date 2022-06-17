using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void Attack(in byte cell, in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();

            if (_eMG.BuildingTC(cell).HaveBuilding)
            {
                _eMG.BuildingHpC(cell).Health -= damage;

                if (!_eMG.BuildingHpC(cell).IsAlive)
                {
                    if (_eMG.BuildingTC(cell).Is(BuildingTypes.City))
                    {
                        _eMG.WinnerPlayerT = whoKiller.NextPlayer();
                    }

                    //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                    //{
                    //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).PawnInfoE.MaxAvailablePawns--;
                    //}


                    _eMG.BuildingTC(cell).BuildingT = BuildingTypes.None;
                }
            }
            else
            {
                throw new global::System.Exception();
            }
        }
    }
}