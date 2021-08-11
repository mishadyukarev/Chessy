using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellBarsViewComponent
    {
        private Dictionary<CellBarTypes, SpriteRenderer> _bar_SRs;

        internal CellBarsViewComponent(GameObject cell)
        {
            var parentGO = cell.transform.Find("SupportStatic").gameObject;

            _bar_SRs = new Dictionary<CellBarTypes, SpriteRenderer>();

            _bar_SRs.Add(CellBarTypes.Food, parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>());
            _bar_SRs.Add(CellBarTypes.Wood, parentGO.transform.Find("Forest").GetComponent<SpriteRenderer>());
            _bar_SRs.Add(CellBarTypes.Ore, parentGO.transform.Find("Ore").GetComponent<SpriteRenderer>());
            _bar_SRs.Add(CellBarTypes.Hp, parentGO.transform.Find("Hp").GetComponent<SpriteRenderer>());

            _bar_SRs[CellBarTypes.Food].color = Color.yellow;
            _bar_SRs[CellBarTypes.Wood].color = Color.green;
            _bar_SRs[CellBarTypes.Ore].color = Color.grey;
            _bar_SRs[CellBarTypes.Hp].color = Color.red;
        }

        private void ActiveVision(CellBarTypes supportStaticType, bool isActive) => _bar_SRs[supportStaticType].enabled = isActive;

        internal void EnableSR(CellBarTypes cellBarType) => ActiveVision(cellBarType, true);
        internal void DisableSR(CellBarTypes cellBarType) => ActiveVision(cellBarType, false);

        internal void SetScale(CellBarTypes cellBarType, Vector3 scale) => _bar_SRs[cellBarType].transform.localScale = scale;
        internal void SetColorHp(Color color) => _bar_SRs[CellBarTypes.Hp].color = color;
    }
}
