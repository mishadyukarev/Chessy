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

                    RpcS.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    RpcS.SoundToGeneral(sender, ClipTypes.Mistake);
                    RpcS.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                RpcS.SoundToGeneral(sender, ClipTypes.Mistake);
                RpcS.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}