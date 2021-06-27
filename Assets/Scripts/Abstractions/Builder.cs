using System;
using UnityEngine;

public sealed class Builder
{
    public GameObject CreateGameObject(string name, Type[] types = default, Transform parent = default)
    {
        var gameObject = new GameObject(name);

        if (types != default)
        {
            foreach (var Type in types)
            {
                gameObject.AddComponent(Type);
            }
        }

        if (parent != default)
            gameObject.transform.SetParent(parent);

        return gameObject;
    }

    public GameObject Instantiate(GameObject gameObject) => GameObject.Instantiate(gameObject);
}
