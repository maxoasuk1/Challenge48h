using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private ObjectToWear obj;
    [SerializeField] private Quaternion rotation;
    [SerializeField] private GameObject fragement;
    private GameObject _player;

    private bool _isWear;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = null;
        _isWear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWear)
            return;
        
        if (_player is not null && Input.GetKey(KeyCode.E) && obj.IsGet())
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = rotation;
            _isWear = true;
            obj.wear();
            fragement.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
    }
}
