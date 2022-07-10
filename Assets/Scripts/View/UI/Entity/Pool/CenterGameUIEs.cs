﻿using Chessy.Model;
using Chessy.Model.Enum;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct CenterGameUIEs
    {
        readonly Dictionary<UnitTypes, CenterHeroUIE> _ents;
        readonly Dictionary<LessonTypes, GameObjectVC> _lessonGOs;

        public readonly GameObjectVC ParenGOC;

        public readonly ButtonUIC ReadyButtonC;

        public readonly TextUIC EndGame;
        public readonly TextUIC MotionTextC;

        public readonly ButtonUIC OpenShopButtonC;


        public readonly CenterFriendUIE FriendE;
        public readonly CenterKingUIE KingE;
        public readonly MistakeUIE MistakeE;
        public readonly CenterMarketUIE MarketE;
        public readonly CenterSmelterUIE SmelterE;

        public readonly SkipLessonUIE SkipLessonE;


        public CenterHeroUIE HeroE(in UnitTypes unit) => _ents[unit];
        public GameObjectVC LessonGOC(in LessonTypes lessonT) => _lessonGOs[lessonT];


        internal CenterGameUIEs(in Transform centerZone)
        {
            var parent = centerZone.Find("Heroes");

            ParenGOC = new GameObjectVC(parent.gameObject);
            OpenShopButtonC = new ButtonUIC(parent.Find("OpenShop_Button").GetComponent<Button>());



            _ents = new Dictionary<UnitTypes, CenterHeroUIE>();
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
            {
                _ents.Add(unit, new CenterHeroUIE(parent, unit));
            }


            FriendE = new CenterFriendUIE(centerZone);
            KingE = new CenterKingUIE(centerZone);
            MistakeE = new MistakeUIE(centerZone);

            var training = centerZone.Find("Lesson+");

            _lessonGOs = new Dictionary<LessonTypes, GameObjectVC>();
            for (var lessonT = (LessonTypes)1; lessonT < LessonTypes.End; lessonT++)
            {
                _lessonGOs.Add(lessonT, new GameObjectVC(training.Find(lessonT.ToString() + "+").gameObject));
            }

            SkipLessonE = new SkipLessonUIE(centerZone.Find("SkipLesson+").Find("Button+").GetComponent<Button>());




            var buildingZone = centerZone.Find("Building+");
            MarketE = new CenterMarketUIE(buildingZone);
            SmelterE = new CenterSmelterUIE(buildingZone);


            EndGame = new TextUIC(centerZone.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>());
            MotionTextC = new TextUIC(centerZone.Find("MotionZone").Find("MotionText").GetComponent<TextMeshProUGUI>());


            var readyZone = centerZone.Find("ReadyZone");
            ReadyButtonC = new ButtonUIC(readyZone.Find("ReadyButton").GetComponent<Button>());
        }
    }
}