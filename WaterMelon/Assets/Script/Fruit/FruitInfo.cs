using UnityEngine;

public class FruitInfo : MonoBehaviour
{
    public FruitData fruitData;
    Rigidbody2D _rb;

    public int FruitIndex { get; private set; }
    public int PointsWhenAnnihilated { get; private set; }
    public bool isGround { private get; set; }
    int _layerIndex;

    private void Start() 
    {
        SetFruitData();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        CheckCollisionFruit(other);
    }

   private void OnDisable() 
   {
        FruitManager.Instance?.FruitDie();
   }

    #region 水果設定
    void SetFruitData()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.mass = fruitData.BastStats.FruitMass;
        _layerIndex = gameObject.layer;
        this.FruitIndex = fruitData.BastStats.Index;
        this.PointsWhenAnnihilated = fruitData.BastStats.PointsWhenAnnihilated;
    }
    #endregion
    #region 水果碰撞
    void CheckCollisionFruit(Collision2D other)
    {
        if(other.gameObject.layer == _layerIndex)
        {
            FruitInfo info = other.gameObject.GetComponent<FruitInfo>();
            if( info != null && info.FruitIndex == this.FruitIndex)
            {
                int thisID = gameObject.GetInstanceID();
                int otherID = other.gameObject.GetInstanceID();

                if(thisID > otherID)
                {
                    UiManager.Instance.IncreaseScore(PointsWhenAnnihilated);

                    if(FruitIndex == FruitManager.Instance.FruitDatas.Length - 1)
                    {
                        Destroy(other.gameObject);
                        Destroy(gameObject);
                    }
                    else
                    {
                        Vector3 middlePos = (transform.position + other.transform.position) * 0.5f;
                        GameObject go = Instantiate(SpawnCombinedFruit(FruitIndex), FruitManager.Instance.transform);
                        go.transform.position = middlePos;

                        ColliderInformer colliderInformer = go.GetComponent<ColliderInformer>();
                        if(colliderInformer != null)
                        {
                            colliderInformer.WasCombineIn = true;
                        }

                        Destroy(other.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    #endregion
    #region 水果輸贏判定
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.layer == 7 && isGround)
        {
            GameManager.Instance.ChangeGameState(GameState.Lose);
        }
    }
    #endregion
    #region 生成下一級水果
    GameObject SpawnCombinedFruit(int index)
    {
        GameObject go = FruitManager.Instance.FruitDatas[index + 1].Fruit;
        return go;
    }
    #endregion
}
