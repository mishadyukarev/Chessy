namespace Game.Game
{
    struct GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntitiesMaster.ForGetHero.Unit;

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            InventorUnitsE.Units(unit, LevelTypes.First, whoseMove)++;
            AvailableCenterHeroEs.HaveAvailHero(whoseMove).Have = false;
        }
    }
}