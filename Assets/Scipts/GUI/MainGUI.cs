using UnityEngine;
using System.Collections;

public class MainGUI : MonoSingleton<MainGUI>
{
    public GameObject mainMenu;
    public Rect commonRect;
    public Rect slowRect;
    public Rect poisonRect;
    public Rect splashRect;
    public Rect antiAirRect;
    public Rect activeRect;
    void Start()
    {
        commonRect = new Rect(Screen.width/2 - 180, Screen.height - 90, 70, 70);
        slowRect = new Rect(Screen.width / 2 - 105, Screen.height - 90, 70, 70);
        poisonRect = new Rect(Screen.width / 2 - 30, Screen.height - 90, 70, 70);
        splashRect = new Rect(Screen.width / 2 + 45, Screen.height - 90, 70, 70);
        antiAirRect = new Rect(Screen.width / 2 + 120, Screen.height - 90, 70, 70);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainClass.instance.gameState == GameState.Pause)
        {
            mainMenu.SetActive(true);
        }
        if (MainClass.instance.gameState == GameState.Play)
        {
            mainMenu.SetActive(false);
        }

        SetTowerHowerProperties(commonRect, "Common");
        SetTowerHowerProperties(slowRect, "Slow");
        SetTowerHowerProperties(poisonRect, "Poison");
        SetTowerHowerProperties(splashRect, "Splash");
        SetTowerHowerProperties(antiAirRect, "AntiAir");
    }

    void SetTowerHowerProperties(Rect rect, string towerName)
    {
        if (rect.Contains(new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z)))
        {
            TowerHowerGUI.instance.Activate(towerName);
            activeRect = rect;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(30, 50, 100, 30), "Gold = " + PlayerScript.instance.gold);
        GUI.Label(new Rect(30, 80, 100, 30), "Lives = " + PlayerScript.instance.lives);
        GUI.Label(new Rect(30, 110, 100, 30), "Total waves = " + (LevelClass.instance.waves.Count));
        if ((LevelClass.instance.currentWave + 1) != LevelClass.instance.waves.Count)
        {
            GUI.Label(new Rect(30, 140, 250, 30), "Time to next wave = " + (LevelClass.instance.timeToNextWave.ToString("#.00")));
        }
        else
        {
            GUI.Label(new Rect(30, 140, 250, 30), "Last wave!");
        }

        if (GUI.Button(commonRect, "Common \n" + "55g"))
        {
            BuildingControl.instance.StartBuildMode("Common");
        }
        if (GUI.Button(slowRect, "Slow \n" + "60g"))
        {
            BuildingControl.instance.StartBuildMode("Slow");
        }
        if (GUI.Button(poisonRect, "Poison \n" + "65g"))
        {
            BuildingControl.instance.StartBuildMode("Poison");
        }
        if (GUI.Button(splashRect, "Splash \n" + "90g"))
        {
            BuildingControl.instance.StartBuildMode("Splash");
        }
        if (GUI.Button(antiAirRect, "AntiAir \n" + "70g"))
        {
            BuildingControl.instance.StartBuildMode("AntiAir");
        }


        // next wave info
        if ((LevelClass.instance.currentWave + 1) < LevelClass.instance.waves.Count)
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(Screen.width/2 - 200, 10, 180, 40), "Next wave");
            if (LevelClass.instance.currentWave != -1)
            {
                for (int i = 0; i < LevelClass.instance.waves[LevelClass.instance.currentWave + 1].wave.Count; i++)
                {
                    if (LevelClass.instance.waves[LevelClass.instance.currentWave+1].wave.Count == 1)
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 190 + 48, 28, 150, 30),
                                                    "" + LevelClass.instance.waves[LevelClass.instance.currentWave + 1].wave[i].enemy.GetComponent<Enemy>().type
                                                    + " x"
                                                    + LevelClass.instance.waves[LevelClass.instance.currentWave + 1].wave[i].amount);
                    }
                    else
                    {
                        GUI.Label(new Rect(Screen.width / 2 - 190 + i * 95, 28, 150, 30),
                            "" + LevelClass.instance.waves[LevelClass.instance.currentWave + 1].wave[i].enemy.GetComponent<Enemy>().type
                            + " x"
                            + LevelClass.instance.waves[LevelClass.instance.currentWave + 1].wave[i].amount);
                    }
                }
            }
        }


        // current wave info
        GUI.color = Color.white;
        GUI.Box(new Rect(Screen.width/2, 10, 180, 40), "Current wave " + (LevelClass.instance.currentWave+1));
        if (LevelClass.instance.currentWave != -1)
        {
            for (int i = 0; i < LevelClass.instance.waves[LevelClass.instance.currentWave].wave.Count; i++)
            {
                if (LevelClass.instance.waves[LevelClass.instance.currentWave].wave.Count == 1)
                {
                    GUI.Label(new Rect(Screen.width/2 +10 +48, 28, 150, 30),
                        "" + LevelClass.instance.waves[LevelClass.instance.currentWave].wave[i].enemy.GetComponent<Enemy>().type
                        + " x"
                        + LevelClass.instance.waves[LevelClass.instance.currentWave].wave[i].amount);
                }
                else
                {
                    GUI.Label(new Rect(Screen.width/2 +10 + 95*i, 28, 150, 30),
                                            "" + LevelClass.instance.waves[LevelClass.instance.currentWave].wave[i].enemy.GetComponent<Enemy>().type
                                            + " x"
                                            + LevelClass.instance.waves[LevelClass.instance.currentWave].wave[i].amount);
                }
            }
        }
    }
}
