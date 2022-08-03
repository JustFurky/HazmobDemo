using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("Text Referances")]
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Text _goldText;

    [Header("Managers")]
    [SerializeField] private GoldManager _goldManager;
    [SerializeField] private PlayFabManager _playFabManager;
    [SerializeField] private BallManager _ballManager;
    [SerializeField] private HighScoreManager _highScoreManager;

    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _retryPanel;
    public void Login()
    {
        _playFabManager.Login();
        _shopPanel.gameObject.SetActive(false);
        _menuPanel.SetActive(true);
        _inGamePanel.SetActive(false);
        _loginPanel.SetActive(false);
    }

    public void CloseShop()
    {
        _shopPanel.gameObject.SetActive(false);
        _menuPanel.SetActive(true);
        _retryPanel.SetActive(false);
    }

    public void Shop()
    {
        _shopPanel.gameObject.SetActive(true);
        _menuPanel.SetActive(false);
        _inGamePanel.SetActive(false);
        _loginPanel.SetActive(false);
    }
    public void Play()
    {
        SetListeners();
        _shopPanel.gameObject.SetActive(false);
        _menuPanel.SetActive(false);
        _inGamePanel.SetActive(true);
        _loginPanel.SetActive(false);
        _retryPanel.SetActive(false);
    }

    public void LevelFailed()
    {
        _playFabManager.SaveAppearance();
        _inGamePanel.SetActive(false);
        _retryPanel.SetActive(true);
    }

    private void SetListeners()
    {
        _ballManager.CreateBall();
        _goldManager.SetListeners();
        _highScoreManager.SetListeners();
        BallScript ballScript = _ballManager.GetCurrentBallScript();
        ballScript.AddGold += SetGoldText;
        ballScript.UpgradeHighScore += SetHighScore;
        ballScript.LevelFailed += LevelFailed;
    }

    private void SetHighScore(int highScore)
    {
        if (_highScoreText)
        {
            _highScoreText.text = highScore.ToString();
        }
    }
    private void SetGoldText()
    {
        if (_goldText)
        {
            _goldText.text = _goldManager.GoldValue.ToString();
        }
    }
}
