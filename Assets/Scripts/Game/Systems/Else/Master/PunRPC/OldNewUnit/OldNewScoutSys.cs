using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class OldNewScoutSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom> _cellUnitFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCell = _forOldNewUnitCom.Get1(0).IdxCell;
            ref var unitCom = ref _cellUnitFilt.Get1(idxCell);

            var playerSender = WhoseMoveC.WhoseMove;

            if (unitCom.HaveAmountHealth)
            {
                if (unitCom.HaveMinAmountSteps)
                {
                    InventorUnitsC.TakeUnitsInInv(playerSender, UnitTypes.Scout, LevelUnitTypes.Wood);

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