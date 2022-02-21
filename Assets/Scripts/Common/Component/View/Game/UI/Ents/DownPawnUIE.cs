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


        public readonly DownPawnMaxPeopleE MaxPeopleE;

        public DownPawnUIE(in Transform downZone, in EcsWorld gameW)
        {
            var pawnT = downZone.Find(UnitTypes.Pawn.ToString());

            var button = pawnT.Find("Button").GetComponent<Button>();

            _taker =  gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new TextUIC(pawnT.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));

            MaxPeopleE = new DownPawnMaxPeopleE(pawnT.Find("MaxPeople_TextMP+").GetComponent<TextMeshProUGUI>(), gameW);
        }
    }
}
