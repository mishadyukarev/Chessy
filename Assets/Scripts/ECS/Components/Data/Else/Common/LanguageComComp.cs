using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components.Data.Else.Common
{
    internal struct LanguageComComp
    {
        private static Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>> _comLanguages;

        internal static LanguageTypes CurLanguageType { get; set; }



        internal LanguageComComp(bool needNew) : this()
        {
            if (needNew)
            {
                CurLanguageType = LanguageTypes.English;

                _comLanguages = new Dictionary<LanguageTypes, Dictionary<ComLanguageTypes, string>>();

                _comLanguages.Add(LanguageTypes.English, new Dictionary<ComLanguageTypes, string>());
                _comLanguages.Add(LanguageTypes.Russian, new Dictionary<ComLanguageTypes, string>());


                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Online, "Online");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Online, "Онлаин");

                _comLanguages[LanguageTypes.English].Add(ComLanguageTypes.Offline, "Offline");
                _comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Offline, "Одиночная");

                //_comLanguages[LanguageTypes.English].Add(ComLanguageTypes.CreatePGRoom, "Create game");
                //_comLanguages[LanguageTypes.Russian].Add(ComLanguageTypes.Online, "Offline");
            }
        }

        internal static string GetText(ComLanguageTypes comLanguageType) => _comLanguages[CurLanguageType][comLanguageType];
    }
}