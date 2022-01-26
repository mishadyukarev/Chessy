namespace Game.Game
{
    struct CreateUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntitiesMaster.CreateUnit<UnitTC>().Unit;


            var playerSend = Entities.WhoseMoveE.WhoseMove.Player;


            if (WhereBuildsE.IsSetted(BuildingTypes.City, playerSend, out var idx_city))
            {
                if (InventorResourcesE.CanCreateUnit(playerSend, unit, out var needRes))
                {
                    InventorResourcesE.BuyCreateUnit(playerSend, unit);
                    InventorUnitsE.Units(unit, LevelTypes.First, playerSend)++;

                    Entities.Rpc.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    Entities.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
                    Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                Entities.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}