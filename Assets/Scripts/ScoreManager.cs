using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    private int displayedScore;
    public void AddScore(int amout)
    {
        score += amout;
        StartCoroutine(AnimateScoreUpdate());
    }
    private IEnumerator AnimateScoreUpdate()
    {
        float duration = 0.5f; 
        float elapsedTime = 0f; 
        int initialScore = displayedScore; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Increment the elapsed time by the time since the last frame
            displayedScore = (int)Mathf.Lerp(initialScore, score, elapsedTime / duration); // Interpolate the displayed score
            UpdateScoreText(); 
            yield return null; 
        }

        displayedScore = score; 
        UpdateScoreText(); 
    }
    private void UpdateScoreText()
    {
        scoreText.text = $"Score : {displayedScore}";
    }

}
