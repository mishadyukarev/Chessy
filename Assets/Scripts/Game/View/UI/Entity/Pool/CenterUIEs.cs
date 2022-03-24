using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Game.View.UI.Entity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CenterUIEs
    {
        readonly Dictionary<UnitTypes, CenterHeroUIE> _ents;

        public readonly GameObjectVC Zone;

        public readonly ButtonUIC JoinDiscordButtonC;
        public readonly ButtonUIC ReadyButtonC;

        public readonly TextUIC EndGame;
        public readonly TextUIC Motion;

        public readonly ButtonUIC OpenShopButtonC;


        public readonly CenterFriendUIE FriendE;
        public readonly CenterKingUIE KingE;
        public readonly CenterSelectorUIE SelectorE;
        public readonly MistakeUIE MistakeE;
        public readonly CenterMarketUIE MarketE;
        public readonly CenterSmelterUIE SmelterE;


        #region Lesson

        public readonly SelectionBookGuidUIE BookGuidSelectionE;
        public readonly SettingUnitLessonUIE SettingUnitLessonE;

        #endregion


        public CenterHeroUIE HeroE(in UnitTypes unit) => _ents[unit];


        internal CenterUIEs(in Transform centerZone)
        {
            var parent = centerZone.transform.Find("Heroes");


            OpenShopButtonC = new ButtonUIC(parent.Find("OpenShop_Button").GetComponent<Button>());

            Zone = new GameObjectVC(parent.gameObject);

            _ents = new Dictionary<UnitTypes, CenterHeroUIE>();
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
            {
                _ents.Add(unit, new CenterHeroUIE(parent, unit));
            }


            FriendE = new CenterFriendUIE(centerZone);
            KingE = new CenterKingUIE(centerZone);
            SelectorE = new CenterSelectorUIE(centerZone);
            MistakeE = new MistakeUIE(centerZone);

            var training = centerZone.Find("Lesson+");
            BookGuidSelectionE = new SelectionBookGuidUIE(training.Find("SelectionGuidBook+"));
            SettingUnitLessonE = new SettingUnitLessonUIE(training.Find("NeedSetKing+"));


            var buildingZone = centerZone.Find("Building+");
            MarketE = new CenterMarketUIE(buildingZone);
            SmelterE = new CenterSmelterUIE(buildingZone);


            EndGame = new TextUIC(centerZone.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>());
            Motion = new TextUIC(centerZone.Find("MotionZone").Find("MotionText").GetComponent<TextMeshProUGUI>());


            var readyZone = centerZone.Find("ReadyZone");

            JoinDiscordButtonC = new ButtonUIC(readyZone.Find("JoinDiscord+").Find("Button+").GetComponent<Button>());
            ReadyButtonC = new ButtonUIC(readyZone.Find("ReadyButton").GetComponent<Button>());
        }
    }
}