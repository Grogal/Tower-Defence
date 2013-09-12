using UnityEngine;
using System.Collections;

public class PlayerScript : MonoSingleton<PlayerScript>
{
    public int gold;
    public int lives;

    public LayerMask towerColliderLayerMask;
    private bool selected;
    public GameObject towerGUI;
    private GameObject towerForGUI;

	// Use this for initialization
	void Start () {
        towerGUI = GameObject.FindGameObjectWithTag("Tower GUI");
	}
	
	// Update is called once per frame
	void Update () {
        if (MainClass.instance.gameState != GameState.Lose && MainClass.instance.gameState != GameState.Win)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //TowerGUI.instance.Deactivate();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, towerColliderLayerMask))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 20);
                    if (hit.transform.tag == "Tower Collider")
                    {
                        towerForGUI = hit.transform.gameObject.transform.parent.gameObject;
                        towerGUI.transform.position = towerForGUI.transform.position;
                        TowerGUI.instance.Deactivate();
                        TowerGUI.instance.Activate(towerForGUI);
                    }
                }
                else
                {
                    if (GUIUtility.hotControl == 0)
                        TowerGUI.instance.Deactivate();
                }
            }
        }
	}

    public void LoseLive(int lifeCost) {
        lives -= lifeCost;
        CheckLives();
    }

    void CheckLives()
    {
        if (lives <= 0)
        {
            Lose();
        }
    }

    public void Lose()
    {
        MainClass.instance.gameState = GameState.Lose;
        Debug.Log("End Game");
    }

    public void Win()
    {
        MainClass.instance.gameState = GameState.Win;
        RateLevel();
        switch(Application.loadedLevelName) {
            case "Level 1":
                PlayerPrefs.SetInt("Level 1 Rate", LevelClass.instance.levelRate);
                break;
            case "Level 2":
                PlayerPrefs.SetInt("Level 2 Rate", LevelClass.instance.levelRate);
                break;
        }
        Debug.Log(LevelClass.instance.levelRate);
        Debug.Log("Win Game :" + lives);
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SubstractGold(int amount)
    {
        gold -= amount;
    }

    public void RateLevel()
    {
        if (lives <= LevelClass.instance.livesFor1Star)
        {
            LevelClass.instance.levelRate = 1;
        }
        else if (lives >= LevelClass.instance.livesFor3Star)
        {
            LevelClass.instance.levelRate = 3;
        }
        else
        {
            LevelClass.instance.levelRate = 2;
        }
    }
}
