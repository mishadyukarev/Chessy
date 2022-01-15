using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SecButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            var needActiveButton = false;

            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                ref var selUnitDatCom = ref Unit<UnitTC>(SelIdx<IdxC>().Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref Unit<PlayerTC>(SelIdx<IdxC>().Idx);

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
