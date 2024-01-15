using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _moveSpeed = 5f;
    [SerializeField] private BoxCollider2D _boudaries;
    [SerializeField] private Transform _fruitTransform;

    Bounds _bounds;

    float _leftBound, _rightBound;
    float _startingLeftBound, _startingRightBound;
    float _offset;

    private void Awake()
    {
        SetData();
    }

    private void Update() => MovePlayer();
   

    void SetData()
    {
        _bounds = _boudaries.bounds;

        _offset = transform.position.x - _fruitTransform.position.x;

        _leftBound = _bounds.min.x + _offset;
        _rightBound = _bounds.max.x + _offset;

        _startingLeftBound = _leftBound;
        _startingRightBound = _rightBound;
    }

    void MovePlayer()
    {
        Vector3 newPosition = transform.position + new Vector3(
        InputManager.Instance.MoveInput.x * _moveSpeed * Time.deltaTime, 0f, 0f);

        newPosition.x = Mathf.Clamp(newPosition.x, _leftBound, _rightBound);
        transform.position = newPosition; 
    }

    public void ChangeBoundary(float extraWidth)
    {
        _leftBound = _startingLeftBound;
        _rightBound = _startingRightBound;

        _leftBound += ThrowManager.Instance.Bounds.extents.x + extraWidth;
        _rightBound -= ThrowManager.Instance.Bounds.extents.x + extraWidth;
    }
}
