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

            Economy<TextMPUGUIC>(ResTypes.Wood).Text = "+ " + extracts[ResTypes.Wood];
            Economy<TextMPUGUIC>(ResTypes.Ore).Text = "+ " + extracts[ResTypes.Ore];


            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                Economy<TextMPUGUIC>(res).Text = InventorResourcesE.Resource<AmountC>(res, curPlayer).Amount.ToString();
            }
        }
    }
}