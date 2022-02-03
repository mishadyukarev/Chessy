using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CenterUIEs
    {
        readonly Dictionary<UnitTypes, CenterHeroUIE> _ents;

        public CenterHeroUIE HeroE(in UnitTypes unit) => _ents[unit];


        internal CenterUIEs(in EcsWorld gameW)
        {
            var centerZone = CanvasC.FindUnderCurZone("CenterZone").transform;

            

            var parent = centerZone.transform.Find("Heroes");

            _ents = new Dictionary<UnitTypes, CenterHeroUIE>();
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Camel; unit++)
            {
                _ents.Add(unit, new CenterHeroUIE(parent,unit, gameW));
            }


            ///Center

            new EntityCenterUIPool(gameW, centerZone);
            new CenterFriendUIE(gameW, centerZone);
            new CenterUpgradeUIE(gameW, centerZone);
            new CenterHintUIE(gameW, centerZone);
            new CenterSelectorUIE(gameW, centerZone);
            new CenterKingUIE(gameW, centerZone);
            new MistakeUIE(gameW, centerZone);
        }
    }
}