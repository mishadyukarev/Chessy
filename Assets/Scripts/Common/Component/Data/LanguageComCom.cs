using System.Collections.Generic;

namespace Scripts.Common
{
    public struct LanguageComCom
    {
        private static Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>> _comLanguages;
        private static Dictionary<LanguageTypes, Dictionary<GameLanguageTypes, string>> _gameLanguages;

        public static LanguageTypes CurLanguageType { get; set; }



        public LanguageComCom(LanguageTypes languageType) : this()
        {
            CurLanguageType = languageType;

            _comLanguages = new Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>>();

            _comLanguages.Add(LanguageTypes.English, new Dictionary<ComLanguageTypes, string>());
            _comLanguages.Add(LanguageTypes.Russian, new Dictionary<ComLanguageTypes, string>());
            _comLanguages.Add(LanguageTypes.Chinese, new Dictionary<ComLanguageTypes, string>());
            _comLanguages.Add(LanguageTypes.Spanish, new Dictionary<ComLanguageTypes, string>());


            _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.InfoBuy, /*"/*-Ad \n*/ "+Help the project \n +Get all future factions \n -Ad(still don't work)\n And many other) \n \n Get Premium forever!");
            //_comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.InfoBuy, /*"-Реклама \n*/ "+Помощь проекту \n +получить все будущие фракции \n \n Получить ежемесячную подписку");
            //_comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.InfoBuy, /*"-广告 \n*/ "+项目援助 \n +获取所有未来派系 \n \n 获得包月订阅");
            //_comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.InfoBuy, /*"-Publicidad \n*/ "+Ayuda al proyecto \n +Obtener todas las facciones futuras \n \n Obtener una suscripción mensual");




            _gameLanguages = new Dictionary<LanguageTypes, Dictionary<GameLanguageTypes, string>>();

            _gameLanguages.Add(LanguageTypes.English, new Dictionary<GameLanguageTypes, string>());
            _gameLanguages.Add(LanguageTypes.Russian, new Dictionary<GameLanguageTypes, string>());
            _gameLanguages.Add(LanguageTypes.Chinese, new Dictionary<GameLanguageTypes, string>());
            _gameLanguages.Add(LanguageTypes.Spanish, new Dictionary<GameLanguageTypes, string>());


            _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.YouAreWinner, "You're WINNER!");
            _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.YouAreWinner, "Ты победил!");
            _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.YouAreWinner, "你赢了！");
            _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.YouAreWinner, "¡Eres el GANADOR!");

            _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.YouAreLoser, "You're loser :(");
            _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.YouAreLoser, "Ты проиграл :(");
            _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.YouAreLoser, "你输了:(");
            _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.YouAreLoser, "Eres un perdedor :(");
        }


        public static string GetText(ComLanguageTypes comLanguageType)
        {
            if (_comLanguages[CurLanguageType].ContainsKey(comLanguageType))
            {
                return _comLanguages[CurLanguageType][comLanguageType];
            }
            else
            {
                return _comLanguages[LanguageTypes.English][comLanguageType];
            }

        }
        public static string GetText(GameLanguageTypes gameLanguageType)
        {
            if (_gameLanguages[CurLanguageType].ContainsKey(gameLanguageType))
            {
                return _gameLanguages[CurLanguageType][gameLanguageType];
            }
            else
            {
                return _gameLanguages[LanguageTypes.English][gameLanguageType];
            }
        }
    }
}