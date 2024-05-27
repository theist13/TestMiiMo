using UnityEngine;
[System.Serializable]
public class ResponsiveCamera
{
    private GridManager gridManager;

    public void Init(GridManager grid)
    {
        gridManager = grid;
        //Set Camera pos to center of grid
        Camera.main.transform.position = new Vector3( ((float)gridManager.width - 1) / 2, ((float)gridManager.height - 1) / 2, -10);
        //Set Orthographic to fit most of resolution
        Camera.main.orthographicSize = gridManager.width + ReverseAspectRatio();
    }

    float ReverseAspectRatio()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float aspectRatio = screenHeight / screenWidth; // screenHeight;

        Debug.Log(aspectRatio);

        return aspectRatio;
    }

}
