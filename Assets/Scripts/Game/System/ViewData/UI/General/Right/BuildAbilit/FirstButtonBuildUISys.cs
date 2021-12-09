using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class FirstButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitF = default;

        public void Run()
        {
            var needActiveButton = false;

            if (SelIdx<SelIdxC>().IsSelCell)
            {
                ref var selUnitDatCom = ref _unitF.Get1(SelIdx<IdxC>().Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref _unitF.Get2(SelIdx<IdxC>().Idx);

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
