using Chessy.Common;
using Chessy.Game.Entity.View.Cell.Unit.Effect;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Entity.View.Cell
{
    public readonly struct UnitVEs
    {
        readonly SpriteRendererVC[] _units;
        //public readonly Dictionary<string, SpriteRendererVC> ExtraTws;
        //public readonly Dictionary<string, SpriteRendererVC> MainTws;
        //public readonly Dictionary<string, SpriteRendererVC> BowCrossbows;


        public readonly EffectVEs EffectVEs;
        public readonly SpriteRendererVC NeedFoodSRC;

        public readonly AnimationVC AnimationUnitC;

        public SpriteRendererVC UnitSRC(in UnitTypes unitT) => _units[(byte)unitT];
        //public SpriteRendererVC ExtraToolWeaponE(in LevelTypes level, in ToolWeaponTypes tw) => ExtraTws[level.ToString() + tw];
        //public SpriteRendererVC MainToolWeaponE(in LevelTypes level, in ToolWeaponTypes tw) => MainTws[level.ToString() + tw];
        //public SpriteRendererVC MainBowCrossbowE(in LevelTypes levelT, in bool isRight) => BowCrossbows[levelT.ToString() + isRight];


        public UnitVEs(in Transform cellT)
        {
            var unitZone = cellT.Find("Unit+");

            NeedFoodSRC = new SpriteRendererVC(unitZone.Find("NeedFood_SR").GetComponent<SpriteRenderer>());
            EffectVEs = new EffectVEs(unitZone);


            _units = new SpriteRendererVC[(byte)UnitTypes.End];
            //ExtraTws = new Dictionary<string, SpriteRendererVC>();
            //MainTws = new Dictionary<string, SpriteRendererVC>();
            //BowCrossbows = new Dictionary<string, SpriteRendererVC>();


            var unitZ = unitZone.Find("Unit+");


            AnimationUnitC = new AnimationVC(unitZ.GetComponent<Animation>());

            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                if(unitT != UnitTypes.Pawn)
                {
                    _units[(byte)unitT] = new SpriteRendererVC(unitZ.Find(unitT.ToString() + "_" + "SR+").GetComponent<SpriteRenderer>());
                }

                
            }



            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                //var zonee = selZone.Find(levT.ToString() + "+");

                //for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                //{
                //    if (unitT != UnitTypes.Pawn)
                //    {
                //        Units.Add(isSelected.ToString() + levT + unitT, new SpriteRendererVC(zonee.Find(unitT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                //    }
                //}

                //var extraTwZone = zonee.Find("ExtraToolWeapon+");

                //for (var twT = ToolWeaponTypes.Pick; twT <= ToolWeaponTypes.Shield; twT++)
                //{
                //    ExtraTws.Add(isSelected.ToString() + levT + twT, new SpriteRendererVC(extraTwZone.Find(twT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                //}


                //var mainTwZone = zonee.Find("MainToolWeapon+");

                //for (var twT = ToolWeaponTypes.Staff; twT <= ToolWeaponTypes.Axe; twT++)
                //{
                //    if (twT == ToolWeaponTypes.BowCrossbow)
                //    {
                //        var bowCrossbow = mainTwZone.Find(twT.ToString() + "+");

                //        BowCrossbows.Add(isSelected.ToString() + levT + true, new SpriteRendererVC(bowCrossbow.Find("Right_SR+").GetComponent<SpriteRenderer>()));
                //        BowCrossbows.Add(isSelected.ToString() + levT + false, new SpriteRendererVC(bowCrossbow.Find("Cornered_SR+").GetComponent<SpriteRenderer>()));
                //    }
                //    else
                //    {
                //        MainTws.Add(isSelected.ToString() + levT + twT, new SpriteRendererVC(mainTwZone.Find(twT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                //    }
                //}
            }
            


        }
    }
}