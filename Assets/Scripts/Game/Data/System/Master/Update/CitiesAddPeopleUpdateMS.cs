namespace Chessy.Game
{
    sealed class CitiesAddPeopleUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal CitiesAddPeopleUpdateMS(in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            if(E.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    E.PlayerInfoE(playerT).PeopleInCity += 1;
                }
            }


            //for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            //{
            //    E.PlayerE(playerT).PeopleInCity 
            //        += E.PlayerE(playerT).MaxAvailablePawns 
            //        * E.PlayerE(playerT).ResourcesC(ResourceTypes.Food).Resources 
            //        / Update_VALUES.FROM_MAX_AVAILABLE_PAWNS;

            //    if (E.PlayerE(playerT).PeopleInCity > E.PlayerE(playerT).MaxPeopleInCity)
            //    {
            //        E.PlayerE(playerT).PeopleInCity = E.PlayerE(playerT).MaxPeopleInCity;
            //    }
            //}
        }
    }
}