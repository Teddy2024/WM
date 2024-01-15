using UnityEngine;
using UnityEngine.UI;

public class ThrowManager : Singleton<ThrowManager>
{
    public GameObject CurrentFruit { get; set; }
    public FruitData CurrentFruitData { get; set; }

    [SerializeField] private Transform _fruitTransform, _parentAfterThrow;
    [SerializeField] private AudioClip ThrowSound;

    PlayerController _playerController;
    Rigidbody2D _rb;
    CircleCollider2D _circleCollider;

    public Bounds Bounds  { get; private set; }

    const float EXTRA_WIDTH = 0.02f;

    public bool CanThrow { get; set; } = true;

    private void Start() 
    {
        _playerController = GameManager.Instance.GetPlayer();
        SpawnAFruit(FruitManager.Instance.PickRandom());
    }

    private void Update() 
    {
        if(InputManager.Instance.isThrowPressed && CanThrow)
        {
            ThrowAFruit();
        }
    }

    public void SpawnAFruit(FruitData fruit)
    {
        CurrentFruitData = fruit;
        GameObject go = Instantiate(fruit.NoPhysicsFruit, _fruitTransform);
        CurrentFruit = go;
        _circleCollider = CurrentFruit.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        _playerController?.ChangeBoundary(EXTRA_WIDTH);
    }

    private void ThrowAFruit()
    {
        int index = CurrentFruitData.BastStats.Index;
        Vector3 pos = CurrentFruitData.Fruit.transform.position;
        Quaternion rot = CurrentFruitData.Fruit.transform.rotation;

        GameObject go = Instantiate(FruitManager.Instance.FruitDatas[index].Fruit, CurrentFruit.transform.position, rot);
        go.transform.SetParent(_parentAfterThrow);
        AudioManager.Instance.PlaySound(ThrowSound);
        
        Destroy(CurrentFruit);
        CurrentFruitData = null;
        CanThrow = false;
    }

}
