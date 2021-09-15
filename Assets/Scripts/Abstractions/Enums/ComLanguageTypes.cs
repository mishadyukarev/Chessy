using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Abstractions.Enums
{
    public enum ComLanguageTypes
    {
        None,

        Online,

        PublicGame,
        CreatePGRoom,
        JoinPGRoom,

        FriendGame,
        CreateFGRoom,
        JoinFGRoom,


        Offline,

        StartWithBot,

    }
}