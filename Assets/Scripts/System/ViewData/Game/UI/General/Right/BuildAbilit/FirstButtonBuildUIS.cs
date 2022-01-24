using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct FirstButtonBuildUIS : IEcsRunSystem
    {
        public void Run()
        {
            var needActiveButton = false;

            if (SelectedIdxE.IsSelCell)
            {
                ref var selUnitDatCom = ref Unit(SelectedIdxE.IdxC.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref EntitiesPool.UnitElse.Owner(SelectedIdxE.IdxC.Idx);

                    if (selOnUnitCom.Is(WhoseMoveE.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            RightUIEntities.Building(ButtonTypes.First).Parent.SetActive(needActiveButton);
        }
    }
}
