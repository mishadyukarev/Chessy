using Leopotam.Ecs;
using System.Collections.Generic;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class EconomyUpUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;


            var extracts = new Dictionary<ResTypes, int>(); 
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                extracts.Add(res, 0);
                extracts[res] += InvResC.StandartAdding(res);
            }

            foreach (var idx_0 in Idxs)
            {
                if (Unit<UnitCellWC>(idx_0).CanExtract(out var extract, out var env, out var res))
                {
                    extracts[res] += extract;
                }
                if (Unit<OwnerC>(idx_0).Is(WhoseMoveC.CurPlayerI))
                {
                    extracts[ResTypes.Food] -= Unit<UnitC>(idx_0).CostFood;
                }

                if(Build<BuildCellC>(idx_0).CanExtract(out extract, out env, out res))
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
                EntityUIPool.EconomyUp<EconomyUpUIC>(res).Text = InvResC.AmountRes(res, curPlayer).ToString();
            }
        }
    }
}