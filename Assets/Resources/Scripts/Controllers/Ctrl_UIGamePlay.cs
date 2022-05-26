using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_UIGamePlay : MonoBehaviour
{
    [SerializeField] private Text _txtLives;
    [SerializeField] private Text _txtScores;
    [SerializeField] private Text _txtLevel;
    [SerializeField] private GameObject _goResultPanel;
    [SerializeField] private TMP_Text _txtResult;
    [SerializeField] private Text _txtResultPanelScore;
    [SerializeField] private Button _btnToMainMenu;
    [SerializeField] private Button _btnToNextLevel;
    [SerializeField] private Button _btnBack;

    private const string STRFMT_LIVES = "Lives : {0}";
    private const string STRFMT_SCORES = "Scores : {0}";
    private const string STRFMT_LEVEL = "Level : {0}";

    private const string STR_PLAYER_WIN = "All levels cleared.";
    private const string STR_LEVEL_COMPLETED = "Level Completed.";
    private const string STR_PLAYER_LOSE = "Game Over.";

    private void Awake()
    {
        _btnToMainMenu.onClick.AddListener(OnClickToMainMenu);
        _btnBack.onClick.AddListener(OnClickToMainMenu);
        _btnToNextLevel.onClick.AddListener(OnClickToNextLevel);
    }

    private void OnClickToNextLevel()
    {
        GameManager.Instance.GoToNextLevel();
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

    public void ShowResultPanel(E_ResultType resultType)
    {
        _txtResultPanelScore.text = string.Format(STRFMT_SCORES, GameManager.Instance.PlayerData.Scores);
        string resultString = string.Empty;
        switch (resultType)
        {
            case E_ResultType.PlayerWin:
                resultString = STR_PLAYER_WIN;
                _btnToMainMenu.gameObject.SetActive(true);
                _btnToNextLevel.gameObject.SetActive(false);
                break;
            case E_ResultType.LevelCompleted:
                resultString = STR_LEVEL_COMPLETED;
                _btnToMainMenu.gameObject.SetActive(false);
                _btnToNextLevel.gameObject.SetActive(true);
                break;
            case E_ResultType.GameOver:
                _btnToMainMenu.gameObject.SetActive(true);
                _btnToNextLevel.gameObject.SetActive(false);
                resultString = STR_PLAYER_LOSE;
                break;
            default:
                break;
        }
        _txtResult.text = resultString;
        _goResultPanel.SetActive(true);
    }
}
