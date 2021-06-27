using UnityEngine.UI;

internal struct ImageComponent
{
    private Image _image;

    internal void SetImage(Image image) => _image = image;

    internal void SetActive(bool isActive) => _image.gameObject.SetActive(isActive);
}