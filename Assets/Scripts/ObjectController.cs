using UnityEngine;

public class ObjectController : MonoBehaviour
{

    [SerializeField] private int ScoreId;
    private GameObject _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = null;
        if (Score.instance.ScoreIsGet(ScoreId))
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player is not null && Input.GetKey(KeyCode.E))
        {
            Destroy(this.gameObject);
            Score.instance.IncrementScore(ScoreId);
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
