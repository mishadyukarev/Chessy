using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct BarsVC
    {
        private Dictionary<CellBarTypes, SpriteRenderer> _bar_SRs;

        public BarsVC(GameObject cell)
        {
            var parentGO = cell.transform.Find("SupportStatic").gameObject;

            _bar_SRs = new Dictionary<CellBarTypes, SpriteRenderer>();

            _bar_SRs.Add(CellBarTypes.Food, parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>());
            _bar_SRs.Add(CellBarTypes.Wood, parentGO.transform.Find("Forest").GetComponent<SpriteRenderer>());
            _bar_SRs.Add(CellBarTypes.Ore, parentGO.transform.Find("Ore").GetComponent<SpriteRenderer>());
            _bar_SRs.Add(CellBarTypes.Hp, parentGO.transform.Find("Hp").GetComponent<SpriteRenderer>());
        }

        private void ActiveVision(CellBarTypes supportStaticType, bool isActive) => _bar_SRs[supportStaticType].enabled = isActive;

        public void EnableSR(CellBarTypes cellBarType) => ActiveVision(cellBarType, true);
        public void DisableSR(CellBarTypes cellBarType) => ActiveVision(cellBarType, false);

        public void SetScale(CellBarTypes cellBarType, Vector3 scale) => _bar_SRs[cellBarType].transform.localScale = scale;
        public void SetColorHp(Color color) => _bar_SRs[CellBarTypes.Hp].color = color;
    }
}
