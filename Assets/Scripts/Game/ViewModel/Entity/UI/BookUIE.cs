using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.Entity.View.UI.Center
{
    public readonly struct BookUIE
    {
        readonly Dictionary<byte, GameObjectVC> _pages;

        public readonly GameObjectVC ParenGOC;

        public readonly ButtonUIC ExitButtonC;
        public readonly ButtonUIC NextButtonC;
        public readonly ButtonUIC BackButtonC;

        public readonly TextUIC LeftPageTextC;
        public readonly TextUIC RightPageTextC;

        public GameObjectVC PageGOC(in byte idx_page) => _pages[idx_page];

        public BookUIE(in Transform centerZone)
        {
            var parent = centerZone.Find("Book+");

            ParenGOC = new GameObjectVC(parent.gameObject);
            ExitButtonC = new ButtonUIC(parent.Find("Exit+").Find("Button+").GetComponent<Button>());
            NextButtonC = new ButtonUIC(parent.Find("Next_Button+").GetComponent<Button>());
            BackButtonC = new ButtonUIC(parent.Find("Back_Button+").GetComponent<Button>());

            _pages = new Dictionary<byte, GameObjectVC>();
            for (byte idx_page = 0; idx_page < Values.Values.MAX_PAGES; idx_page++) 
                _pages.Add(idx_page, new GameObjectVC(parent.Find("Page" + idx_page + "+").gameObject));


            LeftPageTextC = new TextUIC(parent.Find("LeftPage_TMP+").GetComponent<TextMeshProUGUI>());
            RightPageTextC = new TextUIC(parent.Find("RightPage_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}