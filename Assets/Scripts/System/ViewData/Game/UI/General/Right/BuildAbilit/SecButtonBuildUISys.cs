using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SecButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            var needActiveButton = false;

            if (SelectedIdxE.IsSelCell)
            {
                ref var selUnitDatCom = ref Unit(SelectedIdxE.IdxC.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref CellUnitElseEs.Owner(SelectedIdxE.IdxC.Idx);

                    if (sellOnUnitCom.Is(WhoseMoveE.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            UIEntBuild.Button<ButtonUIC>(ButtonTypes.Second).SetActive(needActiveButton);
        }
    }
}
