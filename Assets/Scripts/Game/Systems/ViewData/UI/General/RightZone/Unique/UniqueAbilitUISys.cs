using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class UniqueAbilitUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;

        public void Run()
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var selOwnUnitCom = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);

            ref var selEnvDataCom = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);

            ref var selFireDatCom = ref _cellFireFilter.Get1(SelectorC.IdxSelCell);



            var activeFirst = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (selOwnUnitCom.Is(WhoseMoveC.CurPlayerI))
                {
                    if (selUnitDatCom.Is(UnitTypes.King))
                    {
                        activeFirst = true;
                        RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.CircularAttack);
                        RightUniqueDataUIC.SetAbilityType(UniqueButtonTypes.First, UniqueAbilTypes.CircularAttack);
                    }
                    else if (selUnitDatCom.Is(UnitTypes.Scout))
                    {

                    }
                    else
                    {
                        if (selUnitDatCom.IsMelee)
                        {
                            activeFirst = true;

                            if (selEnvDataCom.Have(EnvTypes.AdultForest))
                            {
                                if (selFireDatCom.HaveFire)
                                {
                                    RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.FireNone);
                                    RightUniqueDataUIC.SetAbilityType(UniqueButtonTypes.First, UniqueAbilTypes.NoneFirePawn);
                                }
                                else
                                {
                                    RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.Fire);
                                    RightUniqueDataUIC.SetAbilityType(UniqueButtonTypes.First, UniqueAbilTypes.FirePawn);
                                }
                            }
                            else
                            {
                                RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.YoungForest);
                                RightUniqueDataUIC.SetAbilityType(UniqueButtonTypes.First, UniqueAbilTypes.Seed);
                            }
                        }

                        else
                        {
                            activeFirst = true;
                            RightUniqueViewUIC.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.Fire);
                            RightUniqueDataUIC.SetAbilityType(UniqueButtonTypes.First, UniqueAbilTypes.FireArcher);
                        }
                    }
                }
            }

            RightUniqueViewUIC.SetActive_Button(UniqueButtonTypes.First, activeFirst);
        }


    }
}