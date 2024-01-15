using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewFruitData", menuName = "ScriptableObject/FruitData", order = 0)]
public class FruitData : ScriptableObject
{
    [Header("預製物")]
    public GameObject Fruit;
    public GameObject NoPhysicsFruit;

    [Header("參數")]
    [SerializeField] private Stats _stats;
    public Stats BastStats => _stats;
}

[Serializable]
public struct Stats
{
    public Sprite icon;
    public int Index;
    public int PointsWhenAnnihilated;
    public float FruitMass;
}
