using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitVEs
    {
        readonly Entity _main;
        readonly Entity _extra;

        public ref SpriteRendererVC UnitMainSR => ref _main.Get<SpriteRendererVC>();
        public ref SpriteRendererVC UnitExtraSR => ref _extra.Get<SpriteRendererVC>();


        public readonly CellUnitEffectVEs EffectVEs;


        public CellUnitVEs(in Transform cellT, in EcsWorld gameW)
        {
            var cellUnit = cellT.Find("Unit");

            _main = gameW.NewEntity()
                .Add(new SpriteRendererVC(cellUnit.Find("MainUnit_SR").GetComponent<SpriteRenderer>()));

            _extra = gameW.NewEntity()
                .Add(new SpriteRendererVC(cellUnit.Find("ExtraUnit_SR").GetComponent<SpriteRenderer>()));

            EffectVEs = new CellUnitEffectVEs(cellUnit, gameW);
        }
    }
}