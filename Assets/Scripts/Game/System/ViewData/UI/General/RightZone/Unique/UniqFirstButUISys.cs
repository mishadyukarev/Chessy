using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class UniqFirstButUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;

        public void Run()
        {
            ref var unit_sel = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
            ref var ownUnit_sel = ref _cellUnitFilter.Get2(SelectorC.IdxSelCell);

            ref var env_sel = ref _cellEnvFilter.Get1(SelectorC.IdxSelCell);

            ref var fire_sel = ref _cellFireFilter.Get1(SelectorC.IdxSelCell);



            var ability = UniqFirstAbilTypes.None;

            if (unit_sel.HaveUnit)
            {
                if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    if (unit_sel.Is(UnitTypes.King))
                    {
                        ability = UniqFirstAbilTypes.CircularAttack;
                    }
                    else if (!unit_sel.Is(UnitTypes.Scout))
                    {
                        if (unit_sel.IsMelee)
                        {
                            if (env_sel.Have(EnvTypes.AdultForest))
                            {
                                if (fire_sel.HaveFire)
                                {
                                    ability = UniqFirstAbilTypes.PutOutFirePawn;
                                }
                                else
                                {
                                    ability = UniqFirstAbilTypes.FirePawn;
                                }
                            }
                            else
                            {
                                ability = UniqFirstAbilTypes.Seed;
                            }
                        }

                        else
                        {
                            ability = UniqFirstAbilTypes.FireArcher;
                        }
                    }
                }
            }

            UniqFirstButDataC.SetAbility(ability);
            UniqFirstButViewC.SetActive(ability);
        }
    }
}