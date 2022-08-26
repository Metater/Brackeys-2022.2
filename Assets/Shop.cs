using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] List<Sprite> rockType = new List<Sprite>();
    float randomInt;
    [SerializeField] List<Image> shopImage = new List<Image>();

    public void OpenShop()
    {
        CalculateShop();
    }


    public void CloseShop()
    {

    }

    void CalculateShop()
    {
        randomInt = Random.Range(0, 60);

        if(randomInt <=19 && randomInt >= 0)
        {
            //basic
        }else if(randomInt <= 34 && randomInt >= 20)
        {
            //Reinforced
        }
        else if (randomInt <= 49 && randomInt >= 35)
        {
            //Slime
        }
        else if (randomInt <= 59 && randomInt >= 50)
        {
            //Fire
        }
        else if (randomInt <= 69 && randomInt >= 60)
        {
            //Ice
        }
        else if (randomInt <= 79 && randomInt >= 70)
        {
            //Lightning
        }
    }



}
