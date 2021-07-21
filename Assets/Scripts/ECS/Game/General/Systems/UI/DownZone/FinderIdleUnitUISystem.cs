using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using System.Collections.Generic;
using static Assets.Scripts.CellUnitWorker;

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
            //_eGM.CellEnt_CellBaseCom(xy).IsSelected = false;
            _eGM.SelectorEnt_SelectorCom.IsSelected = false;
            SelectorWorker.SetXy(SelectorCellTypes.Selected, new int[] { 0, 0 });


            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (HaveAnyUnitOnCell(xy))
                    {
                        var unitType = UnitType(xy);

                        if (HaveOwner(xy))

                            if (_eGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
                            {
                                _eGM.SelectorEnt_SelectorCom.IsSelected = true;
                                SelectorWorker.SetXy(SelectorCellTypes.Selected, new int[] { x, y });
                            }
                    }
                }
        }
    }
}
