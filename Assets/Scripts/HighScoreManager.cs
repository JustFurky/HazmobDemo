using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public float HighScore;
    [SerializeField]private PlayFabManager _playFabManager;
    [SerializeField]private BallManager _ballManager;

    public void SetListeners()
    {
        _ballManager.GetCurrentBallScript().UpgradeHighScore += SetHighScore;
    }

    private void SetHighScore(int highScore)
    {
        HighScore = highScore;
    }
}
