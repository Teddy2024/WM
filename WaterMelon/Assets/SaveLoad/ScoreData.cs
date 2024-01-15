using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoreData
{
    public List<int> Scroe;

    public ScoreData(ScoreRecord scoreRecord)
    {
        Scroe = scoreRecord.AllScores;
    }
}
