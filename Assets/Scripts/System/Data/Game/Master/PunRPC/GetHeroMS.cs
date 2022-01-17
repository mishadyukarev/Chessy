namespace Game.Game
{
    struct GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntityMPool.GetHero<UnitTC>().Unit;

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            EntInventorUnits.Units<AmountC>(UnitTypes.Elfemale, LevelTypes.First, whoseMove).Add();
            AvailableCenterHeroEs.HaveAvailHero<HaveAvailableHeroC>(whoseMove).Have = false;
        }
    }
}