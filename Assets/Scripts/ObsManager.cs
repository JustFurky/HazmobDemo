using UnityEngine.SceneManagement;
using UnityEngine;

public class ObsManager : MonoBehaviour
{
    //public static ObsManager Instance { get; private set; }
    public int Gold;
    public float HighScore;
    public int CurrentBall;
    [SerializeField] private PlayFabManager PlayFabManager;
    [SerializeField] private GoldManager GoldManager;
    //private void Awake()
    //{
    //if (Instance != null && Instance != this)
    //{
    //Destroy(this);
    //}
    //else
    //{
    //Instance = this;
    //}
    //}
    void Start()
    {
        //BallScript.AddGold += addGold;
    }
    //public void addGold()
    //{
    //    Gold += 10;
    //}

    public void LevelFailed()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //public void SetAppearance(int gold, float highScore, int currentBall)
    //{
    //    GoldManager.GoldValue = gold;
    //    HighScore = highScore;
    //    CurrentBall = currentBall;
    //}
}
