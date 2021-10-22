using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class CreateUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _mastInfoFilter = default;
        private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
        private EcsFilter<InventorUnitsComponent, InventResourCom> _inventorFilter = default;
        private EcsFilter<SoundEffectsComp> _soundEffFilt = default;
        private EcsFilter<BuildsInGameCom> _buildsInGameFilt = default;


        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilt = default;


        public void Run()
        {
            ref var infoCom = ref _mastInfoFilter.Get1(0);
            ref var amountResCom = ref _inventorFilter.Get2(0);
            ref var unitInventorCom = ref _inventorFilter.Get1(0);
            ref var soundEffecCom = ref _soundEffFilt.Get1(0);


            var unitTypeForCreating = _creatorUnitFilter.Get1(0).UnitTypeForCreating;


            PlayerTypes playerSender = default;
            if (GameModesCom.IsOfflineMode) playerSender = WhoseMoveCom.WhoseMoveOffline;
            else playerSender = infoCom.FromInfo.Sender.GetPlayerType();


            if (_buildsInGameFilt.Get1(0).IsSettedCity(playerSender))
            {
                if (amountResCom.CanCreateUnit(playerSender, unitTypeForCreating, out var needRes))
                {
                    amountResCom.BuyCreateUnit(playerSender, unitTypeForCreating);
                    unitInventorCom.AddUnitsInInventor(playerSender, unitTypeForCreating, LevelUnitTypes.Wood);

                    RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.SoundGoldPack);
                }
                else
                {
                    RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.Mistake);
                    RpcSys.MistakeEconomyToGeneral(infoCom.FromInfo.Sender, needRes);
                }
            }
            else
            {
                RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.Mistake);
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, infoCom.FromInfo.Sender);
            }
        }
    }
}