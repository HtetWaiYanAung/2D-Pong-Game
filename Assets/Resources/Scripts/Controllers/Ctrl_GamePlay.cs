using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ctrl_GamePlay : MonoBehaviour
{
    public Transform TfRightX;
    [SerializeField] private BricksManager _brickManager;
    [SerializeField] private Ctrl_UIGamePlay _uiControl;
    [SerializeField] private Ctrl_Ball _ballControl;
    [SerializeField] private GameObject _bricksParent;
    [SerializeField] private GameObject _ballsParent;

    private List<Ctrl_Brick> _bricks;
    private List<Ctrl_Ball> _balls;
    private So_BrickPos _currentLevel;

    internal void BallLost(Ctrl_Ball ballControl)
    {
        _balls.Remove(ballControl);
        Destroy(ballControl.gameObject);
        if (_balls.Count <= 0)
        {
            GameManager.Instance.LiveLost();
            _uiControl.UpdateUI();
        }
        Debug.Log("Ball Lost");
    }

    public void StartLevel(So_BrickPos level)
    {
        Debug.Log("Starting Level : " + level.name);
        _currentLevel = level;
        _uiControl.UpdateUI();
        _uiControl.HideResultPanel();
        ClearLevel();
        _brickManager.Level = _currentLevel;
        _bricks = _brickManager.GenerateBricks();
        DelayedSpawnBall();
    }

    public void DelayedSpawnBall()
    {
        StartCoroutine(nameof(SpawnBallCoroutine));
    }

    private IEnumerator SpawnBallCoroutine()
    {
        yield return new WaitForSeconds(1f);
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
            E_ResultType result = GameManager.Instance.LevelCompleted();
            ClearLevel();
            _uiControl.ShowResultPanel(result);
        }
    }

    public void GameOver()
    {
        ClearLevel();
        _uiControl.ShowResultPanel(E_ResultType.GameOver);
    }

    private void ClearLevel()
    {
        if (_balls == null)
        {
            _balls = new List<Ctrl_Ball>();
        }
        Debug.Log("Balls Count " + _balls.Count);
        for (int i = 0; i < _balls.Count; i++)
        {
            Destroy(_balls[i].gameObject);
        }
        _balls.Clear();

        if (_bricks == null)
        {
            _bricks = new List<Ctrl_Brick>();
        }
        for (int i = 0; i < _bricks.Count; i++)
        {
            Destroy(_bricks[i].gameObject);
        }
        _bricks.Clear();
        _ballControl.gameObject.SetActive(false);

        StopAllCoroutines();
    }
}
