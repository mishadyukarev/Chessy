namespace Chessy.Game
{
    sealed class TryGetPeopleUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal TryGetPeopleUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                E.PlayerE(playerT).PeopleInCity += E.PlayerE(playerT).MaxAvailablePawns / Update_Values.FROM_MAX_AVAILABLE_PAWNS;
            }
        }
    }
}