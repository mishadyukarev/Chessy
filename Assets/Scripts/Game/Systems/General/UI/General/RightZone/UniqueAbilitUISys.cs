using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class UniqueAbilitUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);

            ref var selEnvDataCom = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);

            ref var selFireDatCom = ref _cellFireFilter.Get1(SelectorC.IdxSelCell);



            var activeFirst = false;
            var activeSecond = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (selOwnUnitCom.Is(WhoseMoveC.CurPlayer))
                {
                    if (selUnitDatCom.Is(UnitTypes.King))
                    {
                        activeFirst = true;
                        RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.CircularAttack);
                    }
                    else if (selUnitDatCom.Is(UnitTypes.Scout))
                    {

                    }
                    else
                    {
                        if (selUnitDatCom.IsMelee)
                        {
                            activeFirst = true;

                            if (selEnvDataCom.Have(EnvirTypes.AdultForest))
                            {
                                if (selFireDatCom.HaveFire)
                                {
                                    RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.FireNone);
                                }
                                else
                                {
                                    RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.Fire);     
                                }
                            }
                            else
                            {
                                RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.YoungForest);
                            }
                        }

                        else
                        {
                            activeFirst = true;
                            RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.Fire);
                        }
                    }
                }
            }

            RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.First, activeFirst);
            RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.Second, activeSecond);
        }


    }
}