using Chessy.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CenterUIEs
    {
        readonly Dictionary<UnitTypes, CenterHeroUIE> _ents;

        public GameObjectVC Zone;

        public ButtonUIC JoinDiscordButtonC;
        public ButtonUIC ReadyButtonC;

        public TextUIC EndGame;
        public TextUIC Motion;


        public readonly CenterFriendUIE FriendE;
        public readonly CenterKingUIE KingE;
        public readonly CenterUpgradeUIE UpgradeE;
        public readonly CenterSelectorUIE SelectorE;
        public readonly CenterMistakeUIE MistakeE;

        public CenterHeroUIE HeroE(in UnitTypes unit) => _ents[unit];


        internal CenterUIEs(in bool def)
        {
            var centerZone = CanvasC.FindUnderCurZone("CenterZone").transform;



            var parent = centerZone.transform.Find("Heroes");


            Zone = new GameObjectVC(parent.gameObject);

            _ents = new Dictionary<UnitTypes, CenterHeroUIE>();
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
            {
                _ents.Add(unit, new CenterHeroUIE(parent, unit));
            }


            FriendE = new CenterFriendUIE(centerZone);
            KingE = new CenterKingUIE(centerZone);
            UpgradeE = new CenterUpgradeUIE(centerZone);
            SelectorE = new CenterSelectorUIE(centerZone);
            MistakeE = new CenterMistakeUIE(centerZone);

            new CenterHintUIE(centerZone);
            




            EndGame = new TextUIC(centerZone.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>());
            Motion = new TextUIC(centerZone.Find("MotionZone").Find("MotionText").GetComponent<TextMeshProUGUI>());


            var readyZone = centerZone.Find("ReadyZone");

            JoinDiscordButtonC = new ButtonUIC(readyZone.Find("JoinDiscordButton").GetComponent<Button>());
            ReadyButtonC = new ButtonUIC(readyZone.Find("ReadyButton").GetComponent<Button>());
        }
    }
}