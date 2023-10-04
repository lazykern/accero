using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TextMeshPro _scoreText;

    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int score)
    {
        Score += score;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        Score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        _scoreText.text = "SCORE " + Score;
    }
}
