using UnityEngine;
using static Game.Game.CellUnitEntities;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct EffectsUISys : IEcsRunSystem
    {
        public void Run()
        {
            //if (CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, EntitiesPool.SelectedIdxE.IdxC.Idx).Have) 
            //{
            //    UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Damage).Color = Color.green;
            //}
            //else UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Damage).Color = Color.white;


            //if (CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, EntitiesPool.SelectedIdxE.IdxC.Idx).Have)
            //{
            //    UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Steps).Color = Color.green;
            //}
            //else UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Steps).Color = Color.white;
        }
    }
}