using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CellUnitVEs
    {
        readonly Dictionary<string, CellUnitVE> _ents;
        readonly Dictionary<string, CellUnitToolWeaponVE> _extraTws;
        readonly Dictionary<string, CellUnitToolWeaponVE> _mainTws;
        readonly Dictionary<string, CellUnitToolWeaponVE> _bowCrossbows;

        public CellUnitVE UnitE(in bool isSelected, in LevelTypes levT, in UnitTypes unit) => _ents[isSelected.ToString() + levT + unit];
        public CellUnitToolWeaponVE ExtraToolWeaponE(in bool isSelected, in LevelTypes level, in ToolWeaponTypes tw) => _extraTws[isSelected.ToString() + level + tw];
        public CellUnitToolWeaponVE MainToolWeaponE(in bool isSelected, in LevelTypes level, in ToolWeaponTypes tw) => _mainTws[isSelected.ToString() + level + tw];
        public CellUnitToolWeaponVE MainBowCrossbowE(in bool isSelected, in LevelTypes levelT, in bool isRight) => _bowCrossbows[isSelected.ToString() + levelT + isRight];

        public readonly CellUnitEffectVEs EffectVEs;


        public CellUnitVEs(in Transform cellT)
        {
            var cellUnitZone = cellT.Find("Unit+");

            _ents = new Dictionary<string, CellUnitVE>();
            _extraTws = new Dictionary<string, CellUnitToolWeaponVE>();
            _mainTws = new Dictionary<string, CellUnitToolWeaponVE>();
            _bowCrossbows = new Dictionary<string, CellUnitToolWeaponVE>();




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
                            _ents.Add(isSelected.ToString() + levT + unitT, new CellUnitVE(zonee.Find(unitT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                        }
                    }

                    var extraTwZone = zonee.Find("ExtraToolWeapon+");

                    for (var twT = ToolWeaponTypes.Pick; twT <= ToolWeaponTypes.Shield; twT++)
                    {
                        _extraTws.Add(isSelected.ToString() + levT + twT, new CellUnitToolWeaponVE(extraTwZone.Find(twT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                    }


                    var mainTwZone = zonee.Find("MainToolWeapon+");

                    for (var twT = ToolWeaponTypes.BowCrossbow; twT <= ToolWeaponTypes.Axe; twT++)
                    {
                        if (twT == ToolWeaponTypes.BowCrossbow)
                        {
                            var bowCrossbow = mainTwZone.Find(twT.ToString() + "+");

                            _bowCrossbows.Add(isSelected.ToString() + levT + true, new CellUnitToolWeaponVE(bowCrossbow.Find("Right_SR+").GetComponent<SpriteRenderer>()));
                            _bowCrossbows.Add(isSelected.ToString() + levT + false, new CellUnitToolWeaponVE(bowCrossbow.Find("Cornered_SR+").GetComponent<SpriteRenderer>()));
                        }
                        else
                        {
                            _mainTws.Add(isSelected.ToString() + levT + twT, new CellUnitToolWeaponVE(mainTwZone.Find(twT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
                        }
                    }
                }
            }

            EffectVEs = new CellUnitEffectVEs(cellUnitZone);
        }
    }
}