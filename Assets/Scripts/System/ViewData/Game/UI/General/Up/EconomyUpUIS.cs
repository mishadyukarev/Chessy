using System.Collections.Generic;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
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
                if (Unit<UnitTC>(idx_0).Have && Unit<PlayerTC>(idx_0).Is(WhoseMoveE.CurPlayerI))
                {
                    extracts[ResourceTypes.Food] -= EconomyValues.CostFood(Unit<UnitTC>(idx_0).Unit);

                    if (CellUnitEs.CanExtract(idx_0, out var extract, out var env, out var res))
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

            if (extracts[ResourceTypes.Food] < 0) EconomyExtract<TextMPUGUIC>(ResourceTypes.Food).Text = extracts[ResourceTypes.Food].ToString();
            else EconomyExtract<TextMPUGUIC>(ResourceTypes.Food).Text = "+ " + extracts[ResourceTypes.Food].ToString();

            EconomyExtract<TextMPUGUIC>(ResourceTypes.Wood).Text = "+ " + extracts[ResourceTypes.Wood];
            EconomyExtract<TextMPUGUIC>(ResourceTypes.Ore).Text = "+ " + extracts[ResourceTypes.Ore];


            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                Economy<TextMPUGUIC>(res).Text = InventorResourcesE.Resource<AmountC>(res, curPlayer).Amount.ToString();
            }
        }
    }
}