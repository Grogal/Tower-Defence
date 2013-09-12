using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public enum Types
    {
        Normal,
        Fast,
        Strong,
        Group,
        Fly
    }

    public Types type;
    public int currentHealth;
    public int maxHealth;
    public float speed;
    public int lifeCost;
    public bool air;
    public int gold;
    private float mainSpeed;

    private Transform myTransform;
    public List<TowerClass> targetByTower;

    private bool slow;
    private bool poison;
    private float poisonTime;

    public Collider[] hitColliders;


    private bool flag;
    // Use this for initialization
    void Start()
    {
        maxHealth = currentHealth;
        flag = true;
        mainSpeed = speed;
        targetByTower = new List<TowerClass>();
        myTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myTransform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (myTransform.position.y <= Random.Range(.9f, 1.2f) && flag)
        {
            myTransform.Translate(Vector3.up * Time.deltaTime * Random.Range(.5f, .9f));
        }
        if (myTransform.position.y > Random.Range(.9f, 1.2f) || !flag)
        {
            flag = false;
            myTransform.Translate(Vector3.down * Time.deltaTime * Random.Range(.5f, .9f));
        }
        if (myTransform.position.y <= 0.65f)
        {
            flag = true;
        }

        if (currentHealth <= 0)
        {
            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Tower");
            foreach (GameObject tower in allTowers)
            {
                tower.GetComponent<TowerClass>().RemoveEnemyFromTargets(this);
            }
            Destroy(gameObject);
            PlayerScript.instance.AddGold(gold);
            LevelClass.instance.CheckWin();
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            BulletClass bullet = col.gameObject.GetComponent<BulletClass>();

            if (bullet.enemy.gameObject == this.gameObject)
            {
                SubstractHealth(bullet.tower.damage);

                if (bullet.tower.slow)
                {
                    StartCoroutine("Freeze", bullet.tower.abilityPower);
                }

                if (bullet.tower.poison)
                {
                    poisonTime = Time.time;
                    StartCoroutine("Poison", bullet.tower.abilityPower);
                }

                if (bullet.tower.splash)
                {
                    hitColliders = Physics.OverlapSphere(myTransform.position, 2.0f);
                    foreach (Collider enemyInSplashRange in hitColliders)
                    {
                        Enemy enemy = enemyInSplashRange.GetComponent<Enemy>();
                        if (enemyInSplashRange && enemyInSplashRange.tag == "Enemy" && enemyInSplashRange.gameObject != this.gameObject && !enemy.air)
                        {
                            Debug.Log((int)(bullet.tower.damage * bullet.tower.abilityPower));
                            enemy.SubstractHealth((int)(bullet.tower.damage * bullet.tower.abilityPower));
                        }
                    }
                }
            }
        }

        if (col.gameObject.tag == "Final")
        {
            Destroy(gameObject);
            PlayerScript.instance.LoseLive(lifeCost);
            LevelClass.instance.CheckWin();

            GameObject[] allTowers = GameObject.FindGameObjectsWithTag("Tower");
            foreach (GameObject tower in allTowers)
            {
                tower.GetComponent<TowerClass>().RemoveEnemyFromTargets(this);
            }
        }


    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Road")
        {
            switch (type)
            {
                case Types.Normal:
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, col.gameObject.transform.rotation, Time.deltaTime * speed * 2);
                    break;
                case Types.Fast:
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, col.gameObject.transform.rotation, Time.deltaTime * speed * 3);
                    break;
                case Types.Fly:
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, col.gameObject.transform.rotation, Time.deltaTime * speed * 3);
                    break;
                case Types.Strong:
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, col.gameObject.transform.rotation, Time.deltaTime * speed * 2);
                    break;
                case Types.Group:
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, col.gameObject.transform.rotation, Time.deltaTime * speed * 4);
                    break;
            }
            //StartCoroutine("Freeze", 2.0f);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Road")
        {
            myTransform.rotation = col.gameObject.transform.rotation;
            //StartCoroutine("Freeze", 2.0f);
        }
    }

    public void SetTower(TowerClass tower)
    {
        targetByTower.Add(tower);
    }

    public void RemoveTower(TowerClass tower)
    {
        targetByTower.Remove(tower);
    }

    public void SubstractHealth(int count)
    {
        currentHealth -= count;
    }

    IEnumerator Freeze(float amount)
    {
        if (slow)
        {
            StopCoroutine("Freeze");
            speed = mainSpeed;
            slow = false;
        }
        slow = true;
        speed *= amount;
        //Debug.Log("Freeze Amount = " + amount + " Speed = " + speed);
        yield return new WaitForSeconds(3.0f);
        if (slow)
        {
            speed = mainSpeed;
            slow = false;
        }
    }

    IEnumerator Poison(int amount)
    {
        if (poison)
        {
            StopCoroutine("Poison");
        }
        while (poisonTime + 3 > Time.time)
        {
            //Debug.Log("Posion Amount = " + amount + " Speed = " + speed);
            poison = true;
            currentHealth -= amount;
            yield return new WaitForSeconds(.5f);
        }
    }

    public int Procent()
    {
        return (int)((100 * currentHealth) / maxHealth);
    }
}
