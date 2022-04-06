using Chessy.Common.Component;
using Chessy.Common.Enum;
using Chessy.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Chessy.Common.Entity.View.UI
{
    readonly struct BookUIE
    {
        readonly GameObjectVC[] _pages;

        internal readonly GameObjectVC ParenGOC;

        internal readonly ButtonUIC ExitButtonC;
        internal readonly ButtonUIC NextButtonC;
        internal readonly ButtonUIC BackButtonC;

        internal readonly TextUIC LeftPageTextC;
        internal readonly TextUIC RightPageTextC;

        internal GameObjectVC PageGOC(in PageBookTypes pageT) => _pages[(byte)pageT];

        internal BookUIE(in Transform commonZone)
        {
            var parent = commonZone.Find("Book+");

            ParenGOC = new GameObjectVC(parent.gameObject);
            ExitButtonC = new ButtonUIC(parent.Find("Exit+").Find("Button+").GetComponent<Button>());
            NextButtonC = new ButtonUIC(parent.Find("Next_Button+").GetComponent<Button>());
            BackButtonC = new ButtonUIC(parent.Find("Back_Button+").GetComponent<Button>());

            _pages = new GameObjectVC[(byte)PageBookTypes.End];

            var zone = parent.Find("Zones+");

            for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
            {
                var page = zone.Find(pageT + "+");
                _pages[(byte)pageT] = new GameObjectVC(page.gameObject);

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