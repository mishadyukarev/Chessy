using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    internal struct LanguageComCom
    {
        private static Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>> _comLanguages;
        private static Dictionary<LanguageTypes, Dictionary<GameLanguageTypes, string>> _gameLanguages;

        internal static LanguageTypes CurLanguageType { get; set; }



        internal LanguageComCom(bool needNew) : this()
        {
            if (needNew)
            {
                CurLanguageType = LanguageTypes.English;

                _comLanguages = new Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>>();

                _comLanguages.Add(LanguageTypes.English, new Dictionary<ComLanguageTypes, string>());
                _comLanguages.Add(LanguageTypes.Russian, new Dictionary<ComLanguageTypes, string>());
                _comLanguages.Add(LanguageTypes.Chinese, new Dictionary<ComLanguageTypes, string>());
                _comLanguages.Add(LanguageTypes.Spanish, new Dictionary<ComLanguageTypes, string>());



                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Offline, "Offline");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Offline, "Одиночная");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.Offline, "单人游戏");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.Offline, "Solitaria");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Training, "Training");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Training, "Тренировка");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.Training, "锻炼");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.Training, "Entrenamiento");


                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Online, "Online");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Online, "Онлаин");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.Online, "多用户");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.Online, "Multijugador");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.PublicGame, "Public game");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.PublicGame, "Публичная игра");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.PublicGame, "公共游戏");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.PublicGame, "Juego público");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.CreatePGR, "Create room");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.CreatePGR, "Создать игру");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.CreatePGR, "创建游戏");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.CreatePGR, "Crear habitación");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.JoinPGR, "Join room");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.JoinPGR, "Зайти в игру");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.JoinPGR, "登录游戏");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.JoinPGR, "Unirse a la habitación");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.FriendGame, "Friend game");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.FriendGame, "Игра с другом");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.FriendGame, "与朋友一起玩");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.FriendGame, "Amigo juego");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.JoinFGR, "Join");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.JoinFGR, "Зайти");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.JoinFGR, "进");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.JoinFGR, "Unir");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.CreateFGR, "Create");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.CreateFGR, "Создать");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.CreateFGR, "要创建");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.CreateFGR, "Crear");


                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Info, "You need to destroy the king or capture the castle");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Info, "Вам нужно уничтожить короля или захватить замок");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.Info, "你需要摧毁国王或捕获城堡");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.Info, "Tienes que destruir al rey o capturar el castillo");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Exit, "Exit");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Exit, "Выход");
                _comLanguages[LanguageTypes.Chinese].Add(ComLanguageTypes.Exit, "退出");
                _comLanguages[LanguageTypes.Spanish].Add(ComLanguageTypes.Exit, "Salida");




                _gameLanguages = new Dictionary<LanguageTypes, Dictionary<GameLanguageTypes, string>>();

                _gameLanguages.Add(LanguageTypes.English, new Dictionary<GameLanguageTypes, string>());
                _gameLanguages.Add(LanguageTypes.Russian, new Dictionary<GameLanguageTypes, string>());
                _gameLanguages.Add(LanguageTypes.Chinese, new Dictionary<GameLanguageTypes, string>());
                _gameLanguages.Add(LanguageTypes.Spanish, new Dictionary<GameLanguageTypes, string>());



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.ReadyBeforeGame, "Ready");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.ReadyBeforeGame, "Готов");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.ReadyBeforeGame, "准备");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.ReadyBeforeGame, "Listos");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.WaitReady, "Wait for player");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.WaitReady, "Ждите игрока");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.WaitReady, "等待玩家");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.WaitReady, "Espera al jugador");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.JoinForFind, "For finding game:");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.JoinForFind, "Для поиска игры:");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.JoinForFind, "要搜索游戏:");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.JoinForFind, "Para encontrar juego:");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.SetKing, "King");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.SetKing, "Король");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.SetKing, "王");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.SetKing, "Rey");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedMoreResources, "Need more resources");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedMoreResources, "Нужно больше ресурсов");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.NeedMoreResources, "我们需要更多的资源");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.NeedMoreResources, "Necesita más recursos");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedMoreSteps, "Need more steps");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedMoreSteps, "Нужно больше шагов");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.NeedMoreSteps, "需要更多的步骤");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.NeedMoreSteps, "Necesita más pasos");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedOtherPlace, "Need other place");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedOtherPlace, "Нужно другое место");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.NeedOtherPlace, "我们需要另一个地方");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.NeedOtherPlace, "Necesito otro lugar");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedMoreHealth, "Need more health");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedMoreHealth, "Нужно больше здоровья");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.NeedMoreHealth, "我们需要更多的健康");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.NeedMoreHealth, "Necesita más salud");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PawnMustHaveTool, "Pawn must have tool");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PawnMustHaveTool, "Пешка должна иметь инструмент");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.PawnMustHaveTool, "典当必须有一个工具");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.PawnMustHaveTool, "El peón debe tener herramienta");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PawnHaveTool, "Pawn have tool");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PawnHaveTool, "Пешка имеет инструмент");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.PawnHaveTool, "典当有一个工具");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.PawnHaveTool, "El peón tiene herramienta");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NeedSetCity, "Need set city");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NeedSetCity, "Нужно установить город");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.NeedSetCity, "你需要设置城市");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.NeedSetCity, "Ciudad del sistema de la necesidad");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.ThatsForOtherUnit, "That's for other unit");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.ThatsForOtherUnit, "Это для другого юнита");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.ThatsForOtherUnit, "这是另一个单位");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.ThatsForOtherUnit, "Eso es para otra unidad");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.NearBorder, "Near Border");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.NearBorder, "Рядом границы");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.NearBorder, "靠近边境");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.NearBorder, "Cerca de la Frontera");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Motion, "Motion");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Motion, "Ход");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Motion, "运动");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Motion, "Movimiento");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.GiveOrTakeTool, "Give or take tool");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.GiveOrTakeTool, "Дай или возьми инструмент");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.GiveOrTakeTool, "给予或采取工具");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.GiveOrTakeTool, "Dar o tomar herramienta");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PickAdultForest, "Pick adult forest");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PickAdultForest, "Выбери взрослый лес");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.PickAdultForest, "选择一个成年森林");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.PickAdultForest, "Pick bosque adulto");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.YouAreWinner, "You're WINNER!");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.YouAreWinner, "Ты победил!");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.YouAreWinner, "你赢了！");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.YouAreWinner, "¡Eres el GANADOR!");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.YouAreLoser, "You're loser :(");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.YouAreLoser, "Ты проиграл :(");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.YouAreLoser, "你输了:(");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.YouAreLoser, "Eres un perdedor :(");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.GiveTake, "Give/Take");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.GiveTake, "Дать/Взять");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.GiveTake, "赠送/接受");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.GiveTake, "Dar/Tomar");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Done, "Done");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Done, "Готов");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Done, "准备");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Done, "Listos");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.WaitPlayer, "Wait for player");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.WaitPlayer, "Ждите игрока");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.WaitPlayer, "等待玩家");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.WaitPlayer, "Espera al jugador");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Create, "Create");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Create, "Создать");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Create, "要创建");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Create, "Crear");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Pawn, "Pawn");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Pawn, "Пешка");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Pawn, "典当");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Pawn, "Peón");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Rook, "Rook");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Rook, "Ладья");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Rook, "鲁克");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Rook, "Torre");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Bishop, "Bishop");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Bishop, "Слон");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Bishop, "主教");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Bishop, "Obispo");



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.EnvironmentInfo, "Environment info");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.EnvironmentInfo, "Инфо окружения");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.EnvironmentInfo, "环境信息");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.EnvironmentInfo, "Medio ambiente info");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Fertilizer, "Fertilizer");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Fertilizer, "Удобрение");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Fertilizer, "肥料");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Fertilizer, "Fertilizante");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Wood, "Wood");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Wood, "Дерево");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Wood, "树");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Wood, "Madera");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Ore, "Ore");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Ore, "Руда");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Ore, "矿石");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Ore, "Mineral");



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Melt, "Melt ore");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Melt, "Переплавить руду");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Melt, "融化矿石");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Melt, "Mineral de fusión");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UpgradeFarm, "Upgrade farms");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UpgradeFarm, "Улучшить фермы");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.UpgradeFarm, "改善农场");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.UpgradeFarm, "Granjas de actualización");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UpgradeWoodcutter, "Upgrade woodcutters");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UpgradeWoodcutter, "Улучшить лесорубки");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.UpgradeWoodcutter, "提高樵夫");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.UpgradeWoodcutter, "Actualización de leñadores");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UpgradeMine, "Upgrade mines");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UpgradeMine, "Улучшить шахты");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.UpgradeMine, "改善地雷");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.UpgradeMine, "Minas de actualización");



                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.ConditAbilities, "Standart");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.ConditAbilities, "Стандартные");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.ConditAbilities, "标准");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.ConditAbilities, "Standart");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Protect, "Protect");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Protect, "Защита");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Protect, "保护");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Protect, "Proteger");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Relax, "Relax");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Relax, "Отдых");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Relax, "休息");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Relax, "Relájate");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.Extract, "Extract");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.Extract, "Добыча");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.Extract, "提取");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.Extract, "Extraer");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.UniqueAbilities, "Unique");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.UniqueAbilities, "Уникальные");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.UniqueAbilities, "独特");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.UniqueAbilities, "Exclusivo");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.SeedForest, "Seed forest");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.SeedForest, "Посадить лес");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.SeedForest, "种植森林");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.SeedForest, "Bosque de semillas");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.FireForest, "Fire forest");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.FireForest, "Поджечь лес");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.FireForest, "放火烧森林");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.FireForest, "Bosque de fuego");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.PutOutFire, "PUT OUT");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.PutOutFire, "ПОТУШИТЬ");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.PutOutFire, "熄灭");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.PutOutFire, "APAGA");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.CircularAttack, "Circular attack");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.CircularAttack, "Круговая атака");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.CircularAttack, "循环攻击");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.CircularAttack, "Ataque Circular");


                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.BuildingAbilities, "Building");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.BuildingAbilities, "Постройка");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.BuildingAbilities, "建筑");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.BuildingAbilities, "Edificio");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.BuildFarm, "Farm");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.BuildFarm, "Ферма");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.BuildFarm, "农场");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.BuildFarm, "Granja");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.BuildMine, "Mine");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.BuildMine, "Шахта");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.BuildMine, "我的");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.BuildMine, "Mina");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.BuildCity, "Сity");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.BuildCity, "Город");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.BuildCity, "城市");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.BuildCity, "Ciudad");

                _gameLanguages[LanguageTypes.English].Add(GameLanguageTypes.DestroyBuilding, "Destroy");
                _gameLanguages[LanguageTypes.Russian].Add(GameLanguageTypes.DestroyBuilding, "Разрушить");
                _gameLanguages[LanguageTypes.Chinese].Add(GameLanguageTypes.DestroyBuilding, "销毁");
                _gameLanguages[LanguageTypes.Spanish].Add(GameLanguageTypes.DestroyBuilding, "Destruir");
            }
        }

        internal static string GetText(ComLanguageTypes comLanguageType)
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
        internal static string GetText(GameLanguageTypes gameLanguageType)
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