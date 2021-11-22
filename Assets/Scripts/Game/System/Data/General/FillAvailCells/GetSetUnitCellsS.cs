using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetSetUnitCellsS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;
        private EcsFilter<UnitC> _unitF = default;
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<BuildC, OwnerC> _buldF = default;


        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                SetUnitCellsC.Clear(player);

                if (WhereBuildsC.IsSetted(BuildTypes.City, player))
                {
                    var idx_city = WhereBuildsC.Idx(BuildTypes.City, player);
                    ref var unit_city = ref _unitF.Get1(idx_city);
                    
                    var listAround = CellSpace.GetXyAround(_xyF.Get1(idx_city).Xy);

                    if(!unit_city.HaveUnit) SetUnitCellsC.AddIdxCell(player, idx_city);

                    foreach (var xy in listAround)
                    {
                        var curIdx = _xyF.GetIdxCell(xy);

                        ref var curUnitDatCom = ref _unitF.Get1(curIdx);
                        ref var curEnvDatCom = ref _envF.Get1(curIdx);

                        if (!curEnvDatCom.Have(EnvTypes.Mountain) && !curUnitDatCom.HaveUnit)
                        {
                            SetUnitCellsC.AddIdxCell(player, curIdx);
                        }
                    }
                }

                else
                {
                    foreach (byte curIdx in _xyF)
                    {
                        var xy = _xyF.Get1(curIdx).Xy;
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref _unitF.Get1(curIdx);


                        if (!curUnitDatCom.HaveUnit)
                        {
                            if (player == PlayerTypes.First)
                            {
                                if (y < 3 && x > 3 && x < 12)
                                {
                                    SetUnitCellsC.AddIdxCell(PlayerTypes.First, curIdx);
                                }
                            }
                            else
                            {
                                if (y > 7 && x > 3 && x < 12)
                                {
                                    SetUnitCellsC.AddIdxCell(PlayerTypes.Second, curIdx);
                                }
                            }
                        }
                    }
                }
            }

            foreach (byte idx_0 in _xyF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var buld_0 = ref _buldF.Get1(idx_0);
                ref var ownBuld_0 = ref _buldF.Get2(idx_0);
                ref var env_0 = ref _envF.Get1(idx_0);

                if (buld_0.Is(BuildTypes.Camp))
                {
                    if (!env_0.Have(EnvTypes.Mountain) && !unit_0.HaveUnit)
                    {
                        SetUnitCellsC.AddIdxCell(ownBuld_0.Owner, idx_0);
                    }
                }
            }
        }
    }
}
