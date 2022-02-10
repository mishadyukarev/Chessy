using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct DownPawnUIE
    {
        static Entity _taker;

        public static ref ButtonUIC ButtonUIC => ref _taker.Get<ButtonUIC>();
        public static ref TextUIC TextUIC => ref _taker.Get<TextUIC>();

        public DownPawnUIE(in EcsWorld gameW, in Transform down)
        {
            var pawnT = down.Find(UnitTypes.Pawn.ToString());

            var button = pawnT.Find("Button").GetComponent<Button>();

            _taker =  gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new TextUIC(pawnT.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));
        }
    }
}
