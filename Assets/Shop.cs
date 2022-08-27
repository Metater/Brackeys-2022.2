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
    [SerializeField] List<TMP_Text> shopDesc = new List<TMP_Text>();
    [SerializeField] List<int> rockCost = new List<int>();
    public void OpenShop()
    {
        for(int i = 0; i < 3; i++)
        {
            CalculateShop(i);
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


        switch (rock)
        {
            case 0:
                player.money -= rockCost[0];
                break;
            case 1:
                player.money -= rockCost[1];
                break;
            case 2:
                player.money -= rockCost[2];
                break;
            case 3:
                player.money -= rockCost[3];
                break;
            case 4:
                player.money -= rockCost[4];
                break;
            case 5:
                player.money -= rockCost[5];
                break;
            case 6:
                player.money -= rockCost[6];
                break;
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


        switch (rock)
        {
            case 0:
                player.money -= rockCost[0];
                break;
            case 1:
                player.money -= rockCost[1];
                break;
            case 2:
                player.money -= rockCost[2];
                break;
            case 3:
                player.money -= rockCost[3];
                break;
            case 4:
                player.money -= rockCost[4];
                break;
            case 5:
                player.money -= rockCost[5];
                break;
            case 6:
                player.money -= rockCost[6];
                break;
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


        switch (rock)
        {
            case 0:
                player.money -= rockCost[0];
                break;
            case 1:
                player.money -= rockCost[1];
                break;
            case 2:
                player.money -= rockCost[2];
                break;
            case 3:
                player.money -= rockCost[3];
                break;
            case 4:
                player.money -= rockCost[4];
                break;
            case 5:
                player.money -= rockCost[5];
                break;
            case 6:
                player.money -= rockCost[6];
                break;
        }


    }

}
