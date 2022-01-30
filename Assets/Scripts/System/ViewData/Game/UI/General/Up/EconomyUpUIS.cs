using System.Collections.Generic;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    sealed class EconomyUpUIS : SystemViewAbstract, IEcsRunSystem
    {
        public EconomyUpUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var unitEs = Es.CellEs.UnitEs;
            var envEs = Es.CellEs.EnvironmentEs;


            var curPlayer = Es.WhoseMove.CurPlayerI;


            var extracts = new Dictionary<ResourceTypes, int>();
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                extracts.Add(res, default);
            }
            extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_MOVE;


            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                if (Es.CellEs.UnitEs.Main(idx_0).UnitC.Have && Es.CellEs.UnitEs.Main(idx_0).OwnerC.Is(Es.WhoseMove.CurPlayerI))
                {
                    extracts[ResourceTypes.Food] -= EconomyValues.CostFood(Es.CellEs.UnitEs.Main(idx_0).UnitC.Unit);

                    if (unitEs.CanExtract(idx_0, envEs, out var extract, out var env, out var res))
                    {
                        extracts[res] += extract;
                    }
                }
                if (Es.CellEs.BuildEs.Build(idx_0).BuildTC.Have && Es.CellEs.BuildEs.Build(idx_0).PlayerTC.Is(Es.WhoseMove.CurPlayerI))
                {
                    if (Es.CellEs.BuildEs.CanExtract(idx_0, out var extract, out var env, out var res))
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
                Economy<TextUIC>(res).Text = Es.InventorResourcesEs.Resource(res, curPlayer).Resources.Amount.ToString();
            }
        }
    }
}