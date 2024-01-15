using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreRecord : MonoBehaviour
{
    [SerializeField] private ScoreBar[] AllScoreBars;
    public List<int> AllScores = new List<int>();

    private void Start() 
    {
        LoadAllScore();
        CheckSort(AllScores);
        IntoBar();
    }

    public void CheckSort(List<int> scoreList)
    {
        scoreList.Sort((a, b) => b.CompareTo(a));
    }

    public void LoadAllScore()
    {
        ScoreData data = SaveSystem.LoadScore();
        AllScores = data.Scroe;
    }

    [ContextMenu("SaveAllScore000")]
    public void SaveAllScore()
    {
        SaveSystem.SaveScore(this);
    }

    public void IntoBar()
    {
        for( int i = 0; i < AllScoreBars.Length; i++)
        {
            AllScoreBars[i].UpdateScore(AllScores[i]);
        }
    }

    public void AddToHistory(int CurrentScore)
    {
        var StandByScores = new List<int>();
        StandByScores.AddRange(AllScores);
        StandByScores.Add(CurrentScore);
        CheckSort(StandByScores);

        for( int i = 0; i < AllScoreBars.Length; i++)
        {
            AllScores[i] = StandByScores[i];
        }
        SaveAllScore();
    }
}
