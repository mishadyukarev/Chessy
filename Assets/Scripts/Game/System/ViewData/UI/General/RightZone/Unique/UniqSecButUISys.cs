using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class UniqSecButUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var unit_sel = _cellUnitFilt.Get1(SelectorC.IdxSelCell);
            var ownUnit_sel = _cellUnitFilt.Get2(SelectorC.IdxSelCell);

            var ability = UniqSecAbilTypes.None;

            if (unit_sel.Is(UnitTypes.King))
            {
                if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    ability = UniqSecAbilTypes.Effects; 
                }
            }

            UniqSecButDataC.SetAbility(ability);
            UniqSecButViewC.SetActive(ability);
        }
    }
}