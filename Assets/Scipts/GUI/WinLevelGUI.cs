using UnityEngine;
using System.Collections;

public class WinLevelGUI : MonoBehaviour
{
    public Texture starBG;
    public Texture starFG;

    public int starSize = 50;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 3 - 10, Screen.width / 4 - 50, Screen.height / 3 - 20), "You Win!");
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 80, 70, 40), "Restart"))
        {
            MainClass.instance.ReloadLevel();
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 30, 70, 40), "Exit"))
        {
            MainClass.instance.ExitToMainMenu();
        }

        GUI.DrawTexture(new Rect(Screen.width / 2 - 95, Screen.height / 2 + 15,
            starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 15,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(Screen.width / 2 + 35, Screen.height / 2 + 15,
           starSize, starSize), starBG, ScaleMode.ScaleAndCrop, true, 0);


        switch (LevelClass.instance.levelRate)
        {
            case 1:
                GUI.DrawTexture(new Rect(Screen.width / 2 - 95, Screen.height / 2 + 15,
                          starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                break;
            case 2:
                GUI.DrawTexture(new Rect(Screen.width / 2 - 95, Screen.height / 2 + 15,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 15,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                break;
            case 3:
                GUI.DrawTexture(new Rect(Screen.width / 2 - 95, Screen.height / 2 + 15,
                              starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 15,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(new Rect(Screen.width / 2 + 35, Screen.height / 2 + 15,
                                starSize, starSize), starFG, ScaleMode.ScaleAndCrop, true, 0);
                break;
        }
    }
}
