using Chessy.Common;
using Chessy.Common.Enum;
using Chessy.Model;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;

namespace Chessy
{
    public static class SystemStatic
    {

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
        public static void SetNextLesson(this LessonTypes lessonT)
        {
            if (lessonT == LessonTypes.End - 1)
            {
                lessonT = LessonTypes.None;
            }
            else lessonT++;
        }
        internal static void SetPreviousLesson(this LessonTypes lessonT)
        {
            lessonT--;
        }
        public static void SetEndLesson(this LessonTypes lessonT)
        {
            lessonT = LessonTypes.None;
        }

        #endregion


        #region Ability

        public static bool Is(this AbilityTypes abilityT, params AbilityTypes[] abils)
        {
            foreach (var abil in abils) if (abilityT == abil) return true;
            return false;
        }

        internal static void Reset(this AbilityTypes abilityT) => abilityT = default;

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


        #region XyC

        public static byte[] Xy(this XyCellC xyCellC) => (byte[])xyCellC.Xy.Clone();
        public static byte X(this XyCellC xyCellC) => xyCellC.Xy[0];
        public static byte Y(this XyCellC xyCellC) => xyCellC.Xy[1];

        #endregion


        #region WindC

        public static bool IsMaxSpeed(this WindC windC) => windC.Speed >= StartValues.MAX_SPEED_WIND;
        public static bool IsMinSpeed(this WindC windC) => windC.Speed <= StartValues.MIN_SPEED_WIND;

        #endregion


        #region WaterC

        public static bool HaveAnyWater(this WaterC waterC) => waterC.Water > 0;

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

        internal static void ToggleSide(this IsRightArcherC isRightArcherC) => isRightArcherC.IsRight = !isRightArcherC.IsRight;

        #endregion


        #region Level

        public static bool Is(this LevelTypes levelT, params LevelTypes[] levels)
        {
            if (levels == default) throw new Exception();

            foreach (var level in levels) if (level == levelT) return true;
            return false;
        }

        internal static bool TryUpgrade(this LevelTypes levelT)
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


        #region RiverT

        public static bool HaveRiverNear(this RiverTypes riverT) => riverT != RiverTypes.None && riverT != RiverTypes.End;

        #endregion


        #region ToolWeaponT

        public static bool Is(this ToolWeaponTypes toolWeaponT, params ToolWeaponTypes[] tWs)
        {
            if (tWs == default) throw new Exception();

            foreach (var tw in tWs) if (tw == toolWeaponT) return true;
            return false;
        }
        public static bool HaveToolWeapon(this ToolWeaponTypes toolWeaponT) => !toolWeaponT.Is(ToolWeaponTypes.None, ToolWeaponTypes.End);

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

        public static DirectTypes[] RaysSun(this SunSideTypes sunSideT)
        {
            var directs = new DirectTypes[3];
            switch (sunSideT)
            {
                case SunSideTypes.Dawn:
                    {
                        directs[0] = DirectTypes.UpRight;
                        directs[1] = DirectTypes.Right;
                        directs[2] = DirectTypes.RightDown;
                    }
                    break;
                case SunSideTypes.Sunset:
                    {
                        directs[0] = DirectTypes.LeftUp;
                        directs[1] = DirectTypes.Left;
                        directs[2] = DirectTypes.DownLeft;
                    }
                    break;
                default: throw new Exception();
            }

            return directs;
        }
        public static bool IsAcitveSun(this SunSideTypes sunSideT)
        {
            switch (sunSideT)
            {
                case SunSideTypes.Dawn: return true;
                case SunSideTypes.Center: return false;
                case SunSideTypes.Sunset: return true;
                case SunSideTypes.Night: return false;
                default: throw new Exception();
            }
        }
        public static void ToggleNext(this SunSideTypes sunSideT) => sunSideT = sunSideT == SunSideTypes.Night ? SunSideTypes.Dawn : ++sunSideT;

        #endregion


        #region Book

        public static bool IsOpenedBook(this BookC bookC) => bookC.OpenedNowPageBookT > PageBookTypes.None;

        #endregion

    }
}