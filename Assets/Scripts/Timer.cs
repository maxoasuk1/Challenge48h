using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 300.0f;
    private float currentTime;

    public TextMeshProUGUI timerText;

    public static CountdownTimer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad((gameObject.transform.root.gameObject));
        }
        else
        {
            Destroy(gameObject);
        }
        currentTime = totalTime;
    }

    private void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0f;
            UpdateTimerDisplay();
            //fin du jeu
            Debug.Log("Temps écoulé !");
            FindFirstObjectByType<EndGameUI>().TriggerEnd(false);
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject timerGO = GameObject.Find("TimerText");
        if (timerGO != null)
        {
            timerText = timerGO.GetComponent<TextMeshProUGUI>();
        }
    }
}
