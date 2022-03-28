using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    public sealed class MeltS_M : SystemModelGameAbs
    {
        public MeltS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Melt(in Player sender)
        {
            var needRes = new Dictionary<ResourceTypes, float>();

            needRes.Add(ResourceTypes.Food, 0);
            needRes.Add(ResourceTypes.Wood, EconomyValues.WOOD_NEED_FOR_MELTING);
            needRes.Add(ResourceTypes.Ore, EconomyValues.ORE_NEED_FOR_MELTING);
            needRes.Add(ResourceTypes.Iron, 0);
            needRes.Add(ResourceTypes.Gold, 0);

            var canBuy = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                if (needRes[resT] > e.PlayerInfoE(e.WhoseMove.Player).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    e.PlayerInfoE(e.WhoseMove.Player).ResourcesC(resT).Resources -= needRes[resT];
                }

                e.PlayerInfoE(e.WhoseMove.Player).ResourcesC(ResourceTypes.Iron).Resources += EconomyValues.IRON_AFTER_MELTING;
                e.PlayerInfoE(e.WhoseMove.Player).ResourcesC(ResourceTypes.Gold).Resources += EconomyValues.GOLD_AFTER_MELTING;

                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}