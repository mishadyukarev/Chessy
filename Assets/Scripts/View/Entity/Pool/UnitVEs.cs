using Chessy.Common;
using Chessy.Model.Entity.View.Cell.Unit.Effect;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.Entity.View.Cell
{
    public readonly struct UnitVEs
    {
        readonly SpriteRendererVC[] _blocks;
        readonly SpriteRendererVC[] _units;
        readonly Dictionary<string, SpriteRendererVC> _mainToolWeapons;
        readonly Dictionary<string, SpriteRendererVC> _bowCrossbows;
        readonly Dictionary<string, SpriteRendererVC> _extraToolWeapons;


        internal readonly EffectVE EffectE;
        internal readonly AnimationVC AnimationUnitC;
        internal readonly AnimationVC CircularAttackAnimC;
        internal readonly SpriteRendererVC UnitHpBarSRC;


        public SpriteRendererVC UnitSRC(in UnitTypes unitT) => _units[(byte)unitT];
        public SpriteRendererVC MainToolWeaponSRC(in LevelTypes level, in ToolWeaponTypes tw) => _mainToolWeapons[level.ToString() + tw];
        public SpriteRendererVC MainBowCrossbowSRC(in LevelTypes level, in bool isRight) => _bowCrossbows[level.ToString() + isRight];
        public SpriteRendererVC ExtraToolWeaponSRC(in LevelTypes level, in ToolWeaponTypes twT) => _extraToolWeapons[level.ToString() + twT];

        public SpriteRendererVC Block(in CellBlockTypes block) => _blocks[(byte)block];


        public UnitVEs(in Transform cellT)
        {
            var unitZ = cellT.Find("Unit+");


            var nameSpriteRenderEnd = "_SR+";

            CircularAttackAnimC = new AnimationVC(unitZ.Find("CircularAttackKing+").GetComponent<Animation>());


            CircularAttackAnimC.Animation["CircularAttackKing"].time = 0;


            EffectE = new EffectVE(unitZ);
            AnimationUnitC = new AnimationVC(unitZ.GetComponent<Animation>());
            UnitHpBarSRC = new SpriteRendererVC(unitZ.Find("HpBar_SR+").GetComponent<SpriteRenderer>());

            _blocks = new SpriteRendererVC[(byte)CellBlockTypes.End];

            for (var block = CellBlockTypes.Condition; block < CellBlockTypes.End; block++)
            {
                var blocks = unitZ.Find("Blocks+");
                var name = block.ToString() + nameSpriteRenderEnd;
                var sr = blocks.Find(name).GetComponent<SpriteRenderer>();

                _blocks[(byte)block] = new SpriteRendererVC(sr);
            }

            _units = new SpriteRendererVC[(byte)UnitTypes.End];
            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                if (unitT != UnitTypes.Pawn)
                {
                    _units[(byte)unitT] = new SpriteRendererVC(unitZ.Find(unitT.ToString() + "_" + "SR+").GetComponent<SpriteRenderer>());
                }
            }



            _mainToolWeapons = new Dictionary<string, SpriteRendererVC>();
            _bowCrossbows = new Dictionary<string, SpriteRendererVC>();
            _extraToolWeapons = new Dictionary<string, SpriteRendererVC>();

            var mainToolWeaponZone = unitZ.Find("MainToolWeapon+");
            var extraToolWeaponZone = unitZ.Find("ExtraToolWeapon+");

            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelMainZone = mainToolWeaponZone.Find(levelT.ToString() + "Level+");

                foreach (var twT in new[] { ToolWeaponTypes.Staff, ToolWeaponTypes.Axe })
                {
                    _mainToolWeapons.Add(levelT.ToString() + twT, new SpriteRendererVC(levelMainZone.Find(twT.ToString() + nameSpriteRenderEnd).GetComponent<SpriteRenderer>()));
                }

                var bowCrossbowZone = levelMainZone.Find("BowCrossbow+");

                foreach (var isRight in new[] { true, false })
                {
                    var name = isRight ? "Right" : "Cornered";
                    name += nameSpriteRenderEnd;

                    _bowCrossbows.Add(levelT.ToString() + isRight, new SpriteRendererVC(bowCrossbowZone.Find(name).GetComponent<SpriteRenderer>()));
                }


                var levelExtraZone = extraToolWeaponZone.Find(levelT.ToString() + "Level+");
                foreach (var twT in new[] { ToolWeaponTypes.Pick, ToolWeaponTypes.Shield, ToolWeaponTypes.Sword })
                {
                    _extraToolWeapons.Add(levelT.ToString() + twT, new SpriteRendererVC(levelExtraZone.Find(twT.ToString() + nameSpriteRenderEnd).GetComponent<SpriteRenderer>()));
                }
            }
        }
    }
}