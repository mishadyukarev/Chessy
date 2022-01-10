namespace Game.Game
{
    public sealed class CreateUnitMastSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);


            var playerSend = WhoseMoveC.WhoseMove;


            if (WhereBuildsC.IsSetted(BuildTypes.City, playerSend))
            {
                if (InvResC.CanCreateUnit(playerSend, unit, out var needRes))
                {
                    InvResC.BuyCreateUnit(playerSend, unit);
                    InvUnitsC.Add(unit, LevelTypes.First, playerSend);

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
                    EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}