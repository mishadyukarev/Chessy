using ECS;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct InventorResourcesEs
    {
        readonly Dictionary<string, ResourcesInInventorE> _resources;

        string Key(in ResourceTypes res, in PlayerTypes player) => res.ToString() + player;

        public ResourcesInInventorE Resource(in ResourceTypes res, in PlayerTypes player) => _resources[Key(res, player)];
        public ResourcesInInventorE Resource(in string key) => _resources[key];

        public HashSet<string> Keys
        {
            get
            {
                var keys = new HashSet<string>();
                foreach (var item in _resources) keys.Add(item.Key);
                return keys;
            }
        }

        public InventorResourcesEs(in EcsWorld gameW)
        {
            _resources = new Dictionary<string, ResourcesInInventorE>();

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    _resources.Add(Key(res, player), new ResourcesInInventorE(res, player, gameW));
                }
            }
        }


        public bool CanBuyBuilding_Master(in BuildingTypes build, in PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                needRes.Add(res, Resource(res, player).Need(build));
                if (canCreatBuild) canCreatBuild = Resource(res, player).CanBuy(build);
            }
            return canCreatBuild;
        }

        public void BuyBuilding_Master(in BuildingTypes build, in PlayerTypes player)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Buy(build);
        }

        public bool CanBuyUnit_Master(in UnitTypes unit, in PlayerTypes player, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
            {
                needRes.Add(resType, Resource(resType, player).Need(unit));
                if (canCreatBuild) canCreatBuild = Resource(resType, player).CanBuy(unit);
            }

            return canCreatBuild;
        }
        public void BuyUnit_Master(in PlayerTypes player, in UnitTypes unit)
        {
            for (ResourceTypes resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Buy(unit);
        }

        public void TryMeltOre_Master(in Player sender, in Entities e)
        {
            var whoseMove = e.WhoseMoveE.WhoseMove.Player;


            var needRes = new Dictionary<ResourceTypes, int>();
            var canMelt = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                needRes.Add(res, Resource(res, whoseMove).NeedForMelting());

                if (canMelt) canMelt = Resource(res, whoseMove).CanBuyMelting();
            }

            if (canMelt)
            {
                for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                    Resource(res, whoseMove).BuyMelting();

                e.RpcE.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                e.RpcE.MistakeEconomyToGeneral(sender, needRes);
            }
        }

        public bool CanBuy_Master(PlayerTypes player, ResourceTypes res, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
            {
                var difAmountRes = Resource(resType, player).Resources.Amount - ResourcesInInventorValues.AmountResForBuyRes(resType);
                needRes.Add(resType, ResourcesInInventorValues.AmountResForBuyRes(resType));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyRes_Master(PlayerTypes player, ResourceTypes res)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
            {
                Resource(resType, player).Take(ResourcesInInventorValues.AmountResForBuyRes(resType));
            }

            var amount = 0;

            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: amount = 100; break;
                case ResourceTypes.Wood: amount = 100; break;
                case ResourceTypes.Ore: throw new Exception();
                case ResourceTypes.Iron: throw new Exception();
                case ResourceTypes.Gold: throw new Exception();
                default: throw new Exception();
            }

            Resource(res, player).Add(amount);
        }

        public bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, playerType).Resources.Amount - ResourcesInInventorValues.AmountResForUpgradeUnit(unitType, res);
                needRes.Add(res, ResourcesInInventorValues.AmountResForUpgradeUnit(unitType, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, playerType).Take(ResourcesInInventorValues.AmountResForUpgradeUnit(unitType, resType));
        }

        public bool CanBuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes lev, out Dictionary<ResourceTypes, int> needRes)
        {
            needRes = new Dictionary<ResourceTypes, int>();
            var canCreatBuild = true;

            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var difAmountRes = Resource(res, player).Resources.Amount - ResourcesInInventorValues.AmountResForBuyTW(tw, lev, res);
                needRes.Add(res, ResourcesInInventorValues.AmountResForBuyTW(tw, lev, res));

                if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
            }

            return canCreatBuild;
        }
        public void BuyTW(PlayerTypes player, ToolWeaponTypes tw, LevelTypes level)
        {
            for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
                Resource(resType, player).Take(ResourcesInInventorValues.AmountResForBuyTW(tw, level, resType));
        }

        public void CreateUnit_Master(in UnitTypes unit, in Player sender, in Entities e)
        {
            var playerSend = e.WhoseMoveE.WhoseMove.Player;


            if (e.WhereWorker.TryGetBuilding(BuildingTypes.City, playerSend, out var idx_city))
            {
                if (CanBuyUnit_Master(unit, playerSend, out var needRes))
                {
                    BuyUnit_Master(playerSend, unit);
                    e.InventorUnitsEs.Units(unit, LevelTypes.First, playerSend).AddUnit();

                    e.RpcE.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
                    e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                e.RpcE.SoundToGeneral(sender, ClipTypes.Mistake);
                e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
        public void BuyResources_Master(in ResourceTypes res, in Player sender, in Entities es)
        {
            var whoseMove = es.WhoseMoveE.WhoseMove.Player;

            if (es.InventorResourcesEs.CanBuy_Master(whoseMove, res, out var needRes))
            {
                es.InventorResourcesEs.BuyRes_Master(whoseMove, res);

                es.RpcE.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                es.RpcE.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}

