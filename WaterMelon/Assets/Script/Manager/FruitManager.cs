using UnityEngine;
using UnityEngine.UI;

public class FruitManager : Singleton<FruitManager>
{
    [Header("所有水果DATA")]
    public FruitData[] FruitDatas;

    public int HightestStartingIndex = 3;

    [SerializeField] private Image _nextFruitImage;
    
    public FruitData NextFruitData { get; private set; }

    [SerializeField] private GameObject FruitVFX;
    [SerializeField] private AudioClip FruitSound;

    private void Start() 
    {
        PickNext();
    }

    public FruitData PickRandom()
    {
        int num = Random.Range(0, HightestStartingIndex + 1);
        if(num < FruitDatas.Length)
        {
            FruitData randomFruit = FruitDatas[num];
            return randomFruit;
        }
        else
        {
            return null;
        }
    }

    public void PickNext()
    {
        int num = Random.Range(0, HightestStartingIndex + 1);
        if(num < FruitDatas.Length)
        {
            FruitData nextFruit = FruitDatas[num];
            _nextFruitImage.sprite = FruitDatas[num].BastStats.icon;
            NextFruitData = nextFruit;
        }
    }

    public void FruitDie()
    {
        AudioManager.Instance.PlaySound(FruitSound);
    }
}
