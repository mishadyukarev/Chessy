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
            }
        }

        internal static string GetText(ComLanguageTypes comLanguageType) => _comLanguages[CurLanguageType][comLanguageType];
        internal static string GetText(GameLanguageTypes gameLanguageType) => _gameLanguages[CurLanguageType][gameLanguageType];
    }
}