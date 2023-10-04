using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int score)
    {
        Score += score;
        Debug.Log($"Score: {Score}");
    }

    public void ResetScore()
    {
        Score = 0;
        Debug.Log($"Score: {Score}");
    }
}
