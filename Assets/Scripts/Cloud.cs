using UnityEngine;
using UnityEngine.SceneManagement;

public class Cloud : MonoBehaviour
{
    [Header("Paramètres de montée")]
    [SerializeField] private float liftSpeed = 1f;
    [SerializeField] private Transform playerTransform;

    [Tooltip("Position finale que doit atteindre le nuage (à définir dans l'inspecteur)")]
    [SerializeField] private Vector3 cloudTargetPosition;

    [Header("Composants")]
    [Tooltip("Ce collider doit être un BoxCollider NON trigger qui représente la surface solide du nuage.")]
    [SerializeField] private Collider solidCollider; // À assigner dans l'inspecteur

    private bool isLifting = false;

    private void Start()
    {
        if (solidCollider != null)
        {
            solidCollider.enabled = false; // On désactive le sol au départ
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
        if (Score.instance.IsScoreMax())
        {
            gameObject.SetActive(true);
        }

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
            }
        }
    }
}
