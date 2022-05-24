using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameManager _instance;

    private const string STR_MAINMENUSCENE = "Scn_MainMenu";
    private const string STR_GAMEPLAYSCENE = "Scn_GamePlay";
    private const string STR_PREFHIGHSCROE = "High_Score";
    private const int INT_DEFAULTLIVES = 3;

    private PlayerData _playerData;
    private Ctrl_GamePlay _gamePlayControl;
    private Ctrl_UIMainMenu _mainMenuUIControl;
    public PlayerData PlayerData
    {
        get
        {
            if(_playerData == null)
            {
                _playerData = new PlayerData { Lives = INT_DEFAULTLIVES, Scores = 0, Level = 1 };
            }
            return _playerData;
        }
    }


    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    public void Play()
    {
        SceneManager.LoadScene(STR_GAMEPLAYSCENE, LoadSceneMode.Single);
        _playerData = new PlayerData { Lives = INT_DEFAULTLIVES, Scores = 0, Level = 1 };
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == STR_GAMEPLAYSCENE)
        {
            _gamePlayControl = GameObject.FindObjectOfType<Ctrl_GamePlay>();
            _gamePlayControl.StartPlay();
            return;
        }

        if(scene.name == STR_MAINMENUSCENE)
        {
            _mainMenuUIControl = GameObject.FindObjectOfType<Ctrl_UIMainMenu>();
            _mainMenuUIControl.Reset();
            return;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(STR_MAINMENUSCENE, LoadSceneMode.Single);
    }

    public void LiveLost()
    {
       if(_gamePlayControl == null)
        {
            return;
        }
        _playerData.Lives -= 1;

        if(_playerData.Lives <= 0)
        {
            _gamePlayControl.GameOver();
            int currentHighScore = GetHighScore();
            if(_playerData.Scores > currentHighScore)
            {
                PlayerPrefs.SetInt(STR_PREFHIGHSCROE, _playerData.Scores);
            }
        }
        else
        {
            StartCoroutine(nameof(DelayBallSpawn));
        }
    }

    private IEnumerator DelayBallSpawn()
    {
        yield return new WaitForSeconds(1f);
        _gamePlayControl.SpawnBall();
    }

    public void BrickDestoryed(Ctrl_Brick destroyedBrick)
    {
        if (_gamePlayControl == null)
        {
            return;
        }
        _playerData.Scores += destroyedBrick.Scores;
        _gamePlayControl.BrickDestroyed(destroyedBrick);
    }

    public void LevelCompleted()
    {
        Debug.Log("Level Completed");
        int currentHighScore = GetHighScore();
        if (_playerData.Scores > currentHighScore)
        {
            PlayerPrefs.SetInt(STR_PREFHIGHSCROE, _playerData.Scores);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(STR_PREFHIGHSCROE, 0);
    }
}

public class PlayerData
{
    public int Lives;
    public int Scores;
    public int Level;
}
