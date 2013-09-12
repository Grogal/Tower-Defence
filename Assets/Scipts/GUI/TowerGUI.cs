using UnityEngine;
using System.Collections;

public class TowerGUI : MonoSingleton<TowerGUI>
{
    private TowerClass towerInfo;
    public int leftBorder;
    public int leftUpgBorder;
    private int groupWidth;
    private int groupHeight;
    public int boxWidth;
    public int boxHeight;
    public int boxUpgWidth;
    public int boxUpgHeight;
    // Use this for initialization
    void Start()
    {
        towerInfo = null;
        groupWidth = Screen.width / 2;
        groupHeight = Screen.height / 3 + 25;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(GameObject tower)
    {
        towerInfo = tower.GetComponent<TowerClass>();
    }

    public void Deactivate()
    {
        towerInfo = null;
    }

    void OnGUI()
    {
        if (towerInfo)
        {
            GUI.BeginGroup(new Rect(Camera.main.WorldToScreenPoint(towerInfo.transform.position).x + 20,
                Screen.height - Camera.main.WorldToScreenPoint(towerInfo.transform.position).y - (Screen.height / 3 + 10) / 2,
                groupWidth, groupHeight));

            // Main
            GUI.color = Color.white;
            GUI.Box(new Rect(0, 0, boxWidth, boxHeight), "Tower Information");

            GUI.Label(new Rect(leftBorder, 20, 250, 25), "Damage : " + towerInfo.damage);
            GUI.Label(new Rect(leftBorder, 40, 250, 25), "Attack Speed : " + towerInfo.attackSpeed);
            GUI.Label(new Rect(leftBorder, 60, 250, 25), "Attack Radius : " + towerInfo.attackRadius);

            GUI.color = Color.blue;
            switch (towerInfo.type)
            {
                case TowerClass.Types.Common:
                    GUI.Label(new Rect(leftBorder, 80, 150, 25), "Ability : None");
                    break;
                case TowerClass.Types.AntiAir:
                    GUI.Label(new Rect(leftBorder, 80, 150, 25), "Ability : None");
                    break;
                case TowerClass.Types.Poison:
                    GUI.Label(new Rect(leftBorder, 80, 250, 25), "Ability : Poison");
                    GUI.Label(new Rect(leftBorder, 100, 250, 25), "3s with " + towerInfo.abilityPower + " dmg/sec");
                    break;
                case TowerClass.Types.Slow:
                    GUI.Label(new Rect(leftBorder, 80, 150, 25), "Ability : Slow");
                    GUI.Label(new Rect(leftBorder, 100, 250, 25), "Slows down by " + (100 - towerInfo.abilityPower * 100) + "%");
                    break;
                case TowerClass.Types.Splash:
                    GUI.Label(new Rect(leftBorder, 80, 200, 25), "Ability : Area damage");
                    GUI.Label(new Rect(leftBorder, 100, 250, 25), "Deals " + (towerInfo.abilityPower * 100) + "% area damage");
                    break;
            }

            //Upgrade
            GUI.color = Color.white;
            if (towerInfo.towerLevel < towerInfo.towerMaxLevel)
            {
                GUI.Box(new Rect(boxWidth+5, 0, boxUpgWidth, boxUpgHeight), "Upgrade");
                GUI.Label(new Rect(leftUpgBorder, 20, 250, 25), "Damage : " + towerInfo.upgDamage);
                GUI.Label(new Rect(leftUpgBorder, 40, 250, 25), "Speed : " + towerInfo.upgAttackSpeed);
                GUI.Label(new Rect(leftUpgBorder, 60, 250, 25), "Radius : " + towerInfo.upgAttackRadius);

                GUI.color = Color.blue;
                switch (towerInfo.type)
                {
                    case TowerClass.Types.Common:
                        GUI.Label(new Rect(leftUpgBorder, 80, 150, 25), "Ability : None");
                        break;
                    case TowerClass.Types.AntiAir:
                        GUI.Label(new Rect(leftUpgBorder, 80, 150, 25), "Ability : None");
                        break;
                    case TowerClass.Types.Poison:
                        GUI.Label(new Rect(leftUpgBorder, 80, 250, 25), "Ability : Poison");
                        GUI.Label(new Rect(leftUpgBorder, 100, 250, 25), "3s with " + towerInfo.upgAbilityPower + " dmg/sec");
                        break;
                    case TowerClass.Types.Slow:
                        GUI.Label(new Rect(leftUpgBorder, 80, 150, 25), "Ability : Slow");
                        GUI.Label(new Rect(leftUpgBorder, 100, 250, 25), "Slows down by " + (100 - towerInfo.upgAbilityPower * 100) + "%");
                        break;
                    case TowerClass.Types.Splash:
                        GUI.Label(new Rect(leftUpgBorder, 80, 200, 25), "Ability : Area damage");
                        GUI.Label(new Rect(leftUpgBorder, 100, 250, 25), "Deals " + (towerInfo.upgAbilityPower * 100) + "% area damage");
                        break;
                }

                GUI.color = Color.green;
                if (towerInfo.ground && towerInfo.air)
                    GUI.Label(new Rect(leftUpgBorder, 120, 150, 25), "Target: all enemies");
                else if (towerInfo.ground && !towerInfo.air)
                    GUI.Label(new Rect(leftUpgBorder, 120, 150, 25), "Target: only ground");
                else
                    GUI.Label(new Rect(leftUpgBorder, 120, 150, 25), "Target: only air");
            }



            GUI.color = Color.green;
            if (towerInfo.ground && towerInfo.air)
                GUI.Label(new Rect(leftBorder, 120, 150, 25), "Target: all enemies");
            else if (towerInfo.ground && !towerInfo.air)
                GUI.Label(new Rect(leftBorder, 120, 150, 25), "Target: only ground");
            else
                GUI.Label(new Rect(leftBorder, 120, 150, 25), "Target: only air");

            GUI.color = Color.white;

            if (towerInfo.towerLevel < towerInfo.towerMaxLevel)
            {
                if (GUI.Button(new Rect(leftUpgBorder, 140, boxWidth-(leftUpgBorder - boxWidth), 25), "Upgrade - " + towerInfo.upgradeCost + "g"))
                {
                    towerInfo.UpgradeTower();
                }
            }

            if (GUI.Button(new Rect(leftBorder*2, 140, boxWidth-leftBorder*4, 25), "Sell - " + towerInfo.sellPrice + "g"))
            {
                towerInfo.SellTower();
            }

            GUI.EndGroup();
        }
    }
}
