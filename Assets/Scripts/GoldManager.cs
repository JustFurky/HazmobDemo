using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int GoldValue;
    [SerializeField] private PlayFabManager playFabManager;
    [SerializeField] private BallManager _ballManager;
    public void SetListeners()
    {
        _ballManager.GetCurrentBallScript().AddGold += addGold;
    }

    private void addGold()
    {
        GoldValue += 10;
    }
}
