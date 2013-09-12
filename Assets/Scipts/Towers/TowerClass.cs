using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerClass : MonoBehaviour {
    public enum Types
    {
        Common,
        Slow,
        Poison,
        Splash,
        AntiAir
    }

    public Transform bullet;
    public Types type;
    public int towerLevel;
    public int towerMaxLevel;

    // Main
    public int damage;
    public float attackSpeed;
    public float attackRadius;
    public bool slow;
    public bool poison;
    public bool splash;
    public bool air;
    public bool ground;
    public int towerCost;
    public float abilityPower;
    public int sellPrice;
    // Main

    // Upgrade
    public int upgDamage;
    public float upgAttackSpeed;
    public float upgAttackRadius;
    public int upgTowerCost;
    private int upgSellPrice;
    public int upgradeCost;
    public float upgAbilityPower;
    // Upgrade

    public float turnSpeed;
    public float errorAmount;
    public Transform target;
    public Transform cannonBall;
    public Transform[] startPositions;
    public GameObject place;

    public List<GameObject> targets;

    private float nextFireTime;
    private float nextMoveTime;
    private Quaternion desiredRotation;
    private float aimError;

    private Transform myTransform;
	// Use this for initialization
	void Awake () {
        towerLevel = 1;
        towerMaxLevel = 2;
        sellPrice = (int)((float)towerCost / 100 * 75);
        targets = new List<GameObject>();
        myTransform = transform;
        GetComponent<SphereCollider>().radius = attackRadius;
	}

	
	// Update is called once per frame
	void Update () {
        if (!target)
            target = ChooseTarget();

        if (target)
        {
            Vector3 targetDir = target.position - cannonBall.position;
            float step = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(cannonBall.forward, targetDir, step, 0.0f);
            Debug.DrawRay(cannonBall.position, newDir*8, Color.red);
            cannonBall.rotation = Quaternion.LookRotation(newDir);

            if (Time.time >= nextFireTime)
            {
                FireBullet();
            }
        }

        
	}

    Transform ChooseTarget()
    {
        if (targets.Count > 0)
        {
            List<GameObject> temp = new List<GameObject>();
            foreach (GameObject target in targets)
            {
                if (target == null)
                    temp.Add(target);
            }
            foreach (GameObject target in temp)
            {
                targets.Remove(target);
            }
            if (targets.Count > 0)
            {
                targets[0].GetComponent<Enemy>().SetTower(this);
                return targets[0].transform;
            }
            else
            {
                return null;
            }
        }
        else
            return null;
    }
  

    void OnTriggerEnter(Collider col)
    {
        Enemy enemyInRange = col.gameObject.GetComponent<Enemy>();
        if (col.gameObject.tag == "Enemy")
        {
            if (ground && !enemyInRange.air)
                targets.Add(col.gameObject);
            else if (air && enemyInRange.air)
                targets.Add(col.gameObject);
            else if (air && ground)
                targets.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (targets.Contains(col.gameObject))
                targets.Remove(col.gameObject);

            if (target && target.gameObject == col.gameObject)
            {
                target = null;
                col.gameObject.GetComponent<Enemy>().RemoveTower(this);
            }
        }
    }

    void FireBullet()
    {
        nextFireTime = Time.time + (attackSpeed);
        foreach (Transform startPos in startPositions) {
            Transform temp;
            temp = Instantiate(bullet, startPos.position, startPos.rotation) as Transform;
            temp.GetComponent<BulletClass>().Init(this, target);
        }
    }

    public void RemoveEnemyFromTargets(Enemy enmy)
    {
        if (targets.Contains(enmy.gameObject))
            targets.Remove(enmy.gameObject);
    }

    public void SellTower()
    {
        Destroy(gameObject);
        Debug.Log(sellPrice);
        PlayerScript.instance.AddGold(sellPrice);
        place.tag = "Open";
    }

    public void UpgradeTower()
    {
        if (towerLevel < towerMaxLevel && PlayerScript.instance.gold >= upgradeCost) {
            damage = upgDamage;
            attackSpeed =upgAttackSpeed;
            attackRadius=upgAttackRadius;
            towerCost=upgTowerCost;
            abilityPower = upgAbilityPower;
            sellPrice = (int)((float)towerCost / 100 * 75);
            PlayerScript.instance.SubstractGold(upgradeCost);
            towerLevel++;
        }
    }
}
