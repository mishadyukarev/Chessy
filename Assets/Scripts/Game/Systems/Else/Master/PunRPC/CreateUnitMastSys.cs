using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class CreateUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
        private EcsFilter<SoundEffectC> _soundEffFilt = default;


        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            ref var soundEffecCom = ref _soundEffFilt.Get1(0);


            var unitTypeForCreating = _creatorUnitFilter.Get1(0).UnitTypeForCreating;


            var playerSend = WhoseMoveC.WhoseMove;


            if (BuildsInGameC.IsSettedCity(playerSend))
            {
                if (InventResourcesC.CanCreateUnit(playerSend, unitTypeForCreating, out var needRes))
                {
                    InventResourcesC.BuyCreateUnit(playerSend, unitTypeForCreating);
                    InventorUnitsC.AddUnitsInInventor(playerSend, unitTypeForCreating, LevelUnitTypes.Wood);

                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.SoundGoldPack);
                }
                else
                {
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}