using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MarketManager : MonoBehaviour
{
    [SerializeField] List<BallItem> balls = new List<BallItem>();

    [Header("References")]
    [SerializeField] private Transform uiContainer;
    [SerializeField] private GameObject itemBallPrefab;
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private BallManager _ballManager;
    [SerializeField] private CanvasManager _canvasManager;

    public void BuyItem(BaseEventData bed)
    {
        BallItem Selected = balls[(bed as PointerEventData).pointerClick.transform.GetSiblingIndex()];
        uint ballPrice = Selected.price;
        int ballIndex = (int)Selected.index;

        if (_goldManager.GoldValue >= ballPrice)
        {
            Debug.Log("Can Buy");
            _goldManager.GoldValue -= (int)ballPrice;
            SetDataBallManager(Selected.color, Selected.mass, Selected.extent, ballIndex);
        }
        else
        {
            Debug.LogError("Cant Buy");
        }
        _canvasManager.Invoke("CloseShop", 1);
    }

    public void SetDataBallManager(Color32 color, float mass, float extend, int index)
    {
        _ballManager.BallColor = color;
        _ballManager.BallMass = mass;
        _ballManager.BallExtend = extend;
        _ballManager.BallIndex = index;
    }

    public void GetCatalogItems()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), result =>
        {
            foreach (var ball in result.Catalog)
            {
                var customdata = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).DeserializeObject<Dictionary<string, object>>(ball.CustomData);

                Color32 color = Color.white;
                float mass = 0f;
                float extent = 0f;
                float index = 0f;

                if (customdata.TryGetValue("Color", out object x))
                {
                    string[] colorRGB = x.ToString().Split(',');
                    color = new Color32(System.Convert.ToByte(colorRGB[0]), System.Convert.ToByte(colorRGB[1]), System.Convert.ToByte(colorRGB[2]), 255);
                }

                if (customdata.TryGetValue("Mass", out object y))
                    mass = System.Convert.ToSingle(y);

                if (customdata.TryGetValue("Extent", out object z))
                    extent = System.Convert.ToSingle(z);

                if (customdata.TryGetValue("Index", out object w))
                    index = System.Convert.ToSingle(w);

                balls.Add(new BallItem { displayName = ball.DisplayName, color = color, mass = mass, extent = extent, price = ball.VirtualCurrencyPrices["RM"], index = index });
            }
            OnBallsDataRecevied();
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    void OnBallsDataRecevied()
    {

        foreach (var ball in balls)
        {
            GameObject newItemBall = Instantiate(itemBallPrefab, uiContainer);

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { BuyItem(eventData); });
            newItemBall.GetComponent<EventTrigger>().triggers.Add(entry);
            newItemBall.GetComponent<BallUIUpdater>().SetProperties(ball.displayName, ball.price.ToString(), ball.color);
        }
    }
}

[System.Serializable]
public struct BallItem
{
    public string displayName;
    public uint price;
    public Color32 color;
    public float mass;
    public float extent;
    public float index;
}