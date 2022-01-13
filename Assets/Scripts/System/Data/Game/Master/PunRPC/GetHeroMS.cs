namespace Game.Game
{
    struct GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);

            //EntInventorUnits.Units<AmountC>(unit, LevelTypes.First, WhoseMoveC.WhoseMove).Amount += 1;
        }
    }
}