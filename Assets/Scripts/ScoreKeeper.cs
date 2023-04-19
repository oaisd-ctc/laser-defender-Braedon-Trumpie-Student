using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    private int score;
    static ScoreKeeper instance;

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Awake() 
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        ManageSingleton();
    }
    public int GetScore()
    {
        return score;
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
