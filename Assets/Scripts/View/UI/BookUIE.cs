using Chessy.Common.Component;
using Chessy.Common.Enum;
using Chessy.Common.View.UI.Component;
using Chessy.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Chessy.Common.Entity.View.UI
{
    public readonly struct BookUIE
    {
        readonly Dictionary<PageBookTypes, GameObjectVC> _pages;

        public readonly GameObjectVC ParenGOC;

        public readonly ButtonUIC ExitButtonC;
        public readonly ButtonUIC NextButtonC;
        public readonly ButtonUIC BackButtonC;

        public readonly TextUIC LeftPageTextC;
        public readonly TextUIC RightPageTextC;

        public GameObjectVC PageGOC(in PageBookTypes pageT) => _pages[pageT];

        public BookUIE(in Transform commonZone)
        {
            var parent = commonZone.Find("Book+");

            ParenGOC = new GameObjectVC(parent.gameObject);
            ExitButtonC = new ButtonUIC(parent.Find("Exit+").Find("Button+").GetComponent<Button>());
            NextButtonC = new ButtonUIC(parent.Find("Next_Button+").GetComponent<Button>());
            BackButtonC = new ButtonUIC(parent.Find("Back_Button+").GetComponent<Button>());

            _pages = new Dictionary<PageBookTypes, GameObjectVC>();

            var zone = parent.Find("Zones+");

            for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
            {
                var page = zone.Find(pageT + "+");
                _pages.Add(pageT, new GameObjectVC(page.gameObject));

                if (pageT == PageBookTypes.Main ||
                   pageT == PageBookTypes.God ||
                   pageT == PageBookTypes.Pawn ||
                   pageT == PageBookTypes.ExtractPawn ||
                   pageT == PageBookTypes.UsingAbilities ||
                   pageT == PageBookTypes.Town ||
                   pageT == PageBookTypes.Doner ||
                   pageT == PageBookTypes.ToolWeapons)
                {
                    page.Find("VideoPlayer+").GetComponent<VideoPlayer>().url = Application.streamingAssetsPath + "/" + pageT + ".mp4";
                }
            }



            LeftPageTextC = new TextUIC(parent.Find("LeftPage_TMP+").GetComponent<TextMeshProUGUI>());
            RightPageTextC = new TextUIC(parent.Find("RightPage_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}