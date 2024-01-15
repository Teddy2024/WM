using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _fadePanel;
    [SerializeField] private ScoreRecord _scoreRecord;
    [SerializeField] private GameObject _paused;
    [SerializeField] private float _fadeTime = 2f;

    public event Action<int> AlphaDown;
    public int CurrentScore { get; set; }

    private void Start() 
    {
        _scoreText.text = CurrentScore.ToString("0");
        ChangePanelAlpha(1, 0);
    }

    private void OnEnable() 
    {
        _paused.SetActive(false);
        AlphaDown += DecideInPlay;
        GameManager.OnGameStateChange += ShowPaused;
    }

    private void OnDisable() 
    {
        DOTween.KillAll();
        
        AlphaDown -= DecideInPlay;
        GameManager.OnGameStateChange -= ShowPaused;
        _scoreRecord.AddToHistory(CurrentScore);
    }

    private void DecideInPlay(int start)
    {
        if(start == 0)LevelManager.Instance.GoPlayGame();
    }

    private void ShowPaused(GameState state)
    {
        if(state == GameState.ExitMenu)
        {
            _paused.SetActive(true);
        }
        else
        {
            _paused.SetActive(false);
        }
    }

    public void IncreaseScore(int amount)
    {
        CurrentScore += amount;
        _scoreText.text = CurrentScore.ToString("0");
    }

    public void ChangePanelAlpha(int start, int end)
    {
        _fadePanel.gameObject?.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Append(_fadePanel?.DOFade(start, 0.01f));
        sequence.Append(_fadePanel?.DOFade(end, _fadeTime));
        sequence.OnComplete(() => {AlphaDown?.Invoke(start);}).SetAutoKill(true).SetUpdate(true);
    }
}
