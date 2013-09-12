using UnityEngine;
using System.Collections;

public class LoseLevelGUI : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 3 - 10, Screen.width / 4 - 50, Screen.height / 3 - 20), "You Lose!");
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 70, 70, 40), "Restart"))
        {
            MainClass.instance.ReloadLevel();
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2, 70, 40), "Exit"))
        {
            MainClass.instance.ExitToMainMenu();
        }
    }
}
