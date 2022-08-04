using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int GoldValue;

    [SerializeField] GameObject _coinPrefab;
    [SerializeField] private PlayFabManager playFabManager;
    [SerializeField] private BallManager _ballManager;
    [SerializeField]GameObject _lastCoin;
    Vector2 _lastCoinPos;

    public void SetListeners()
    {
        _ballManager.GetCurrentBallScript().AddGold += addGold;
    }

    private void addGold()
    {
        _lastCoinPos = _lastCoin.transform.position;
        Destroy(_lastCoin);
        _lastCoin = Instantiate(_coinPrefab);
        _lastCoin.transform.position = new Vector2(Random.Range(-2, 2), Random.Range(_lastCoinPos.y + 4, _lastCoinPos.y + 7));
        GoldValue += 10;
    }
}
