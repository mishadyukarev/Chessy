namespace Game.Game
{
    sealed class CreateUnitMS : SystemAbstract, IEcsRunSystem
    {
        public CreateUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = Es.MasterEs.CreateUnit<UnitTC>().Unit;


            var playerSend = Es.WhoseMove.WhoseMove.Player;


            if (Es.WhereBuildingEs.TryGetBuilding(BuildingTypes.City, playerSend, out var idx_city))
            {
                if (Es.InventorResourcesEs.CanCreateUnit(playerSend, unit, out var needRes))
                {
                    Es.InventorResourcesEs.BuyCreateUnit(playerSend, unit);
                    Es.InventorUnitsEs.Units(unit, LevelTypes.First, playerSend).AddUnit();

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    Es.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
                    Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                Es.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}