using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    
    private int ScoreValue;
    [SerializeField] private int ScoreMax;
    [SerializeField] public TextMeshProUGUI ScoreText;

    private Dictionary<int, bool> DicPtsScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static Score instance;

    [SerializeField] private GameObject cloudObject; // Assign in Inspector
    private bool cloudSpawned = false;

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

    // Update is called once per frame
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

            // Si le score est max et que le nuage n'a pas encore été activé
            if (ScoreValue >= ScoreMax && !cloudSpawned)
            {
                if (cloudObject != null)
                {
                    cloudObject.SetActive(true);
                    Debug.Log("Nuage activé !");
                }
                else
                {
                    Debug.LogWarning("cloudObject non assigné dans l'inspecteur !");
                }
                cloudSpawned = true;
            }
        }
    }


    public bool ScoreIsGet(int value)
    {
        return DicPtsScore[value];
    }
}
