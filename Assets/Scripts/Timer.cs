using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 300.0f;
    private float currentTime;

    public TextMeshProUGUI timerText;

    private void Start()
    {
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
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
