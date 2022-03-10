namespace Chessy.Game.System.Model
{
    sealed class DestroyBuildingS : SystemAbstract, IEcsRunSystem
    {
        internal DestroyBuildingS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                var damage = E.BuildingMainE(idx_0).AttackBuildingC.Damage;

                if (damage > 0)
                {
                    if (E.BuildingTC(idx_0).HaveBuilding)
                    {
                        E.BuildHpC(idx_0).Health -= damage;

                        if (!E.BuildHpC(idx_0).IsAlive)
                        {
                            if (E.BuildingTC(idx_0).Is(BuildingTypes.City))
                            {
                                E.WinnerC.Player = E.NextPlayer(E.BuildingMainE(idx_0).KillerC.Player).Player;
                            }

                            else if (E.BuildingTC(idx_0).Is(BuildingTypes.Farm))
                            {
                                E.FertilizeC(idx_0).Resources = 0;
                            }

                            //else if (E.BuildingTC(Idx).Is(BuildingTypes.House))
                            //{
                            //    E.PlayerE(E.BuildingPlayerTC(Idx).Player).MaxAvailablePawns--;
                            //}


                            E.BuildingTC(idx_0).Building = BuildingTypes.None;
                        }




                        E.BuildingMainE(idx_0).AttackBuildingC.Damage = 0;
                    }
                    else
                    {
                        throw new global::System.Exception();
                    }
                }
            }
        }
    }
}