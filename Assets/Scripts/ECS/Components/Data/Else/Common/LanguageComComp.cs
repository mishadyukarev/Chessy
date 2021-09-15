using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    internal struct LanguageComComp
    {
        private static Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>> _comLanguages;
        private static Dictionary<LanguageTypes, Dictionary<GameLanguageTypes, string>> _gameLanguages;

        internal static LanguageTypes CurLanguageType { get; set; }



        internal LanguageComComp(bool needNew) : this()
        {
            if (needNew)
            {
                CurLanguageType = LanguageTypes.English;

                _comLanguages = new Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>>();

                _comLanguages.Add(LanguageTypes.English, new Dictionary<ComLanguageTypes, string>());
                _comLanguages.Add(LanguageTypes.Russian, new Dictionary<ComLanguageTypes, string>());



                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Offline, "Offline");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Offline, "Одиночная");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Training, "Training");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Training, "Тренировка");


                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Online, "Online");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Online, "Онлаин");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.PublicGame, "Public game");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.PublicGame, "Публичная игра");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.CreatePGR, "Create room");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.CreatePGR, "Создать игру");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.JoinPGR, "Join room");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.JoinPGR, "Зайти в игру");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.FriendGame, "Friend game");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.FriendGame, "Игра с другом");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.JoinFGR, "Join");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.JoinFGR, "Зайти");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.CreateFGR, "Create");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.CreateFGR, "Создать");


                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Info, "You need to destroy the king or capture the castle");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Info, "Вам нужно уничтожить короля или захватить замок");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Exit, "Exit");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Exit, "Выход");




                _gameLanguages = new Dictionary<LanguageTypes, Dictionary<GameLanguageTypes, string>>();

                _gameLanguages.Add(LanguageTypes.English, new Dictionary<GameLanguageTypes, string>());
                _gameLanguages.Add(LanguageTypes.Russian, new Dictionary<GameLanguageTypes, string>());



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.ReadyBeforeGame, "Ready");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.ReadyBeforeGame, "Готов");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.WaitReady, "Wait player");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.WaitReady, "Ждите игрока");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.JoinForFind, "For finding game:");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.JoinForFind, "Для поиска игры:");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.SetKing, "King");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.SetKing, "Король");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedMoreResources, "Need more resources");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedMoreResources, "Нужно больше ресурсов");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedMoreSteps, "Need more steps");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedMoreSteps, "Нужно больше шагов");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedOtherPlace, "Need other place");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedOtherPlace, "Нужно другое место");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedMoreHealth, "Need more health");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedMoreHealth, "Нужно больше здоровья");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PawnMustHaveTool, "Pawn must have tool");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PawnMustHaveTool, "Пешка должна иметь инструмент");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PawnHaveTool, "Pawn have tool");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PawnHaveTool, "Пешка имеет инструмент");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedSetCity, "Need set city");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedSetCity, "Нужно установить город");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.ThatsForOtherUnit, "That's for other unit");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.ThatsForOtherUnit, "Это для другого юнита");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NearBorder, "Near Border");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NearBorder, "Рядом границы");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Motion, "Motion");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Motion, "Ход");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.GiveOrTakeTool, "Give or take tool");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.GiveOrTakeTool, "Дай или возьми инструмент");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PickAdultForest, "Pick adult forest");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PickAdultForest, "Выбери взрослый лес");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.YouAreWinner, "You're WINNER!");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.YouAreWinner, "Ты победил!");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.YouAreLoser, "You're loser :(");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.YouAreLoser, "Ты проиграл :(");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.GiveTake, "Give/Take");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.GiveTake, "Дать/Взять");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Done, "Done");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Done, "Готов");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.WaitPlayer, "Wait player");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.WaitPlayer, "Ждите игрока");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Create, "Create");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Create, "Создать");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Pawn, "Pawn");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Pawn, "Пешка");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Rook, "Rook");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Rook, "Ладья");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Bishop, "Bishop");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Bishop, "Слон");



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.EnvironmentInfo, "Environment info");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.EnvironmentInfo, "Инфо окружения");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Fertilizer, "Fertilizer");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Fertilizer, "Удобрение");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Wood, "Wood");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Wood, "Дерево");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Ore, "Ore");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Ore, "Руда");



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Melt, "Melt ore");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Melt, "Переплавить руду");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UpgradeFarm, "Upgrade Farms");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UpgradeFarm, "Улучшить фермы");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UpgradeWoodcutter, "Upgrade woodcutters");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UpgradeWoodcutter, "Улучшить лесорубки");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UpgradeMine, "Upgrade mines");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UpgradeMine, "Улучшить шахты");
            }
        }

        internal static string GetText(ComLanguageTypes comLanguageType) => _comLanguages[CurLanguageType][comLanguageType];
        internal static string GetText(GameLanguageTypes gameLanguageType) => _gameLanguages[CurLanguageType][gameLanguageType];
    }
}