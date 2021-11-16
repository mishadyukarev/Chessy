using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SecButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _cellUnitFilt = default;

        public void Run()
        {
            var needActiveButton = false;

            if (SelIdx.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(SelIdx.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref _cellUnitFilt.Get2(SelIdx.Idx);

                    if (sellOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.Second, needActiveButton);
        }
    }
}
