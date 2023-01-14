using Chessy.Model;
using System;

namespace Chessy.View.UI.Values
{
    public static class ForUITextValues
    {
        public static string GetText(in LanguageTypes languageT, in TextUIType textUIT)
        {
            return languageT switch
            {
                LanguageTypes.None => throw new Exception(),
                LanguageTypes.English => textUIT switch
                {
                    TextUIType.None => throw new Exception(),
                    TextUIType.Settings => "Settings",
                    _ => throw new Exception(),
                },
                LanguageTypes.Russian => textUIT switch
                {
                    TextUIType.None => throw new Exception(),
                    TextUIType.Settings => "Настройки",
                    _ => throw new Exception(),
                },
                LanguageTypes.Chinese => throw new Exception(),
                LanguageTypes.Spanish => throw new Exception(),
                LanguageTypes.End => throw new Exception(),
                _ => throw new Exception(),
            };
        }
    }
}