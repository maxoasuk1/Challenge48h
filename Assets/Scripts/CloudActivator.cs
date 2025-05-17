using UnityEngine;

public class CloudActivator : MonoBehaviour
{
    void Start()
    {
        if (Score.instance != null && Score.instance.cloudShouldBeActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
