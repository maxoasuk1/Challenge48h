using UnityEngine;
using UnityEngine.SceneManagement;

public class Cloud : MonoBehaviour
{
    [Header("Paramètres de montée")]
    [SerializeField] private float liftSpeed = 1f;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Vector3 cloudTargetPosition;

    [Header("Composants")]
    [SerializeField] private Collider solidCollider;

    private bool isLifting = false;

    private void Start()
    {
        if (solidCollider != null)
        {
            solidCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLifting)
        {
            playerTransform = other.transform;

            Vector3 cloudTop = transform.position;
            playerTransform.position = cloudTop + new Vector3(0f, 0.2f, 0f);

            isLifting = true;
        }
    }

    private void Update()
    {
        if (isLifting)
        {
            transform.position = Vector3.MoveTowards(transform.position, cloudTargetPosition, liftSpeed * Time.deltaTime);

            if (playerTransform != null)
            {
                Vector3 playerOffset = new Vector3(0f, 0.2f, 0f);
                playerTransform.position = transform.position + playerOffset;
            }

            if (Vector3.Distance(transform.position, cloudTargetPosition) < 0.01f)
            {
                isLifting = false;

                if (solidCollider != null)
                    solidCollider.enabled = true;

                Debug.Log("Nuage arrivé à destination !");
                FindFirstObjectByType<AudioManager>().Play("Bell");
                FindFirstObjectByType<EndGameUI>().TriggerEnd(true);
            }
        }
    }
}
