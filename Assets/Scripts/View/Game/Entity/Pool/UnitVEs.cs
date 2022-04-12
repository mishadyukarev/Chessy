using Chessy.Common;
using Chessy.Game.Entity.View.Cell.Unit.Effect;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Entity.View.Cell
{
    public readonly struct UnitVEs
    {
        readonly SpriteRendererVC[] _units;
        readonly Dictionary<string, SpriteRendererVC> _mainToolWeapons;
        readonly Dictionary<string, SpriteRendererVC> _bowCrossbows;


        public readonly EffectVEs EffectVEs;
        public readonly SpriteRendererVC NeedFoodSRC;
        public readonly AnimationVC AnimationUnitC;
        public readonly AnimationVC CircularAttackAnimC;

        public SpriteRendererVC UnitSRC(in UnitTypes unitT) => _units[(byte)unitT];
        public SpriteRendererVC MainToolWeaponSRC(in LevelTypes level, in ToolWeaponTypes tw) => _mainToolWeapons[level.ToString() + tw];
        public SpriteRendererVC MainBowCrossbowSRC(in LevelTypes level, in bool isRight) => _bowCrossbows[level.ToString() + isRight];


        public UnitVEs(in Transform cellT)
        {

            var unitZone = cellT.Find("Unit+");

            NeedFoodSRC = new SpriteRendererVC(unitZone.Find("NeedFood_SR").GetComponent<SpriteRenderer>());
            EffectVEs = new EffectVEs(unitZone);
            CircularAttackAnimC = new AnimationVC(unitZone.Find("CircularAttackKing+").GetComponent<Animation>());


            _units = new SpriteRendererVC[(byte)UnitTypes.End];
            var unitZ = unitZone.Find("Unit+");
            AnimationUnitC = new AnimationVC(unitZ.GetComponent<Animation>());


            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                if (unitT != UnitTypes.Pawn)
                {
                    _units[(byte)unitT] = new SpriteRendererVC(unitZ.Find(unitT.ToString() + "_" + "SR+").GetComponent<SpriteRenderer>());
                }
            }



            _mainToolWeapons = new Dictionary<string, SpriteRendererVC>();
            _bowCrossbows = new Dictionary<string, SpriteRendererVC>();

            var mainToolWeaponZone = unitZ.Find("MainToolWeapon+");

            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelZone = mainToolWeaponZone.Find(levelT.ToString() + "Level+");

                var nameSpriteRenderEnd = "_SR+";

                foreach (var twT in new[] { ToolWeaponTypes.Staff, ToolWeaponTypes.Axe })
                {
                    _mainToolWeapons.Add(levelT.ToString() + twT, new SpriteRendererVC(levelZone.Find(twT.ToString() + nameSpriteRenderEnd).GetComponent<SpriteRenderer>()));
                }


                var bowCrossbowZone = levelZone.Find("BowCrossbow+");

                foreach (var isRight in new[] { true, false })
                {
                    var name = isRight ? "Right" : "Cornered";
                    name += nameSpriteRenderEnd;

                    _bowCrossbows.Add(levelT.ToString() + isRight, new SpriteRendererVC(bowCrossbowZone.Find(name).GetComponent<SpriteRenderer>()));
                }
            }
        }
    }
}