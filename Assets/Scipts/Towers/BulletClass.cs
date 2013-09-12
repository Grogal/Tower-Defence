using UnityEngine;
using System.Collections;

public class BulletClass : MonoBehaviour {
    public float speed;
    public float range;

    private Transform myTransform;
    private float distance;
    public Transform enemy;
    public TowerClass tower;
	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        //myTransform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (enemy)
        {
            //myTransform.Translate(Vector3.forward * Time.deltaTime * speed);
            myTransform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
            distance += Time.deltaTime * speed;

            if (distance >= range)
                Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter( Collider col )
    {
        if (col.gameObject.tag == "Enemy" )
        {
            if (col.gameObject == enemy.gameObject) {
                Destroy(gameObject);
            }
        }
    }

    public void Init(TowerClass twr, Transform enmy) 
    {
        enemy = enmy;
        tower = twr;
    }
}
