using Chessy.Model;
using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy
{
    public static class Extension
    {
        public static PlayerTypes GetPlayer(this Player player) => player.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;

        public static Player GetPlayer(this PlayerTypes playerType) => playerType == PlayerTypes.First ? PhotonNetwork.PlayerList[0] : PhotonNetwork.PlayerList[1];

        public static DirectTypes Invert(this DirectTypes dir)
        {
            switch (dir)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Up: return DirectTypes.Down;
                case DirectTypes.UpRight: return DirectTypes.DownLeft;
                case DirectTypes.Right: return DirectTypes.Left;
                case DirectTypes.RightDown: return DirectTypes.LeftUp;
                case DirectTypes.Down: return DirectTypes.Up;
                case DirectTypes.DownLeft: return DirectTypes.UpRight;
                case DirectTypes.Left: return DirectTypes.Right;
                case DirectTypes.LeftUp: return DirectTypes.RightDown;
                default: throw new Exception();
            }
        }


        public static bool IsGod(this UnitTypes unitT)
        {
            switch (unitT)
            {
                case UnitTypes.Elfemale: return true;
                case UnitTypes.Snowy: return true;
                case UnitTypes.Undead: return true;
                case UnitTypes.Hell: return true;
                default: return false;
            }
        }
        public static bool Is(this UnitTypes unitT, params UnitTypes[] units)
        {
            if (units == default) throw new Exception();

            foreach (var unit in units) if (unit == unitT) return true;
            return false;
        }
        public static bool HaveUnit(this UnitTypes unitT) => !Is(unitT, UnitTypes.None, UnitTypes.End);
        public static bool IsAnimal(this UnitTypes unitT) => unitT == UnitTypes.Wolf;
        public static bool IsMelee(this UnitTypes unitT, in ToolsWeaponsWarriorTypes mainTW)
        {
            switch (unitT)
            {
                case UnitTypes.Pawn:
                    if (mainTW == ToolsWeaponsWarriorTypes.BowCrossbow) return false;
                    break;

                case UnitTypes.Elfemale:
                    return false;

                case UnitTypes.Snowy:
                    return false;
            }
            return true;
        }

        public static PlayerTypes NextPlayer(this PlayerTypes playerT) => playerT == PlayerTypes.First ? PlayerTypes.Second : PlayerTypes.First;


        #region Lesson

        public static bool HaveLesson(this LessonTypes lessonT) => !lessonT.Is(LessonTypes.None, LessonTypes.End);
        public static bool Is(this LessonTypes lessonT, params LessonTypes[] lessonTs)
        {
            foreach (var item in lessonTs)
            {
                if (item == lessonT) return true;
            }
            return false;
        }

        #endregion


        #region Ability

        public static bool Is(this AbilityTypes abilityT, params AbilityTypes[] abils)
        {
            foreach (var abil in abils) if (abilityT == abil) return true;
            return false;
        }

        internal static void Reset(this ref AbilityTypes abilityT) => abilityT = default;

        #endregion


        #region Building

        public static bool HaveBuilding(this BuildingTypes buildingT) => !buildingT.Is(BuildingTypes.None, BuildingTypes.End);
        public static bool Is(this BuildingTypes buildingT, params BuildingTypes[] builds)
        {
            foreach (var build in builds) if (build == buildingT) return true;
            return false;
        }

        #endregion


        #region CellClick

        public static bool Is(this CellClickTypes cellClickT, params CellClickTypes[] clicks)
        {
            foreach (var click in clicks)
                if (click == cellClickT) return true;
            return false;
        }

        #endregion


        #region Cooldown

        public static bool HaveCooldown(this CooldownC cooldownC) => cooldownC.Cooldown > 0;

        #endregion


        #region Environment

        public static bool Is(this EnvironmentTypes environmetT, params EnvironmentTypes[] envs)
        {
            if (envs == default) throw new Exception();

            foreach (var env in envs) if (env == environmetT) return true;
            return false;
        }

        #endregion


        #region GameMod

        public static bool Is(this GameModeTypes gameModeT, params GameModeTypes[] gameModes)
        {
            if (gameModes == default) throw new Exception();

            foreach (var gameMode in gameModes)
                if (gameMode == gameModeT) return true;
            return false;
        }
        public static bool IsOffline(this GameModeTypes gameModeTC) => gameModeTC.Is(GameModeTypes.TrainingOffline, GameModeTypes.WithFriendOffline);
        public static bool IsOnline(this GameModeTypes gameModeTC) => !gameModeTC.IsOffline();

        #endregion


        #region Health

        public static bool IsAlive(this HealthC healthC) => healthC.Health > 0;

        #endregion


        #region WindC

        public static bool IsMaxSpeed(this WindC windC) => windC.Speed >= ValuesChessy.MAX_SPEED_WIND;
        public static bool IsMinSpeed(this WindC windC) => windC.Speed <= ValuesChessy.MIN_SPEED_WIND;

        #endregion


        #region WaterC

        public static bool HaveAnyWater(this WaterAmountC waterC) => waterC.Water > 0;

        #endregion


        #region Player

        public static bool Is(this PlayerTypes playerT, params PlayerTypes[] players)
        {
            if (players == default) throw new Exception();

            foreach (var player in players) if (player == playerT) return true;
            return false;
        }

        #endregion


        #region Condition

        public static bool HaveCondition(this ConditionUnitTypes conditionUnitT) => conditionUnitT != default;
        public static bool Is(this ConditionUnitTypes conditionUnitT, params ConditionUnitTypes[] conds)
        {
            if (conds == default) throw new Exception();

            foreach (var cond in conds) if (cond == conditionUnitT) return true;
            return false;
        }

        #endregion


        #region ArcherSide

        internal static void ToggleSide(this UnitOnCellC unitMainC) => unitMainC.IsArcherDirectedToRight = !unitMainC.IsArcherDirectedToRight;

        #endregion


        #region Level

        public static bool Is(this LevelTypes levelT, params LevelTypes[] levels)
        {
            if (levels == default) throw new Exception();

            foreach (var level in levels) if (level == levelT) return true;
            return false;
        }

        internal static bool TryUpgrade(this ref LevelTypes levelT)
        {
            if (levelT.Is(LevelTypes.Second, LevelTypes.End, LevelTypes.None)) return false;

            levelT = LevelTypes.Second;
            return true;
        }

        #endregion


        #region SceneT

        public static bool Is(this SceneTypes sceneT, params SceneTypes[] scenes)
        {
            foreach (var scene in scenes) if (scene == sceneT) return true;
            return false;
        }

        #endregion


        #region Protection

        public static bool HaveAnyProtection(this ProtectionC protectionC) => protectionC.Protection > 0;

        #endregion



        #region ToolWeaponT

        public static bool Is(this ToolsWeaponsWarriorTypes toolWeaponT, params ToolsWeaponsWarriorTypes[] tWs)
        {
            if (tWs == default) throw new Exception();

            foreach (var tw in tWs) if (tw == toolWeaponT) return true;
            return false;
        }
        public static bool HaveToolWeapon(this ToolsWeaponsWarriorTypes toolWeaponT) => !toolWeaponT.Is(ToolsWeaponsWarriorTypes.None, ToolsWeaponsWarriorTypes.End);

        #endregion

        public static bool Is(this TestModeTypes testModeT, params TestModeTypes[] testModes)
        {
            foreach (var testMode in testModes)
            {
                if (testModeT == testMode) return true;
            }
            return false;
        }


        #region SunSideT



        #endregion


        #region Book

        public static bool IsOpenedBook(this BookC bookC) => bookC.OpenedNowPageBookT > PageBookTypes.None;

        #endregion

    }
}