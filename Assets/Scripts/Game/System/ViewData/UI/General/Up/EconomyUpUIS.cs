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
                extracts[res] += EconomyValues.Adding(res);
            }

            foreach (var idx_0 in Idxs)
            {
                if (Unit<UnitCellC>(idx_0).CanExtract(out var extract, out var env, out var res))
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

            if (extracts[ResTypes.Food] < 0) EconomyUIC.SetAddText(ResTypes.Food, extracts[ResTypes.Food].ToString());
            else EconomyUIC.SetAddText(ResTypes.Food, "+ " + extracts[ResTypes.Food].ToString());


            EconomyUIC.SetAddText(ResTypes.Wood, "+ " + extracts[ResTypes.Wood]);
            EconomyUIC.SetAddText(ResTypes.Ore, "+ " + extracts[ResTypes.Ore]);




            EconomyUIC.SetMainText(ResTypes.Food, InvResC.AmountRes(ResTypes.Food, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Wood, InvResC.AmountRes(ResTypes.Wood, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Ore, InvResC.AmountRes(ResTypes.Ore, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Iron, InvResC.AmountRes(ResTypes.Iron, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Gold, InvResC.AmountRes(ResTypes.Gold, curPlayer).ToString());

        }
    }
}