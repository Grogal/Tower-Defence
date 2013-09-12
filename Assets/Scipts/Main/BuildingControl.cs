using UnityEngine;
using System.Collections;

public class BuildingControl : MonoSingleton<BuildingControl> {
    public bool buildTower;
	// Use this for initialization
    public Transform prefabCommonTower;
    public Transform prefabSlowTower;
    public Transform prefabPoisonTower;
    public Transform prefabSplashTower;
    public Transform prefabAntiAirTower;
    public LayerMask placementLayerMask;

    private TowerClass tower;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (MainClass.instance.gameState != GameState.Lose && MainClass.instance.gameState != GameState.Win)
        {
            if (Input.GetKey("1"))
            {
                StartBuildMode("Common");
            }
            if (Input.GetKey("2"))
            {
                StartBuildMode("Slow");
            }
            if (Input.GetKey("3"))
            {
                StartBuildMode("Poison");
            }
            if (Input.GetKey("4"))
            {
                StartBuildMode("Splash");
            }
            if (Input.GetKey("5"))
            {
                StartBuildMode("AntiAir");
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                buildTower = false;
            }
            if (buildTower)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
                    {
                        Debug.DrawLine(ray.origin, hit.point, Color.green, 20);
                        if (hit.transform.tag == "Open")
                        {
                            if (PlayerScript.instance.gold - tower.towerCost >= 0)
                            {
                                PlayerScript.instance.gold -= tower.towerCost;
                                TowerClass temp = null;
                                temp = Instantiate(tower, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z), hit.transform.rotation) as TowerClass;
                                temp.GetComponent<TowerClass>().place = hit.transform.gameObject;
                                hit.transform.tag = "Close";
                                buildTower = false;
                            }
                            else
                            {
                                Debug.Log("Not enough gold!");
                            }
                        }
                    }
                }
            }
        }
	}

    public void StartBuildMode(string type)
    {
        TowerGUI.instance.Deactivate();
        buildTower = true;
        switch (type) {
            case "Common":
                tower = prefabCommonTower.GetComponent<TowerClass>();
                break;
            case "Slow":
                tower = prefabSlowTower.GetComponent<TowerClass>();
                break;
            case "Poison":
                tower = prefabPoisonTower.GetComponent<TowerClass>();
                break;
            case "Splash":
                tower = prefabSplashTower.GetComponent<TowerClass>();
                break;
            case "AntiAir":
                tower = prefabAntiAirTower.GetComponent<TowerClass>();
                break;
        }
    }
}
