using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncUnitBarHpVS : SystemViewCellGameAbs
    {
        bool _needActiveBar;
        Color _needSetColorToBar;

        readonly SpriteRendererVC _hpBarSRC;

        internal SyncUnitBarHpVS(in SpriteRendererVC hpBarSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _hpBarSRC = hpBarSRC;
        }

        internal override void Sync()
        {
            _needActiveBar = false;
            _needSetColorToBar = Color.white;


            if (e.UnitVisibleC(_currentCell).IsVisible(e.CurPlayerIT))
            {
                if (e.UnitTC(_currentCell).HaveUnit && !e.UnitTC(_currentCell).IsAnimal)
                {
                    _needActiveBar = true;

                    var xCordinate = (float)(e.HpUnit(_currentCell) / HpValues.MAX);
                    _hpBarSRC.Transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                    _needSetColorToBar = e.UnitPlayerT(_currentCell) == PlayerTypes.First ? Color.blue : _needSetColorToBar = Color.red;
                }
            }

            _hpBarSRC.SetActive(_needActiveBar);
            _hpBarSRC.SR.color = _needSetColorToBar;
        }
    }
}