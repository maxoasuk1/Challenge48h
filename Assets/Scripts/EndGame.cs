using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private TextMeshProUGUI endMessage;
    [SerializeField] private float fadeDuration = 2f;

    private static EndGameUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        fadePanel.color = new Color(0, 0, 0, 0);
        endMessage.text = "";
    }

    public void TriggerEnd(bool victory)
    {
        string message = victory ? "Félicitations !" : "Essayez encore...";
        StartCoroutine(FadeToBlack(message));
    }

    private IEnumerator FadeToBlack(string message)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, 1);
        endMessage.text = message;
    }
}
