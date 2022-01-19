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
                ref var selUnitDatCom = ref Unit<UnitTC>(SelectedIdxE.IdxC.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref Unit<PlayerTC>(SelectedIdxE.IdxC.Idx);

                    if (selOnUnitCom.Is(WhoseMoveE.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            UIEntBuild.Button<ButtonUIC>(ButtonTypes.First).SetActive(needActiveButton);
        }
    }
}
