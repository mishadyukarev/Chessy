using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncCanAttackUnitOnCellVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly Color[] _needColor = new Color[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _caAttackSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];

        internal SyncCanAttackUnitOnCellVS(in SpriteRendererVC[] maxStepsSRCs, in EntitiesModel eM) : base(eM)
        {
            _caAttackSRCs = maxStepsSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
                _needColor[cellIdxCurrent] = Color.white;


                if (_e.WhereViewDataUnitC(cellIdxCurrent).HaveDataReference)
                {
                    var dataCellIdx = _e.WhereViewDataUnitC(cellIdxCurrent).DataIdxCellP;

                    if (_e.UnitVisibleC(dataCellIdx).IsVisible(_e.CurrentPlayerIT))
                    {
                        if (_e.UnitT(dataCellIdx).HaveUnit() && !_e.UnitT(dataCellIdx).IsAnimal())
                        {
                            _needActive[cellIdxCurrent] = !_e.UnitMainC(dataCellIdx).HaveCoolDownForAttackAnyUnit;

                            if (_e.UnitPlayerT(dataCellIdx).Is(PlayerTypes.First))
                            {
                                _needColor[cellIdxCurrent] = Color.blue;
                            }
                            else
                            {
                                _needColor[cellIdxCurrent] = Color.red;
                            }
                        }
                    }
                }


            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _caAttackSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
                _caAttackSRCs[cellIdxCurrent].Color = _needColor[cellIdxCurrent];
            }
        }
    }
}
