using UnityEngine;
using System.Collections;

public class TileClass : MonoBehaviour {
    public enum Types
    {
        Road,
        Open,
        Close,
        Final,
        Start
    }
    public Types type;

    public bool canMove;
    public bool canBuild;
    public bool respawn;
    public int respawnNumber;
    public bool final;

    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (BuildingControl.instance.buildTower)
        {
            if (tag == "Open")
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
	}
}
