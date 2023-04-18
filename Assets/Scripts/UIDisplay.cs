using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update() 
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
