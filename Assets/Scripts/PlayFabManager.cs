using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using PlayFab.Json;

public class PlayFabManager : MonoBehaviour
{

    [SerializeField] private ObsManager _obsManager;
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private BallManager _ballManager;
    [SerializeField] private MarketManager _marketManager;
    [SerializeField] private HighScoreManager _highScoreManager;
    [SerializeField] private CanvasManager _canvasManager;

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }


    public void GetAppearance()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Gold") && result.Data.ContainsKey("HighScore") && result.Data.ContainsKey("BallIndex"))
        {
            int receivedHighScore = (int)float.Parse(result.Data["HighScore"].Value);
            int receivedGold = int.Parse(result.Data["Gold"].Value);
            int BallIndex = int.Parse(result.Data["BallIndex"].Value);
            _goldManager.GoldValue = receivedGold;
            _highScoreManager.HighScore = receivedHighScore;
            _ballManager.BallIndex = BallIndex;
        }
    }

    public void SaveAppearance()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Gold",_goldManager.GoldValue.ToString() },
                {"HighScore",_highScoreManager.HighScore.ToString() },
                {"BallIndex",_ballManager.BallIndex.ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log(result);
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Login Success");
        GetAppearance();
        _marketManager.GetCatalogItems();
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
