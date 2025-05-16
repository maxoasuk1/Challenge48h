using UnityEngine;

public class ObjectToWear : MonoBehaviour
{
    [SerializeField] private float _smoothTime;
    [SerializeField] private Vector3 _inPlayerPosition;
    private GameObject _player;
    private Vector3 velocity = Vector3.zero;

    private bool _isGet;
    private bool _isWear;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = null;
        _isGet = false;
        _isWear = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_isWear)
            return;
        
        if (_isGet || (_player is not null && Input.GetKey(KeyCode.E)))
        {
            Vector3 targetPosition = _player.transform.position + _inPlayerPosition;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
            _isGet = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isGet)
            _player = null;
    }

    public bool IsGet()
    {
        return _isGet;
    }

    public void wear()
    {
        _player = null;
        _isWear = false;
        _isWear = true;
    }
}
