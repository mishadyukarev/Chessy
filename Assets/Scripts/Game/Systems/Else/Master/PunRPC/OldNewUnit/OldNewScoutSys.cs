using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class OldNewScoutSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilt = default;
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom> _cellUnitFilt = default;

        private EcsFilter<InventorUnitsCom> _invUnitsFilt = default;

        public void Run()
        {
            var sender = _infoFilt.Get1(0).FromInfo.Sender;
            var idxCell = _forOldNewUnitCom.Get1(0).IdxCell;
            ref var unitCom = ref _cellUnitFilt.Get1(idxCell);

            PlayerTypes playerSender = default;
            if (GameModesCom.IsOfflineMode) playerSender = WhoseMoveCom.WhoseMoveOffline;
            else playerSender = sender.GetPlayerType();

            if (unitCom.HaveAmountHealth)
            {
                if (unitCom.HaveMinAmountSteps)
                {
                    _invUnitsFilt.Get1(0).TakeUnitsInInv(playerSender, UnitTypes.Scout, LevelUnitTypes.Wood);

                    unitCom.UnitType = _forOldNewUnitCom.Get1(0).UnitType;
                    unitCom.SetMaxAmountHealth();
                    unitCom.SetMaxAmountSteps();
                    unitCom.DefCondType();
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
        }
    }
}