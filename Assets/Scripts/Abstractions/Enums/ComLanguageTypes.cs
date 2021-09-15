using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Abstractions.Enums
{
    public enum ComLanguageTypes
    {
        None,

        Online,
        PublicGame,
        CreatePGR,
        JoinPGR,
        FriendGame,
        CreateFGR,
        JoinFGR,

        Offline,
        Training,

        Info,
        Exit,
    }
}