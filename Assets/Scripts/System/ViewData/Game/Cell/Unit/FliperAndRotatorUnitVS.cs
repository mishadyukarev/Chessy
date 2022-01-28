using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct FliperAndRotatorUnitVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;
                ref var corner_0 = ref Entities.CellEs.UnitEs.Else(idx_0).CornedC;

                ref var main_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extra_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);




                main_0.LocalEulerAngles = new Vector3(0, 0, 0);
                main_0.FlipX = false;
                extra_0.FlipX = false;

                if (Entities.SelectedIdxE.IdxC.Is(idx_0))
                {
                    if (unit_0.Have)
                    {
                        if (ownUnit_0.Is(Entities.WhoseMove.CurPlayerI))
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
