using System.Collections.Generic;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildEs;

using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    struct EconomyUpUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = Entities.WhoseMoveE.CurPlayerI;


            var extracts = new Dictionary<ResourceTypes, int>();
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                extracts.Add(res, default);
            }
            extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_MOVE;


            foreach (var idx_0 in Idxs)
            {
                if (Else(idx_0).UnitC.Have && CellUnitEs.Else(idx_0).OwnerC.Is(Entities.WhoseMoveE.CurPlayerI))
                {
                    extracts[ResourceTypes.Food] -= EconomyValues.CostFood(Else(idx_0).UnitC.Unit);

                    if (CellUnitEs.CanExtract(idx_0, out var extract, out var env, out var res))
                    {
                        extracts[res] += extract;
                    }
                }
                if (CellBuildEs.Build(idx_0).BuildTC.Have && CellBuildEs.Build(idx_0).PlayerTC.Is(Entities.WhoseMoveE.CurPlayerI))
                {
                    if (CellBuildEs.CanExtract(idx_0, out var extract, out var env, out var res))
                    {
                        extracts[res] += extract;
                    }
                }
            }

            if (extracts[ResourceTypes.Food] < 0) EconomyExtract<TextUIC>(ResourceTypes.Food).Text = extracts[ResourceTypes.Food].ToString();
            else EconomyExtract<TextUIC>(ResourceTypes.Food).Text = "+ " + extracts[ResourceTypes.Food].ToString();

            EconomyExtract<TextUIC>(ResourceTypes.Wood).Text = "+ " + extracts[ResourceTypes.Wood];
            EconomyExtract<TextUIC>(ResourceTypes.Ore).Text = "+ " + extracts[ResourceTypes.Ore];


            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                Economy<TextUIC>(res).Text = InventorResourcesE.Resource(res, curPlayer).Amount.ToString();
            }
        }
    }
}