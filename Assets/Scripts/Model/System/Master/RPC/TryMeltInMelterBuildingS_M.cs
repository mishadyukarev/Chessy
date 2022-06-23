using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel
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
                if (needRes[resT] > _e.PlayerInfoE(_e.WhoseMovePlayerT).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _e.PlayerInfoE(_e.WhoseMovePlayerT).ResourcesC(resT).Resources -= needRes[resT];
                }

                if (_e.LessonT == LessonTypes.NeedBuildSmelterAndMeltOre)
                {
                    _e.LessonT.SetNextLesson();
                    _e.IsSelectedCity = true;
                }

                _e.PlayerInfoE(_e.WhoseMovePlayerT).ResourcesC(ResourceTypes.Iron).Resources += EconomyValues.IRON_AFTER_MELTING;
                _e.PlayerInfoE(_e.WhoseMovePlayerT).ResourcesC(ResourceTypes.Gold).Resources += EconomyValues.GOLD_AFTER_MELTING;

                ExecuteSoundActionToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}