using UnityEngine;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
                ref var corner_0 = ref Unit<CornerArcherC>(idx_0);

                ref var main_0 = ref EntityCellVPool.UnitV<UnitMainVC>(idx_0);
                ref var extra_0 = ref EntityCellVPool.UnitV<UnitExtraVC>(idx_0);


                main_0.SetLocRot(new Vector3(0, 0, 0));
                main_0.SetFlipX(false);
                extra_0.SetFlipX(false);

                if (SelIdx<IdxC>().Is(idx_0))
                {
                    if (unit_0.Have)
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
