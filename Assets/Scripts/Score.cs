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
        ScoreValue++;
        DicPtsScore[value] = true;
    }

    public bool ScoreIsGet(int value)
    {
        return DicPtsScore[value];
    }
}
