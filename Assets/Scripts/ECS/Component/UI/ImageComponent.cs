using UnityEngine.UI;

internal struct ImageComponent
{
    internal Image Image { get; private set; }

    internal ImageComponent(Image image) => Image = image;
}