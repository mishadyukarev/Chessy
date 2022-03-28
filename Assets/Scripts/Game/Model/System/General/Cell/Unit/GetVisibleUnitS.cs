using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetVisibleUnitS : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        internal GetVisibleUnitS(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        internal void Get()
        {
            if (_cellEs.UnitTC.HaveUnit)
            {
                if (_cellEs.UnitPlayerTC.Is(PlayerTypes.None))
                {
                    if (_cellEs.UnitTC.IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var idx_1 = _cellEs.AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                                if (_cellEs.UnitTC.HaveUnit)
                                {
                                    if (eMGame.UnitPlayerTC(idx_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (eMGame.UnitPlayerTC(idx_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        _cellEs.UnitEs.ForPlayer(PlayerTypes.First).IsVisible = isVisForFirst;
                        _cellEs.UnitEs.ForPlayer(PlayerTypes.Second).IsVisible = isVisForSecond;
                    }
                }
                else
                {
                    _cellEs.UnitEs.ForPlayer(_cellEs.UnitPlayerTC.Player).IsVisible = true;

                    if (_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _cellEs.AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                            if (eMGame.UnitTC(idx_1).HaveUnit)
                            {
                                if (!eMGame.UnitPlayerTC(idx_1).Is(_cellEs.UnitPlayerTC.Player))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        _cellEs.UnitEs.ForPlayer(eMGame.NextPlayer(_cellEs.UnitPlayerTC.Player).Player).IsVisible = isVisibledNextPlayer;
                    }
                    else
                    {
                        _cellEs.UnitEs.ForPlayer(eMGame.NextPlayer(_cellEs.UnitPlayerTC.Player).Player).IsVisible = true;
                    }
                }
            }
        }
    }
}