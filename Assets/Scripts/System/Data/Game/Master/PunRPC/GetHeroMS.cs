namespace Game.Game
{
    struct GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntityMPool.ForGetHero.Unit;

            var whoseMove = WhoseMoveE.WhoseMove.Player;

            InventorUnitsE.Units(unit, LevelTypes.First, whoseMove)++;
            AvailableCenterHeroEs.HaveAvailHero(whoseMove).Have = false;
        }
    }
}