using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private ScoreManager scoreManager;
    private ResponsiveCamera responsiveCamera = new ResponsiveCamera();
    // Start is called before the first frame update
    void Start()
    {
        responsiveCamera.Init(gridManager);
        gridManager.Init();
        gridManager.OnAddScore += scoreManager.AddScore;
    }
}
