using Chessy.Model.Model.Entity;
using Chessy.Model.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncUnitBarHpVS : SystemViewCellGameAbs
    {
        bool _needActiveBar;
        Color _needSetColorToBar;

        readonly SpriteRendererVC _hpBarSRC;

        internal SyncUnitBarHpVS(in SpriteRendererVC hpBarSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _hpBarSRC = hpBarSRC;
        }

        internal override void Sync()
        {
            _needActiveBar = false;
            _needSetColorToBar = Color.white;


            if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerIT))
            {
                if (_e.UnitT(_currentCell).HaveUnit() && !_e.UnitT(_currentCell).IsAnimal())
                {
                    _needActiveBar = true;

                    var xCordinate = (float)(_e.HpUnit(_currentCell) / HpValues.MAX);
                    _hpBarSRC.Transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                    _needSetColorToBar = _e.UnitPlayerT(_currentCell) == PlayerTypes.First ? Color.blue : _needSetColorToBar = Color.red;
                }
            }

            _hpBarSRC.SetActive(_needActiveBar);
            _hpBarSRC.SR.color = _needSetColorToBar;
        }
    }
}