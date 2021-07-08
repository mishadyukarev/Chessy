

using System.Collections.Generic;

namespace Assets.Scripts.ECS.Game.General.Systems.RunUpdate.UI.DownZone
{
    internal sealed class FinderIdleUnitUISystem : RPCGeneralSystemReduction
    {
        private List<int[]> _preXyList;

        public override void Init()
        {
            base.Init();

            _preXyList = new List<int[]>();
            _eGM.FinderIdleEnt_ButtonCom.AddListener(FindIdleUnit);
        }

        private void FindIdleUnit()
        {
            var xy = _eGM.SelectorEnt_SelectorCom.XySelectedCell;
            //_eGM.CellEnt_CellBaseCom(xy).IsSelected = false;
            _eGM.SelectorEnt_SelectorCom.IsSelected = false;
            _eGM.SelectorEnt_SelectorCom.XySelectedCell = new int[] { 0, 0 };


            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                    {
                        var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;

                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)

                            if (_eGM.CellUnitEnt_CellUnitCom(x,y).HaveMaxSteps(unitType))
                            {
                                _eGM.SelectorEnt_SelectorCom.IsSelected = true;
                                _eGM.SelectorEnt_SelectorCom.XySelectedCell = new int[] { x, y };
                            }
                    }
                }
        }
    }
}
