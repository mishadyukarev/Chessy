namespace Game.Game
{
    struct CreateUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntityMPool.CreateUnit<UnitTC>().Unit;


            var playerSend = WhoseMoveE.WhoseMove<PlayerTC>().Player;


            if (WhereBuildsE.IsSetted(BuildingTypes.City, playerSend, out var idx_city))
            {
                if (InventorResourcesE.CanCreateUnit(playerSend, unit, out var needRes))
                {
                    InventorResourcesE.BuyCreateUnit(playerSend, unit);
                    InventorUnitsE.Units(unit, LevelTypes.First, playerSend).Add();

                    EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
                    EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}