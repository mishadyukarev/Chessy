using Chessy.Common.Component;
using Chessy.Common.Enum;
using Chessy.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Common.Entity.View.UI
{
    public readonly struct BookUIE
    {
        readonly Dictionary<PageBoookTypes, GameObjectVC> _pages;

        public readonly GameObjectVC ParenGOC;

        public readonly ButtonUIC ExitButtonC;
        public readonly ButtonUIC NextButtonC;
        public readonly ButtonUIC BackButtonC;

        public readonly TextUIC LeftPageTextC;
        public readonly TextUIC RightPageTextC;

        public GameObjectVC PageGOC(in PageBoookTypes pageT) => _pages[pageT];

        public BookUIE(in Transform commonZone)
        {
            var parent = commonZone.Find("Book+");

            ParenGOC = new GameObjectVC(parent.gameObject);
            ExitButtonC = new ButtonUIC(parent.Find("Exit+").Find("Button+").GetComponent<Button>());
            NextButtonC = new ButtonUIC(parent.Find("Next_Button+").GetComponent<Button>());
            BackButtonC = new ButtonUIC(parent.Find("Back_Button+").GetComponent<Button>());

            _pages = new Dictionary<PageBoookTypes, GameObjectVC>();
            for (var pageT = PageBoookTypes.None + 1; pageT < PageBoookTypes.End; pageT++) 
                _pages.Add(pageT, new GameObjectVC(parent.Find(pageT + "+").gameObject));


            LeftPageTextC = new TextUIC(parent.Find("LeftPage_TMP+").GetComponent<TextMeshProUGUI>());
            RightPageTextC = new TextUIC(parent.Find("RightPage_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}