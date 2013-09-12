using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Texture backgroundTexture;
    public Texture foregroundTexture;
    public Texture frameTexture;

    public int healthWidth = 20;
    public int healthHeight = 5;

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
        GUI.DrawTexture(new Rect(Camera.main.WorldToScreenPoint(transform.position).x - 10, Screen.height - Camera.main.WorldToScreenPoint(transform.position).y + 10,
            healthWidth, healthHeight), frameTexture, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(Camera.main.WorldToScreenPoint(transform.position).x - 10, Screen.height - Camera.main.WorldToScreenPoint(transform.position).y + 10,
            healthWidth, healthHeight), backgroundTexture, ScaleMode.ScaleAndCrop, true, 0);

        GUI.DrawTexture(new Rect(Camera.main.WorldToScreenPoint(transform.position).x - 10, Screen.height - Camera.main.WorldToScreenPoint(transform.position).y + 10,
            healthWidth * GetComponent<Enemy>().Procent() / 100, healthHeight), foregroundTexture, ScaleMode.ScaleAndCrop, true, 0);
    }
}
