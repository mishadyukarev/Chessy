using Chessy.Model;
using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Chessy.View.UI.Entity
{
    readonly struct BookUIE
    {
        readonly GameObjectVC[] _pagesZones;

        internal readonly GameObjectVC ParenGOC;

        internal readonly ButtonUIC ExitButtonC;
        internal readonly ButtonUIC NextButtonC;
        internal readonly ButtonUIC BackButtonC;

        internal readonly TextUIC LeftPageTextC;
        internal readonly TextUIC RightPageTextC;

        internal GameObjectVC PageGOC(in PageBookTypes pageT) => _pagesZones[(byte)pageT];

        internal BookUIE(in Transform commonZone)
        {
            var parent = commonZone.Find("Book+");

            ParenGOC = new GameObjectVC(parent.gameObject);
            ExitButtonC = new ButtonUIC(parent.Find("Exit+").Find("Button+").GetComponent<Button>());
            NextButtonC = new ButtonUIC(parent.Find("Next_Button+").GetComponent<Button>());
            BackButtonC = new ButtonUIC(parent.Find("Back_Button+").GetComponent<Button>());

            _pagesZones = new GameObjectVC[(byte)PageBookTypes.End];

            var zone = parent.Find("Zones+");

            for (var pageT = PageBookTypes.None + 1; pageT < PageBookTypes.End; pageT++)
            {
                var page = zone.Find(pageT + "+");
                _pagesZones[(byte)pageT] = new GameObjectVC(page.gameObject);


                foreach (var neededPageT in new[] { PageBookTypes.Main, PageBookTypes.God, PageBookTypes.Pawn,
                    PageBookTypes.ExtractPawn, PageBookTypes.UsingAbilities, PageBookTypes.Town, PageBookTypes.ToolWeapons })
                {
                    if (neededPageT == pageT)
                    {
                        page.Find("VideoPlayer+").Find("VideoPlayer+").GetComponent<VideoPlayer>().url = Application.streamingAssetsPath + "/" + pageT + ".mp4";
                    }
                }
            }



            LeftPageTextC = new TextUIC(parent.Find("LeftPage_TMP+").GetComponent<TextMeshProUGUI>());
            RightPageTextC = new TextUIC(parent.Find("RightPage_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}