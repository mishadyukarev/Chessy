using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellUnitVEs
    {
        readonly Dictionary<string, CellUnitVE> _ents;
        readonly Dictionary<string, CellUnitVE> _pawn;
        readonly Dictionary<string, CellUnitToolWeaponVE> _tws;
        readonly Dictionary<string, CellUnitToolWeaponVE> _shields;
        readonly Dictionary<string, CellUnitToolWeaponVE> _bowCrossbows;

        public CellUnitVE UnitE(in UnitTypes unit, in bool isSelected) => _ents[unit.ToString() + isSelected];
        public CellUnitVE PawnE(in bool isSelected, in LevelTypes levelT) => _pawn[isSelected.ToString() + levelT];
        public CellUnitToolWeaponVE ToolWeaponE(in ToolWeaponTypes tw, in bool isSelected) => _tws[tw.ToString() + isSelected];
        public CellUnitToolWeaponVE ShieldE(in LevelTypes level, in bool isSelected) => _shields[level.ToString() + isSelected];
        public CellUnitToolWeaponVE BowCrossbowE(in bool isRight, in bool isSelected, in LevelTypes levelT) => _bowCrossbows[isRight.ToString() + isSelected + levelT];

        public readonly CellUnitEffectVEs EffectVEs;


        public CellUnitVEs(in Transform cellT, in EcsWorld gameW)
        {
            var cellUnit = cellT.Find("Unit+");

            _ents = new Dictionary<string, CellUnitVE>();
            _pawn = new Dictionary<string, CellUnitVE>();




            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                var unitZone = cellUnit.Find(unitT.ToString() + "+");

                if (unitT == UnitTypes.Pawn)
                {
                    var selZone = unitZone.Find("Selected+");
                    var notSelZone = unitZone.Find("NotSelected+");

                    for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                    {
                        _pawn.Add(true.ToString() + levT, new CellUnitVE(selZone.Find(levT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));
                        _pawn.Add(false.ToString() + levT, new CellUnitVE(notSelZone.Find(levT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));
                    }
                }
                else
                {
                    _ents.Add(unitT.ToString() + true, new CellUnitVE(unitZone.Find("Selected_SR+").GetComponent<SpriteRenderer>(), gameW));
                    _ents.Add(unitT.ToString() + false, new CellUnitVE(unitZone.Find("NotSelected_SR+").GetComponent<SpriteRenderer>(), gameW));
                }
            }


            var twZone = cellUnit.Find("ToolWeapon+");
            var shieldZone = twZone.Find("Shield+");

            _tws = new Dictionary<string, CellUnitToolWeaponVE>();
            _shields = new Dictionary<string, CellUnitToolWeaponVE>();
            _bowCrossbows = new Dictionary<string, CellUnitToolWeaponVE>();

            for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
            {
                var twZ = twZone.Find(tw.ToString() + "+");

                if (tw == ToolWeaponTypes.Shield)
                {
                    for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                    {
                        var levTrans = twZ.Find(levT.ToString() + "+");

                        _shields.Add(levT.ToString() + true, new CellUnitToolWeaponVE(levTrans.Find("Selected_SR+").GetComponent<SpriteRenderer>(), gameW));
                        _shields.Add(levT.ToString() + false, new CellUnitToolWeaponVE(levTrans.Find("NotSelected_SR+").GetComponent<SpriteRenderer>(), gameW));
                    }   
                }
                else if (tw == ToolWeaponTypes.BowCrossbow)
                {
                    var right = twZ.Find("Right+");
                    var cornered = twZ.Find("Cornered+");

                    for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                    {
                        _bowCrossbows.Add(true.ToString() + true + levT, new CellUnitToolWeaponVE(right.Find("Selected+").Find(levT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));
                        _bowCrossbows.Add(true.ToString() + false + levT, new CellUnitToolWeaponVE(right.Find("NotSelected+").Find(levT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));

                        var v = cornered.Find("Selected+");
                        var vv = v.Find(levT.ToString() + "_SR+");

                        _bowCrossbows.Add(false.ToString() + true + levT, new CellUnitToolWeaponVE(cornered.Find("Selected+").Find(levT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));
                        _bowCrossbows.Add(false.ToString() + false + levT, new CellUnitToolWeaponVE(cornered.Find("NotSelected+").Find(levT.ToString() + "_SR+").GetComponent<SpriteRenderer>(), gameW));
                    }
                }
                else
                {
                    

                    _tws.Add(tw.ToString() + true, new CellUnitToolWeaponVE(twZ.Find("Selected_SR+").GetComponent<SpriteRenderer>(), gameW));
                    _tws.Add(tw.ToString() + false, new CellUnitToolWeaponVE(twZ.Find("NotSelected_SR+").GetComponent<SpriteRenderer>(), gameW));
                }
                
            }

            EffectVEs = new CellUnitEffectVEs(cellUnit, gameW);
        }
    }
}