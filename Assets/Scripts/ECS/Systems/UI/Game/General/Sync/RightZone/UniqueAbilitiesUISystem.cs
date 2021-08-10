using Leopotam.Ecs;

internal sealed class UniqueAbilitiesUISystem : IEcsRunSystem
{
    //private EcsFilter<SelectorComponent> _selectorFilter = default;
    //private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
    //private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    //private int[] XySelectedCell => _selectorFilter.Get1(0).XySelectedCell;

    public void Run()
    {
        //if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        //{
        //    if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
        //    {
        //        if (CellUnitsDataSystem.IsMine(XySelectedCell))
        //        {
        //            _unitZoneUIFilter.Get1(0).RemoveAllListenersInUniqueButton(UniqueButtonTypes.First);
        //            _unitZoneUIFilter.Get1(0).RemoveAllListenersInUniqueButton(UniqueButtonTypes.Second);
        //            _unitZoneUIFilter.Get1(0).RemoveAllListenersInUniqueButton(UniqueButtonTypes.Third);

        //            _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.Second, false);
        //            _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.Third, false);


        //            if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, XySelectedCell))
        //            {
        //                _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
        //                _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);

        //                _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, CircularAttackKing);
        //            }
        //            else
        //            {
        //                if (CellUnitsDataSystem.IsMelee(XySelectedCell))
        //                {
        //                    _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);

        //                    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
        //                    _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);

        //                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, XySelectedCell))
        //                    {
        //                        _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { Fire(XySelectedCell, XySelectedCell); });
        //                        if (CellFireDataSystem.HaveFireCom(XySelectedCell).HaveFire)
        //                        {
        //                            _unitZoneUIFilter.Get1(0).SetTextToUnique(UniqueButtonTypes.First, "Put Out FIRE");
        //                        }
        //                        else
        //                        {
        //                            _unitZoneUIFilter.Get1(0).SetTextToUnique(UniqueButtonTypes.First, "Fire forest");
        //                        }

        //                    }
        //                    else
        //                    {
        //                        _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
        //                        _unitZoneUIFilter.Get1(0).SetTextToUnique(UniqueButtonTypes.First, "Seed Forest");
        //                    }
        //                }

        //                else
        //                {
        //                    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
        //                    _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);
        //                    _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, ActiveFireSelector);
        //                }
        //            }
        //        }

        //        else
        //        {
        //            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
        //        }
        //    }
        //    else if (CellUnitsDataSystem.IsBot(XySelectedCell))
        //    {
        //        _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
        //    }
        //}

        //else
        //{
        //    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
        //}
    }

    //private void SeedEnvironment(EnvironmentTypes environmentType)
    //{
    //    if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.SeedEnvironmentToMaster(XySelectedCell, environmentType);
    //}

    //private void Fire(int[] fromXy, int[] toXy)
    //{
    //    if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.FireToMaster(fromXy, toXy);
    //}

    //private void CircularAttackKing()
    //{
    //    RPCGameSystem.CircularAttackKingToMaster(XySelectedCell);
    //}

    //private void ActiveFireSelector()
    //{
    //    if (_selectorFilter.Get1(0).SelectorType == SelectorTypes.PickFire)
    //    {
    //        _selectorFilter.Get1(0).SelectorType = SelectorTypes.StartClick;
    //    }
    //    else
    //    {
    //        _selectorFilter.Get1(0).SelectorType = SelectorTypes.PickFire;
    //    }
    //}
}
