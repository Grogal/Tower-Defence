using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubWave
{
    public Enemy enemy;
    public int amount;
    public int respawnNumber;
    public float timeBetweenEnemy;

}


public class WaveClass : MonoBehaviour {
    public List<SubWave> wave;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
