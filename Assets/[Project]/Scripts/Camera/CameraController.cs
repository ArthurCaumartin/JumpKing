using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private RectTransform _screenCameraTarget;
    [SerializeField] Transform _target;
    [SerializeField] float _speed;
    private Camera _mainCamnera;
    private float _offSet;

    private void Start()
    {
        _mainCamnera = Camera.main;
        _offSet = GetOffSet();
    }

    private void Update()
    {
        Vector3 positionToGo = _target.position + new Vector3(0, -GetOffSet(), -10);
        _mainCamnera.transform.position = Vector3.Lerp(_mainCamnera.transform.position, positionToGo, Time.deltaTime * _speed);
    }

    private float GetOffSet()
    {
        Vector3 mid = _mainCamnera.ScreenToWorldPoint(new Vector2(_mainCamnera.pixelWidth, _mainCamnera.pixelHeight) / 2);
        Vector3 offSetTarget = _mainCamnera.ScreenToWorldPoint(_screenCameraTarget.position);
        // Debug.DrawRay(mid, Vector3.right, Color.blue);
        // Debug.DrawRay(offSetTarget, Vector3.right, Color.red);
        return Vector3.Distance(mid, offSetTarget);
    }

    public void SetTarget(Transform toSet)
    {
        if (_target == toSet) return;
        _target = toSet;
    }
}
