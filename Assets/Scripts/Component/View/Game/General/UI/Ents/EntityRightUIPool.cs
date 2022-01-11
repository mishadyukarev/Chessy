using ECS;
using Game.Common;

namespace Game.Game
{
    public readonly struct EntityRightUIPool
    {
        static EntityRightUIPool()
        {

        }
        public EntityRightUIPool(in WorldEcs gameW)
        {
            var rightZone = CanvasC.FindUnderCurZone("RightZone");

            var uniqAbilZone_trans = rightZone.transform.Find("UniqueAbilitiesZone");

            ///Right
            new StatUIC(rightZone);
            new UniqButtonsUIC(uniqAbilZone_trans);
            new BuildAbilitUIC(rightZone.transform.Find("BuildingZone"));
            new ExtraTWZoneUIC(rightZone.transform);
            new EffectsUIC(rightZone.transform);
            new ProtectUIC(rightZone.transform.Find("ConditionZone"));
            new RelaxUIC(rightZone.transform.Find("ConditionZone"));
        }
    }
}