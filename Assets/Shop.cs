using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] List<Sprite> rockType = new List<Sprite>();
    float randomInt;
    [SerializeField] List<Image> shopImage = new List<Image>();
    [SerializeField] List<GameObject> shopButton = new List<GameObject>();
    [SerializeField] List<TMP_Text> shopDesc = new List<TMP_Text>();
    [SerializeField] List<int> rockCost = new List<int>();

    [Space(20)]

    [SerializeField] GameManager gameManager;

    public void OpenShop()  //End of Wave CALL THIS
    {
        for(int i = 0; i < 3; i++)
        {
            CalculateShop(i);
            shopButton[i].SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenShop();
        }
    }


    public void CloseShop()
    {

    }

    void CalculateShop(int index)
    {
        randomInt = Random.Range(0, 89);

        if(randomInt <=19 && randomInt >= 0)
        {
            shopImage[index].sprite = rockType[0];
            shopDesc[index].text = "Basic Rock, Helpful for Wackin!";
            //basic
        }else if(randomInt <= 34 && randomInt >= 20)
        {
            shopImage[index].sprite = rockType[1];
            shopDesc[index].text = "Reinforced Rock, Great tool for Extreme Beatings!";
            //Reinforced
        }
        else if (randomInt <= 49 && randomInt >= 35)
        {
            shopImage[index].sprite = rockType[2];
            shopDesc[index].text = "A Very Sticky Rock, Wouldn't want to stay near that!";
            //Slime
        }
        else if (randomInt <= 59 && randomInt >= 50)
        {
            shopImage[index].sprite = rockType[3];
            shopDesc[index].text = "A Flamin Rock, Now he is lookin kinda hot!";
            //Fire
        }
        else if (randomInt <= 69 && randomInt >= 60)
        {
            shopImage[index].sprite = rockType[4];
            shopDesc[index].text = "A Chill Rock, Very slippery but might slow enemies down!";
            //Ice
        }
        else if (randomInt <= 79 && randomInt >= 70)
        {
            shopImage[index].sprite = rockType[5];
            shopDesc[index].text = "A Shocking Rock, Might paralized some enemies for a bit!";
            //Lightning
        }
        else if(randomInt <= 89 && randomInt >= 80)
        {
            shopImage[index].sprite = rockType[6];
            shopDesc[index].text = "A Undead Rock, Might steal some health to give you!";
            //Vampire
        }
    }

    public void Purchase1()
    {
        int rock = 0;
        for(int i = 0; i < rockType.Count; i++)
        {
            if(shopImage[0].sprite == rockType[i])
            {
                rock = i;
                break;
            }
        }
        if (RockSelection(rock))
        {
            shopButton[0].SetActive(false);
        }

    }

    public void Purchase2()
    {
        int rock = 0;
        for (int i = 0; i < rockType.Count; i++)
        {
            if (shopImage[1].sprite == rockType[i])
            {
                rock = i;
                break;
            }
        }
        if (RockSelection(rock))
        {
            shopButton[1].SetActive(false);
        }
    }


    public void Purchase3()
    {
        int rock = 0;
        for (int i = 0; i < rockType.Count; i++)
        {
            if (shopImage[2].sprite == rockType[i])
            {
                rock = i;
                break;
            }
        }

        if (RockSelection(rock))
        {
            shopButton[2].SetActive(false);
        }

    }


    bool RockSelection(int rock)
    {
        switch (rock)
        {
            case 0:
                if (player.money >= rockCost[0])
                {
                    player.money -= rockCost[0];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            case 1:
                if(player.money>= rockCost[1])
                {
                    player.money -= rockCost[1];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            case 2:
                if (player.money >= rockCost[1])
                {
                    player.money -= rockCost[2];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            case 3:
                if (player.money >= rockCost[1])
                {
                    player.money -= rockCost[3];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            case 4:
                if (player.money >= rockCost[1])
                {
                    player.money -= rockCost[4];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            case 5:
                if (player.money >= rockCost[1])
                {
                    player.money -= rockCost[5];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            case 6:
                if (player.money >= rockCost[1])
                {
                    player.money -= rockCost[6];
                    gameManager.CreateRock(rock);
                }
                return true;
            //break;
            default:
                return false;
                //break;
        }
    }

}
