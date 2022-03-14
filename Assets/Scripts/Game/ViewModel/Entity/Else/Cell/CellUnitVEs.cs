using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CellUnitVEs
    {
        public readonly Dictionary<string, SpriteRendererVC> Ents;
        public readonly Dictionary<string, SpriteRendererVC> ExtraTws;
        public readonly Dictionary<string, SpriteRendererVC> MainTws;
        public readonly Dictionary<string, SpriteRendererVC> BowCrossbows;

        public SpriteRendererVC UnitE(in bool isSelected, in LevelTypes levT, in UnitTypes unit) => Ents[isSelected.ToString() + levT + unit];
        public SpriteRendererVC ExtraToolWeaponE(in bool isSelected, in LevelTypes level, in ToolWeaponTypes tw) => ExtraTws[isSelected.ToString() + level + tw];
        public SpriteRendererVC MainToolWeaponE(in bool isSelected, in LevelTypes level, in ToolWeaponTypes tw) => MainTws[isSelected.ToString() + level + tw];
        public SpriteRendererVC MainBowCrossbowE(in bool isSelected, in LevelTypes levelT, in bool isRight) => BowCrossbows[isSelected.ToString() + levelT + isRight];

        public readonly CellUnitEffectVEs EffectVEs;


        public CellUnitVEs(in Transform cellT)
        {
            var cellUnitZone = cellT.Find("Unit+");

            Ents = new Dictionary<string, SpriteRendererVC>();
            ExtraTws = new Dictionary<string, SpriteRendererVC>();
            MainTws = new Dictionary<string, SpriteRendererVC>();
            BowCrossbows = new Dictionary<string, SpriteRendererVC>();




            for (int i = 0; i <= 1; i++)
            {
                var isSelected = i == 0;

                var selZone = isSelected ? cellUnitZone.Find("Selected+") : cellUnitZone.Find("NotSelected+");

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    var zonee = selZone.Find(levT.ToString() + "+");

                    for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                    {
                        if (unitT != UnitTypes.Pawn)
                        {
                            Ents.Add(isSelected.ToString() + levT + unitT, new SpriteRendererVC(zonee.Find(unitT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                        }
                    }

                    var extraTwZone = zonee.Find("ExtraToolWeapon+");

                    for (var twT = ToolWeaponTypes.Pick; twT <= ToolWeaponTypes.Shield; twT++)
                    {
                        ExtraTws.Add(isSelected.ToString() + levT + twT, new SpriteRendererVC(extraTwZone.Find(twT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                    }


                    var mainTwZone = zonee.Find("MainToolWeapon+");

                    for (var twT = ToolWeaponTypes.Staff; twT <= ToolWeaponTypes.Axe; twT++)
                    {
                        if (twT == ToolWeaponTypes.BowCrossbow)
                        {
                            var bowCrossbow = mainTwZone.Find(twT.ToString() + "+");

                            BowCrossbows.Add(isSelected.ToString() + levT + true, new SpriteRendererVC(bowCrossbow.Find("Right_SR+").GetComponent<SpriteRenderer>()));
                            BowCrossbows.Add(isSelected.ToString() + levT + false, new SpriteRendererVC(bowCrossbow.Find("Cornered_SR+").GetComponent<SpriteRenderer>()));
                        }
                        else
                        {
                            MainTws.Add(isSelected.ToString() + levT + twT, new SpriteRendererVC(mainTwZone.Find(twT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                        }
                    }
                }
            }

            EffectVEs = new CellUnitEffectVEs(cellUnitZone);
        }
    }
}