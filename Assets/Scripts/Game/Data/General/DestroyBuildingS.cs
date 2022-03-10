namespace Chessy.Game.System.Model
{
    sealed class DestroyBuildingS : CellSystem, IEcsRunSystem
    {
        internal DestroyBuildingS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            var damage = E.BuildingMainE(Idx).AttackBuildingC.Damage;

            if (damage > 0)
            {
                if (E.BuildingTC(Idx).HaveBuilding)
                {
                    E.BuildHpC(Idx).Health -= damage;

                    if (!E.BuildHpC(Idx).IsAlive)
                    {
                        if (E.BuildingTC(Idx).Is(BuildingTypes.City))
                        {
                            E.WinnerC.Player = E.NextPlayer(E.BuildingMainE(Idx).KillerC.Player).Player;
                        }

                        else if (E.BuildingTC(Idx).Is(BuildingTypes.Farm))
                        {
                            E.FertilizeC(Idx).Resources = 0;
                        }

                        //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                        //{
                        //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).MaxAvailablePawns--;
                        //}


                        E.BuildingTC(Idx).Building = BuildingTypes.None;
                    }




                    E.BuildingMainE(Idx).AttackBuildingC.Damage = 0;
                }
                else
                {
                    throw new global::System.Exception();
                }
            }
        }
    }
}