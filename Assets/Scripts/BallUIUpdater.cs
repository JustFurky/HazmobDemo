using UnityEngine.UI;
using UnityEngine;

public class BallUIUpdater : MonoBehaviour
{
    [SerializeField] Text _displayName;
    [SerializeField] Text _price;
    [SerializeField] Image _ballImage;

    public void SetProperties(string displayname,string price, Color32 color)
    {
        _displayName.text = displayname;
        _price.text = price+" RC";
        _ballImage.color = color;
    }
}
