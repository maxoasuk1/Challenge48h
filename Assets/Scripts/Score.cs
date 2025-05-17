using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private int ScoreValue;
    [SerializeField] private int ScoreMax;
    [SerializeField] public TextMeshProUGUI ScoreText;

    private Dictionary<int, bool> DicPtsScore;

    public static Score instance;

    public bool finalFragmentShouldBeActive = false;
    public bool cloudShouldBeActive = false;

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
    }

    void Start()
    {
        DicPtsScore = new Dictionary<int, bool>();
        for (int i = 1; i <= ScoreMax; i++)
        {
            DicPtsScore.Add(i, false);
        }
    }

    void Update()
    {
        ScoreText.text = ScoreValue.ToString() + "/" + ScoreMax.ToString();
    }

    public void IncrementScore(int value)
    {
        if (!DicPtsScore[value])
        {
            ScoreValue++;
            FindFirstObjectByType<AudioManager>().Play("Fragment");
            DicPtsScore[value] = true;

            if (ScoreValue >= 2 && !finalFragmentShouldBeActive)
            {
                finalFragmentShouldBeActive = true;
            }

            if (ScoreValue >= ScoreMax)
            {
                cloudShouldBeActive = true;
                SceneManager.LoadScene("MainLevel");
            }
        }
    }

    public bool ScoreIsGet(int value)
    {
        return DicPtsScore[value];
    }
}
