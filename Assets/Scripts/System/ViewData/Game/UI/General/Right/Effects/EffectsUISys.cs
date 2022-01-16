using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct EffectsUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (Unit<HaveEffectC>(UnitStatTypes.Damage, SelIdx<IdxC>().Idx).Have) 
            {
                UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Damage).Color = Color.green;
            }
            else UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Damage).Color = Color.white;


            if (Unit<HaveEffectC>(UnitStatTypes.Steps, SelIdx<IdxC>().Idx).Have)
            {
                UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Steps).Color = Color.green;
            }
            else UIEntRightEffects.Image<ImageUIC>(UnitStatTypes.Steps).Color = Color.white;
        }
    }
}