using System;
using UnityEngine;

public class BuilderManager
{
    public void CreateGameObject(out GameObject gameObject, string name, Type[] types = default, Transform parent = default)
    {
        gameObject = new GameObject(name);

        if (types != default)
        {
            foreach (var Type in types)
            {
                gameObject.AddComponent(Type);
            }
        }

        if (parent != default)
            gameObject.transform.SetParent(parent);
    }
}
