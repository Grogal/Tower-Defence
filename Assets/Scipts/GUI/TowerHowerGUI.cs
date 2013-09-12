using UnityEngine;
using System.Collections;

public class TowerHowerGUI : MonoSingleton<TowerHowerGUI> {
	// Use this for initialization
    public TowerClass commonTower;
    public TowerClass slowTower;
    public TowerClass poisonTower;
    public TowerClass splashTower;
    public TowerClass antiAirTower;

    public int leftBorder;
    public int boxWidth;
    public int boxHeight;

    private TowerClass tower;
	void Start () {
        tower = null;
	}
	
	// Update is called once per frame
	void Update () {
        tower = null;
	}

    public void Activate(string type)
    {
        switch (type)
        {
            case "Common":
                tower = commonTower;
                break;
            case "Slow":
                tower = slowTower;
                break;
            case "Poison":
                tower = poisonTower;
                break;
            case "Splash":
                tower = splashTower;
                break;
            case "AntiAir":
                tower = antiAirTower;
                break;
        }
    }

    void OnGUI()
    {
        if (tower)
        {
            Rect activeRect = MainGUI.instance.activeRect;
            GUI.BeginGroup(new Rect(activeRect.x,activeRect.y-155, activeRect.width+80,activeRect.height+120));
            GUI.color = Color.white;
            GUI.Box(new Rect(0, 0, boxWidth, boxHeight), "Tower Information");

            GUI.Label(new Rect(leftBorder, 20, 250, 25), "Damage : " + tower.damage);
            GUI.Label(new Rect(leftBorder, 40, 250, 25), "Attack Speed : " + tower.attackSpeed);
            GUI.Label(new Rect(leftBorder, 60, 250, 25), "Attack Radius : " + tower.attackRadius);

            GUI.color = Color.blue;
            switch (tower.type)
            {
                case TowerClass.Types.Common:
                    GUI.Label(new Rect(leftBorder, 80, 150, 25), "Ability : None");
                    break;
                case TowerClass.Types.AntiAir:
                    GUI.Label(new Rect(leftBorder, 80, 150, 25), "Ability : None");
                    break;
                case TowerClass.Types.Poison:
                    GUI.Label(new Rect(leftBorder, 80, 250, 25), "Ability : Poison");
                    GUI.Label(new Rect(leftBorder, 100, 250, 25), "3s with " + tower.abilityPower + " dmg/sec");
                    break;
                case TowerClass.Types.Slow:
                    GUI.Label(new Rect(leftBorder, 80, 150, 25), "Ability : Slow");
                    GUI.Label(new Rect(leftBorder, 100, 250, 25), "Slows down by " + (100 - tower.abilityPower * 100) + "%");
                    break;
                case TowerClass.Types.Splash:
                    GUI.Label(new Rect(leftBorder, 80, 200, 25), "Ability : Area damage");
                    GUI.Label(new Rect(leftBorder, 100, 250, 25), "Deals " + (tower.abilityPower * 100) + "% area damage");
                    break;
            }

            GUI.color = Color.green;
            if (tower.ground && tower.air)
                GUI.Label(new Rect(leftBorder, 120, 150, 25), "Target: all enemies");
            else if (tower.ground && !tower.air)
                GUI.Label(new Rect(leftBorder, 120, 150, 25), "Target: only ground");
            else
                GUI.Label(new Rect(leftBorder, 120, 150, 25), "Target: only air");

            GUI.EndGroup();
        }
    }
}
