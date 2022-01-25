using System.Collections.Generic;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.CellBuildE;

using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    struct EconomyUpUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveE.CurPlayerI;


            var extracts = new Dictionary<ResourceTypes, int>();
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                extracts.Add(res, default);
            }
            extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_MOVE;


            foreach (var idx_0 in Idxs)
            {
                if (Else(idx_0).UnitC.Have && CellUnitEntities.Else(idx_0).OwnerC.Is(WhoseMoveE.CurPlayerI))
                {
                    extracts[ResourceTypes.Food] -= EconomyValues.CostFood(Else(idx_0).UnitC.Unit);

                    if (CellUnitEntities.CanExtract(idx_0, out var extract, out var env, out var res))
                    {
                        extracts[res] += extract;
                    }
                }
                if (Build<BuildingTC>(idx_0).Have && Build<PlayerTC>(idx_0).Is(WhoseMoveE.CurPlayerI))
                {
                    if (CellBuildE.CanExtract(idx_0, out var extract, out var env, out var res))
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