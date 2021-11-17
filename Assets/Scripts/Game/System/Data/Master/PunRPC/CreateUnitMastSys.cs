using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class CreateUnitMastSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);


            var playerSend = WhoseMoveC.WhoseMove;


            if (WhereBuildsC.IsSettedCity(playerSend))
            {
                if (InvResC.CanCreateUnit(playerSend, unit, out var needRes))
                {
                    InvResC.BuyCreateUnit(playerSend, unit);
                    InvUnitsC.AddUnit(playerSend, unit, LevelTypes.First);

                    RpcSys.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}