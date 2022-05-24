using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ctrl_GamePlay : MonoBehaviour
{
    public Transform TfRightX;
    [SerializeField] private Ctrl_UIGamePlay _uiControl;
    [SerializeField] private Ctrl_Ball _ballControl;
    [SerializeField] private GameObject _bricksParent;
    [SerializeField] private GameObject _ballsParent;

    private List<Ctrl_Brick> _bricks;
    private List<Ctrl_Ball> _balls;

    internal void BallLost(Ctrl_Ball ballControl)
    {
        _balls.Remove(ballControl);
        Destroy(ballControl.gameObject);
        if (_balls.Count <= 0)
        {
            GameManager.Instance.LiveLost();
            _uiControl.UpdateUI();
        }
    }

    public void StartPlay()
    {
        _ballControl.gameObject.SetActive(false);
        _uiControl.UpdateUI();
        _uiControl.HideResultPanel();
        Ctrl_Ball[] remainingBalls = _ballsParent.transform.GetComponentsInChildren<Ctrl_Ball>();
        for (int i = 0; i < remainingBalls.Length; i++)
        {
            Destroy(remainingBalls[i]);
        }
        _balls = new List<Ctrl_Ball>();

         SpawnBall();
        _bricks = _bricksParent.transform.GetComponentsInChildren<Ctrl_Brick>().ToList();
    }

    public void SpawnBall()
    {
        Ctrl_Ball spawnedBall = Instantiate(_ballControl, _ballsParent.transform, true);
        spawnedBall.Spawn();
        _balls.Add(spawnedBall);
    }

    public void BrickDestroyed(Ctrl_Brick destroyedBrick)
    {
        _uiControl.UpdateUI();
        _bricks.Remove(destroyedBrick);
        Destroy(destroyedBrick.gameObject);

        if (_bricks.Count <= 0)
        {
            GameManager.Instance.LevelCompleted();
            _uiControl.ShowResultPanel(true);
        }
    }

    public void GameOver()
    {
        _uiControl.ShowResultPanel(false);
    }
}
