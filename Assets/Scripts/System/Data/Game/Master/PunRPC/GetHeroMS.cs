namespace Game.Game
{
    struct GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntityMPool.ForGetHero.Unit;

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            InventorUnitsE.Units(unit, LevelTypes.First, whoseMove).Add();
            AvailableCenterHeroEs.HaveAvailHero<HaveAvailableHeroC>(whoseMove).Have = false;
        }
    }
}