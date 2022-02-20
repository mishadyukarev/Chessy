namespace Game.Game
{
    sealed class TryGetPeopleUpdateMS : SystemAbstract, IEcsRunSystem
    {
        int _curStep;

        internal TryGetPeopleUpdateMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            _curStep++;

            if(_curStep >= UpdateValues.UPDATES_FOR_GET_UNIT)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    E.PlayerE(playerT).PeopleInCity++;
                }

                _curStep = 0;
            }
        }
    }
}