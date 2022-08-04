using System;
using UnityEngine;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    [Header("Ball Properties From PlayFab")]
    public int BallIndex;



    [HideInInspector] public Color32 BallColor;
    [HideInInspector] public float BallExtend;
    [HideInInspector] public float BallMass;

    [Header("Ball Object References")]
    [SerializeField] private BallScript _ball;
    [SerializeField] private Transform _ballStartPosition;
    [SerializeField] private Transform _gameZone;


    private BallScript _currentBallScript;

    public BallScript GetCurrentBallScript()
    {
        return _currentBallScript;
    }
    public void CreateBall()
    {
        _currentBallScript = Instantiate(_ball);
        _currentBallScript.transform.position = _ballStartPosition.position;
        _currentBallScript._gameZone = _gameZone;
        if (BallIndex == 0)
            _currentBallScript.SetProperties(1, 1, Color.white);
        else
            _currentBallScript.SetProperties(BallMass, BallExtend, BallColor);
    }
}