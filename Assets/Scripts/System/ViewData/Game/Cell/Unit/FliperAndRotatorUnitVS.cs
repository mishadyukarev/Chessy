using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct FliperAndRotatorUnitVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var corner_0 = ref Unit<IsCornedArcherC>(idx_0);

                ref var main_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extra_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);




                main_0.LocalEulerAngles = new Vector3(0, 0, 0);
                main_0.FlipX = false;
                extra_0.FlipX = false;

                if (SelectedIdxE.IdxC.Is(idx_0))
                {
                    if (unit_0.Have)
                    {
                        if (ownUnit_0.Is(WhoseMoveE.CurPlayerI))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                if (corner_0.IsCornered)
                                {
                                    main_0.LocalEulerAngles = new Vector3(0, 0, -90);
                                    main_0.FlipX = false;
                                }
                                else
                                {
                                    main_0.LocalEulerAngles = new Vector3(0, 0, 0);
                                    main_0.FlipX = true;
                                }
                            }
                            else
                            {
                                main_0.FlipX = true;
                                extra_0.FlipX = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
