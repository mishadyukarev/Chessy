using static Game.Game.EntCellUnit;
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
                ref var selUnitDatCom = ref Unit<UnitC>(SelIdx<IdxC>().Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref Unit<PlayerC>(SelIdx<IdxC>().Idx);

                    if (sellOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            UIEntBuild.Button<ButtonUIC>(BuildButtonTypes.Second).SetActive(needActiveButton);
        }
    }
}
