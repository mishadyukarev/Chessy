using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void TryMeltInMelterBuildingM(in Player sender)
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
                if (needRes[resT] > _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(resT).Resources -= needRes[resT];
                }

                if (_eMG.LessonT == LessonTypes.NeedBuildSmelterAndMeltOre)
                {
                    _eMG.LessonTC.SetNextLesson();
                    _eMG.IsSelectedCity = true;
                }

                _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Iron).Resources += EconomyValues.IRON_AFTER_MELTING;
                _eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).ResourcesC(ResourceTypes.Gold).Resources += EconomyValues.GOLD_AFTER_MELTING;

                _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}