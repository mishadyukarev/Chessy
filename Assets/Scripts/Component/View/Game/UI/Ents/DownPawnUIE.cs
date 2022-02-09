using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct DownPawnUIE
    {
        static readonly Dictionary<UnitTypes, Entity> _taker;
        static readonly Dictionary<UnitTypes, Entity> _create;

        public static ref C Taker<C>(in UnitTypes unit) where C : struct => ref _taker[unit].Get<C>();
        public static ref C BuyUnit<C>(in UnitTypes unit) where C : struct => ref _create[unit].Get<C>();

        static DownPawnUIE()
        {
            _taker = new Dictionary<UnitTypes, Entity>();
            _create = new Dictionary<UnitTypes, Entity>();

            for (var unit = UnitTypes.None; unit <= UnitTypes.End; unit++)
            {
                _taker.Add(unit, default);
                _create.Add(unit, default);
            }
        }
        public DownPawnUIE(in EcsWorld gameW, in Transform takeUnit)
        {
            _create[UnitTypes.Pawn] = gameW.NewEntity()
                .Add(new ButtonUIC(takeUnit.Find("CreatePawn_Button").GetComponent<Button>()));


            var button = takeUnit.Find(UnitTypes.Pawn.ToString()).GetComponent<Button>();
            _taker[UnitTypes.Pawn] =  gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new TextUIC(button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));
        }
    }
}
