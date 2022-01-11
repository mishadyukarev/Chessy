using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SecButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            var needActiveButton = false;

            if (SelIdx<SelIdxC>().IsSelCell)
            {
                ref var selUnitDatCom = ref Unit<UnitC>(SelIdx<IdxC>().Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref Unit<OwnerC>(SelIdx<IdxC>().Idx);

                    if (sellOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitUIC.SetActive_Button(BuildButtonTypes.Second, needActiveButton);
        }
    }
}
