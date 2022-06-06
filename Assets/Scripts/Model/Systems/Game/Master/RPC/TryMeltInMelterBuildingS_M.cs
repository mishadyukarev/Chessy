using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class TryMeltInMelterBuildingS_M : SystemModel
    {
        internal TryMeltInMelterBuildingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryMelt(in Player sender)
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
                if (needRes[resT] > eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources -= needRes[resT];
                }

                if (eMG.LessonT == LessonTypes.NeedBuildSmelterAndMeltOre)
                {
                    eMG.LessonTC.SetNextLesson();
                    eMG.IsSelectedCity = true;
                }

                eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Iron).Resources += EconomyValues.IRON_AFTER_MELTING;
                eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Gold).Resources += EconomyValues.GOLD_AFTER_MELTING;

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}