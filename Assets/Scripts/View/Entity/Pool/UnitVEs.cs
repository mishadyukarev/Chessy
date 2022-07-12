using Chessy.Model;
using Chessy.View.Component;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.View.Entity
{
    public readonly struct UnitVEs
    {
        readonly SpriteRendererVC[] _blocks;
        readonly SpriteRendererVC[] _units;
        readonly SpriteRendererVC[,] _mainToolWeapons;
        readonly SpriteRendererVC[,] _bowCrossbows;
        readonly SpriteRendererVC[,] _extraToolWeapons;




        internal readonly TransformVC ParentTC;
        internal readonly EffectVE EffectE;
        internal readonly AnimationVC AnimationUnitC;
        internal readonly AnimationVC CircularAttackAnimC;
        internal readonly AnimationVC AddingWaterAnimationC;
        internal readonly SpriteRendererVC UnitHpBarSRC;


        public SpriteRendererVC UnitSRC(in UnitTypes unitT) => _units[(byte)unitT];
        public SpriteRendererVC MainToolWeaponSRC(in LevelTypes level, in ToolsWeaponsWarriorTypes tw) => _mainToolWeapons[(byte)level, (byte)tw];
        public SpriteRendererVC MainBowCrossbowSRC(in LevelTypes level, in bool isRight) => _bowCrossbows[(byte)level, isRight ? 1 : 0];
        public SpriteRendererVC ExtraToolWeaponSRC(in LevelTypes level, in ToolsWeaponsWarriorTypes twT) => _extraToolWeapons[(byte)level, (byte)twT];

        public SpriteRendererVC Block(in CellBlockTypes block) => _blocks[(byte)block];


        public UnitVEs(in Transform cellT)
        {
            var unitZ = cellT.Find("Unit+");


            var nameSpriteRenderEnd = "_SR+";

            ParentTC = new TransformVC(unitZ);
            CircularAttackAnimC = new AnimationVC(unitZ.Find("CircularAttackKing+").GetComponent<Animation>());


            CircularAttackAnimC.Animation["CircularAttackKing"].time = 0;
            AddingWaterAnimationC = new AnimationVC(unitZ.Find("AddingWater+").GetComponent<Animation>());


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



            _mainToolWeapons = new SpriteRendererVC[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
            _bowCrossbows = new SpriteRendererVC[(byte)LevelTypes.End, 2];
            _extraToolWeapons = new SpriteRendererVC[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];

            var mainToolWeaponZone = unitZ.Find("MainToolWeapon+");
            var extraToolWeaponZone = unitZ.Find("ExtraToolWeapon+");

            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelMainZone = mainToolWeaponZone.Find(levelT.ToString() + "Level+");

                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    _mainToolWeapons[(byte)levelT, (byte)twT] = new SpriteRendererVC(levelMainZone.Find(twT.ToString() + nameSpriteRenderEnd).GetComponent<SpriteRenderer>());
                }

                var bowCrossbowZone = levelMainZone.Find("BowCrossbow+");

                foreach (var isRight in new[] { true, false })
                {
                    var name = isRight ? "Right" : "Cornered";
                    name += nameSpriteRenderEnd;

                    _bowCrossbows[(byte)levelT, isRight ? 1 : 0] = new SpriteRendererVC(bowCrossbowZone.Find(name).GetComponent<SpriteRenderer>());
                }


                var levelExtraZone = extraToolWeaponZone.Find(levelT.ToString() + "Level+");
                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Pick, ToolsWeaponsWarriorTypes.Shield, ToolsWeaponsWarriorTypes.Sword })
                {
                    _extraToolWeapons[(byte)levelT, (byte)twT] = new SpriteRendererVC(levelExtraZone.Find(twT.ToString() + nameSpriteRenderEnd).GetComponent<SpriteRenderer>());
                }
            }
        }
    }
}