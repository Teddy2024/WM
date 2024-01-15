using UnityEngine;

public class ControlLine : MonoBehaviour
{
    [SerializeField] private Transform _throwTransform, _bottomTransform;

    LineRenderer _lineRenderer;
    float _topPos, _bottomPos, _x;

    private void Start() 
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() => SetLinePos();

    private void OnValidate() 
    {
        _lineRenderer = GetComponent<LineRenderer>();
        SetLinePos();
    }

    void SetLinePos()
    {
        _x = _throwTransform.position.x;
        _topPos = _throwTransform.position.y;
        _bottomPos = _bottomTransform.position.y;

        _lineRenderer.SetPosition(0, new Vector3(_x, _topPos));
        _lineRenderer.SetPosition(1, new Vector3(_x, _bottomPos));
    }
}
