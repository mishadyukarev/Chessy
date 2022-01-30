namespace Game.Game
{
    sealed class GetHeroMS : SystemAbstract, IEcsRunSystem
    {
        public GetHeroMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = Es.MasterEs.ForGetHero.Unit;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            Es.InventorUnitsEs.Units(unit, LevelTypes.First, whoseMove).Units++;
            Es.AvailableCenterHero(whoseMove).HaveCenterHero.Have = false;
        }
    }
}