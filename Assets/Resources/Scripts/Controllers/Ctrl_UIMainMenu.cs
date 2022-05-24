using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Text _txtHighScore;

    private const string STRFMT_HIGHSCORE = "High Score : {0}";
    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnClickBtnPlay);
    }

    private void Start()
    {
        Reset();
    }

    private void OnClickBtnPlay()
    {
        GameManager.Instance.Play();
    }

    public void Reset()
    {
        _txtHighScore.text = string.Format(STRFMT_HIGHSCORE, GameManager.Instance.GetHighScore());
    }
}
