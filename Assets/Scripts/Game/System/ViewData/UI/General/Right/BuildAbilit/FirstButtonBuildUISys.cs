using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class FirstButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitF = default;

        public void Run()
        {
            var needActiveButton = false;

            if (CellClickC.Is(CellClickTypes.SelCell))
            {
                ref var selUnitDatCom = ref _unitF.Get1(SelIdx.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref _unitF.Get2(SelIdx.Idx);

                    if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.First, needActiveButton);
        }
    }
}
