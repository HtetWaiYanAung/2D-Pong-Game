using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_UIGamePlay : MonoBehaviour
{
    [SerializeField] private Text _txtLives;
    [SerializeField] private Text _txtScores;
    [SerializeField] private Text _txtLevel;

    [SerializeField] private GameObject _goResultPanel;
    [SerializeField] private Text _txtResult;
    [SerializeField] private Text _txtResultPanelScore;
    [SerializeField] private Button _btnToMainMenu;

    private const string STRFMT_LIVES = "Lives = {0}";
    private const string STRFMT_SCORES = "Scores = {0}";
    private const string STRFMT_LEVEL = "Level = {0}";

    private const string STR_PLAYER_WIN = "Level Completed";
    private const string STR_PLAYER_LOSE = "Game Over";

    private void Awake()
    {
        _btnToMainMenu.onClick.AddListener(OnClickToMainMenu);
    }

    private void OnClickToMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }

    public void UpdateUI()
    {
        _txtLives.text = string.Format(STRFMT_LIVES, GameManager.Instance.PlayerData.Lives);
        _txtScores.text = string.Format(STRFMT_SCORES, GameManager.Instance.PlayerData.Scores);
        _txtLevel.text = string.Format(STRFMT_LEVEL, GameManager.Instance.PlayerData.Level);
    }
    public void HideResultPanel()
    {
        _goResultPanel.SetActive(false);
    }

    public void ShowResultPanel(bool isPlayerWin)
    {
        _txtResultPanelScore.text = string.Format(STRFMT_SCORES, GameManager.Instance.PlayerData.Scores);
        _txtResult.text = isPlayerWin ? STR_PLAYER_WIN : STR_PLAYER_LOSE;
        _goResultPanel.SetActive(true);
    }
}
