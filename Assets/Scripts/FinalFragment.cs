using UnityEngine;

public class FinalFragmentActivator : MonoBehaviour
{
    void Start()
    {
        if (Score.instance != null && Score.instance.finalFragmentShouldBeActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
