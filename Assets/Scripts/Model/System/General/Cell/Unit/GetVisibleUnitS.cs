using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void GetVisibleUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    _unitVisibleCs[cellIdxCurrent].Set(playerT, true);
                }

                if (unitCs[cellIdxCurrent].HaveUnit)
                {

                    if (unitCs[cellIdxCurrent].UnitT.IsAnimal())
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var cell_1 = cellsByDirectAroundC[cellIdxCurrent].Get(dirT);

                                if (unitCs[cell_1].HaveUnit)
                                {
                                    if (unitCs[cell_1].PlayerT == PlayerTypes.First) isVisForFirst = true;
                                    if (unitCs[cell_1].PlayerT == PlayerTypes.Second) isVisForSecond = true;
                                }
                            }
                        }

                        _unitVisibleCs[cellIdxCurrent].Set(PlayerTypes.First, isVisForFirst);
                        _unitVisibleCs[cellIdxCurrent].Set(PlayerTypes.Second, isVisForSecond);
                    }

                    else
                    {
                        if (environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            var isVisibledNextPlayer = false;

                            foreach (var idx_1 in idxsAroundCellCs[cellIdxCurrent].IdxCellsAroundArray)
                            {
                                if (unitCs[idx_1].HaveUnit)
                                {
                                    if (!unitCs[idx_1].UnitT.IsAnimal())
                                    {
                                        if (unitCs[idx_1].PlayerT != unitCs[cellIdxCurrent].PlayerT)
                                        {
                                            isVisibledNextPlayer = true;
                                        }
                                    }
                                }
                            }

                            _unitVisibleCs[cellIdxCurrent].Set(unitCs[cellIdxCurrent].PlayerT.NextPlayer(), isVisibledNextPlayer);
                        }
                    }
                }
            }
        }
    }
}