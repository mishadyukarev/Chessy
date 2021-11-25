using System;
using UnityEngine;

namespace Game.Game
{
    public struct CellVC : ICellVE
    {
        private GameObject _cell;
        private GameObject _cellUnder;

        public bool IsActiveSelf => _cellUnder.activeSelf;
        public int InstanceID => _cellUnder.GetInstanceID();

        public CellVC(GameObject cell)
        {
            _cell = cell;
            _cellUnder = cell.transform.Find("Cell").gameObject;
        }

        public void SetRotForClient(PlayerTypes player)
        {
            if (player == PlayerTypes.None) throw new Exception();
            _cell.transform.rotation = player == PlayerTypes.First ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
        }
    }
}
