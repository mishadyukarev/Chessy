using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct FirstButtonBuildUIS : IEcsRunSystem
    {
        public void Run()
        {
            var needActiveButton = false;

            if (SelIdx<SelIdxC>().IsSelCell)
            {
                ref var selUnitDatCom = ref Unit<UnitC>(SelIdx<IdxC>().Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref Unit<OwnerC>(SelIdx<IdxC>().Idx);

                    if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitUIC.SetActive_Button(BuildButtonTypes.First, needActiveButton);
        }
    }
}
