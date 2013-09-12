using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelClass : MonoSingleton<LevelClass>
{
    public List<WaveClass> waves;
    public List<GameObject> respawns;

    public float timeBetweenWaves;

    private int count;
    public int currentWave;
    public int totalEnemies;
    public float timeToNextWave;

    public GameObject enemiesParent;

    public int livesFor1Star;
    public int livesFor2Star;
    public int livesFor3Star;

    public int levelRate;
    // Use this for initialization
    void Start()
    {
        currentWave = -1;
        timeToNextWave = timeBetweenWaves;
        count = 0;

        foreach (WaveClass cWave in waves)
        {
            for (int i = 0; i < cWave.wave.Count; i++)
            {
                totalEnemies += cWave.wave[i].amount;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count < waves.Count)
        {
            timeToNextWave -= Time.deltaTime;
            if (timeToNextWave <= 0 || currentWave == -1)
            {
                timeToNextWave = timeBetweenWaves;
                Respawn(count);
                count++;
            }
        }
    }

    void Respawn(int waveNumber)
    {
        for (int i = 0; i < waves[waveNumber].wave.Count; i++)
        {
            foreach (GameObject respawn in respawns)
            {
                if (waves[waveNumber].wave[i].respawnNumber == respawn.GetComponent<TileClass>().respawnNumber)
                {
                    StartCoroutine(
                        CreateEnemy(
                            waves[waveNumber].wave[i].timeBetweenEnemy,
                            waves[waveNumber].wave[i].enemy,
                            respawn,
                            waves[waveNumber].wave[i].amount)
                        );
                }
            }
        }
        currentWave = waveNumber;
    }

    IEnumerator CreateEnemy(float time, Enemy prefab, GameObject pos, int amount)
    {
        int i = 0;
        while (i < amount)
        {
            Enemy temp;
            if (prefab.name.Contains("Group"))
            {
                for (int j = 0; j < 3; j++)
                {
                    temp = Instantiate(prefab, new Vector3(pos.transform.position.x + j * .4f,
                                                           pos.transform.position.y + 1,
                                                           pos.transform.position.z),
                                                           pos.transform.rotation) as Enemy;
                    temp.transform.parent = enemiesParent.transform;
                    i++;
                }
            }
            else
            {
                temp = Instantiate(prefab, new Vector3(pos.transform.position.x,
                                                       pos.transform.position.y + 1,
                                                       pos.transform.position.z),
                                                       pos.transform.rotation) as Enemy;
                temp.transform.parent = enemiesParent.transform;
                i++;
            }
            yield return new WaitForSeconds(time);
        }
    }

    public void CheckWin()
    {
        totalEnemies--;
        if (currentWave >= waves.Count - 1 && PlayerScript.instance.lives > 0)
        {
            if (totalEnemies == 0)
            {
                PlayerScript.instance.Win();
            }
        }
    }

}
