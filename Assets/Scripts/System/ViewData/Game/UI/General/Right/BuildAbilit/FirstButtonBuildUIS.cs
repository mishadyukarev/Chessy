using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct FirstButtonBuildUIS : IEcsRunSystem
    {
        public void Run()
        {
            var needActiveButton = false;

            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                ref var selUnitDatCom = ref Unit<UnitTC>(SelIdx<IdxC>().Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref Unit<PlayerTC>(SelIdx<IdxC>().Idx);

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
