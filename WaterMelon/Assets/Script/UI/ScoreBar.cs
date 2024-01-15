using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void UpdateScore(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }

    public void ClearScore()
    {
        _scoreText.text = null;
    }
}
