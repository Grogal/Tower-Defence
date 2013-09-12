using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private int Level1Rate;
    private int Level2Rate;

    public Texture starBG;
    public Texture starFG;

    public int starSize = 50;

    private Vector2 posFirstStar;
    private Vector2 posSecondStar;
    private Vector2 posThirdStar;
    // Use this for initialization
    void Start()
    {
        Level1Rate = PlayerPrefs.GetInt("Level 1 Rate");
        Level2Rate = PlayerPrefs.GetInt("Level 2 Rate");

        posFirstStar = new Vector2(Screen.width / 2 - 215, Screen.height / 2 + 15);
        posSecondStar = new Vector2(Screen.width / 2 - 150, Screen.height / 2 + 15);
        posThirdStar = new Vector2(Screen.width / 2 - 85, Screen.height / 2 + 15);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 150, 100), "Level 1"))
        {
            Application.LoadLevel("Level 1");
        }

        GUI.DrawTexture(new Rect(posFirstStar.x, posFirstStar.y,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(posSecondStar.x, posSecondStar.y,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(posThirdStar.x, posThirdStar.y,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        FillStars(Level1Rate, posFirstStar, posSecondStar, posThirdStar);

        if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2 - 100, 150, 100), "Level 2"))
        {
            Application.LoadLevel("Level 2");
        }

        GUI.DrawTexture(new Rect(posFirstStar.x + 300, posFirstStar.y,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(posSecondStar.x + 300, posSecondStar.y,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(posThirdStar.x + 300, posThirdStar.y,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        FillStars(Level1Rate, new Vector2(posFirstStar.x + 300, posFirstStar.y),
                              new Vector2(posSecondStar.x + 300, posSecondStar.y),
                              new Vector2(posThirdStar.x + 300, posThirdStar.y));
    }

    void FillStars(int levelRate, Vector2 pos1Star, Vector2 pos2Star, Vector2 pos3Star)
    {
        switch (levelRate)
        {
            case 1:
                GUI.DrawTexture(new Rect(pos1Star.x, pos1Star.y,
                          starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                break;
            case 2:
                GUI.DrawTexture(new Rect(pos1Star.x, pos1Star.y,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(new Rect(pos2Star.x, pos2Star.y,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                break;
            case 3:
                GUI.DrawTexture(new Rect(pos1Star.x, pos1Star.y,
                              starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(new Rect(pos2Star.x, pos2Star.y,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(new Rect(pos3Star.x, pos3Star.y,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                break;
        }
    }
}
