using System.Collections.Generic;
using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.CellBuildE;

using static Game.Game.EntityUpUIPool;

namespace Game.Game
{
    struct EconomyUpUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveE.CurPlayerI;


            var extracts = new Dictionary<ResTypes, int>();
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                extracts.Add(res, 0);
                //extracts[res] += InvResC.StandartAdding(res);
            }

            foreach (var idx_0 in Idxs)
            {
                if (Unit<UnitCellEC>(idx_0).CanExtract(out var extract, out var env, out var res))
                {
                    extracts[res] += extract;
                }
                if (Unit<PlayerTC>(idx_0).Is(WhoseMoveE.CurPlayerI))
                {
                    extracts[ResTypes.Food] -= Unit<UnitTC>(idx_0).CostFood;
                }

                if (Build<BuildCellEC>(idx_0).CanExtract(out extract, out env, out res))
                {
                    extracts[res] += extract;
                }
            }

            //if (extracts[ResTypes.Food] < 0) EconomyUIC.SetAddText(ResTypes.Food, extracts[ResTypes.Food].ToString());
            //else EconomyUIC.SetAddText(ResTypes.Food, "+ " + extracts[ResTypes.Food].ToString());


            //EconomyUIC.SetAddText(ResTypes.Wood, "+ " + extracts[ResTypes.Wood]);
            //EconomyUIC.SetAddText(ResTypes.Ore, "+ " + extracts[ResTypes.Ore]);


            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                Economy<EconomyUpUIC>(res).Text = EntInventorResources.Resource<AmountC>(res, curPlayer).Amount.ToString();
            }
        }
    }
}