using UnityEngine;

public class ColliderInformer : MonoBehaviour
{
    public bool WasCombineIn { get; set; }
    bool _hasCollided;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!_hasCollided && !WasCombineIn)
        {
            _hasCollided = true;
            GetComponent<FruitInfo>().isGround = _hasCollided;
            ThrowManager.Instance.CanThrow = true;
            ThrowManager.Instance.SpawnAFruit(FruitManager.Instance.NextFruitData);
            FruitManager.Instance.PickNext();
            Destroy(this);
        }
    }
}
