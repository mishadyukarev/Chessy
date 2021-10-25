using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class UniqueAbilitUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<UniqueAbiltUICom> _unitZoneUIFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        public void Run()
        {
            var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

            ref var unitZoneViewCom = ref _unitZoneUIFilter.Get1(0);

            ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
            ref var selOwnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);

            ref var selEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);

            ref var selFireDatCom = ref _cellFireFilter.Get1(idxSelCell);



            var activeFirst = false;
            var activeSecond = false;

            if (selUnitDatCom.HaveUnit)
            {
                if (selOwnUnitCom.Is(WhoseMoveCom.CurPlayer))
                {
                    if (selUnitDatCom.Is(UnitTypes.King))
                    {
                        activeFirst = true;
                        unitZoneViewCom.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.CircularAttack);
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
                                    unitZoneViewCom.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.FireNone);
                                }
                                else
                                {
                                    unitZoneViewCom.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.Fire);     
                                }
                            }
                            else
                            {
                                unitZoneViewCom.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.YoungForest);
                            }
                        }

                        else
                        {
                            activeFirst = true;
                            unitZoneViewCom.Set_Sprite(UniqueButtonTypes.First, SpriteGameTypes.Fire);
                        }
                    }
                }
            }

            unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, activeFirst);
            unitZoneViewCom.SetActive_Button(UniqueButtonTypes.Second, activeSecond);
        }


    }
}