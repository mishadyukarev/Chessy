using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var unit_0 = ref EntityPool.Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref EntityPool.Unit<OwnerC>(idx_0);
                ref var corner_0 = ref EntityPool.Unit<CornerArcherC>(idx_0);

                ref var main_0 = ref EntityVPool.UnitCellVC<UnitMainVC>(idx_0);
                ref var extra_0 = ref EntityVPool.UnitCellVC<UnitExtraVC>(idx_0);


                main_0.SetLocRot(new Vector3(0, 0, 0));
                main_0.SetFlipX(false);
                extra_0.SetFlipX(false);

                if (SelIdx.Idx == idx_0)
                {
                    if (unit_0.HaveUnit)
                    {
                        if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                if (corner_0.IsCornered)
                                {
                                    main_0.SetLocRot(new Vector3(0, 0, -90));
                                    main_0.SetFlipX(false);
                                }
                                else
                                {
                                    main_0.SetLocRot(new Vector3(0, 0, 0));
                                    main_0.SetFlipX(true);
                                }
                            }
                            else
                            {
                                main_0.SetFlipX(true);
                                extra_0.SetFlipX(true);
                            }
                        }
                    }
                }
            }
        }
    }
}
