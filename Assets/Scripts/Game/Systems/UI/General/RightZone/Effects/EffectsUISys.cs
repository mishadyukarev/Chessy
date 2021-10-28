using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EffectsUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selUnitC = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);

            EffectsIUC.SetColor(StatTypes.Health, selUnitC.Have(StatTypes.Damage));
            EffectsIUC.SetColor(StatTypes.Damage, selUnitC.Have(StatTypes.Damage));
            EffectsIUC.SetColor(StatTypes.Steps, selUnitC.Have(StatTypes.Damage));
        }
    }
}