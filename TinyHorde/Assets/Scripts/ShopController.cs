using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    public GameObject[] buttonArray;
    public GameObject[] panelArray;

    void Start()
    {
        if (DataHolder.objectivesAmount == 3)
        {
            buttonArray[0].SetActive(false);
            Debug.Log("Turning off");
        }
    }

    public void AttemptPurchase(int purchase)
    {
        if(purchase == 1)
        {
            if (CheckMoney(50))
            {
                Debug.Log("Has enough money, subtracting");
                DataHolder.cash -= 50;
                DataHolder.zombieHP += 1;
            }
        }
        if (purchase == 2)
        {
            if (CheckMoney(100))
            {
                Debug.Log("Has enough money, subtracting");
                DataHolder.cash -= 100;
                DataHolder.zombieSpeed += 0.5f;
            }
        }
        if (purchase == 3)
        {
            if (CheckMoney(150))
            {
                Debug.Log("Has enough money, subtracting");
                DataHolder.cash -= 150;
                DataHolder.objectivesAmount += 1;

                if(DataHolder.objectivesAmount == 3)
                {
                    buttonArray[0].SetActive(false);
                    Debug.Log("Turning off");
                }
            }
        }

        if (purchase == 4)
        {

        }
        if (purchase == 5)
        {

        }
        if (purchase == 6)
        {

        }

        if (purchase == 7)
        {

        }
        if (purchase == 8)
        {

        }
        if (purchase == 9)
        {

        }


    }

    public void ActivatePanel(int number)
    {
        foreach(GameObject panel in panelArray)
        {
            panel.SetActive(false);
        }

        panelArray[number].SetActive(true);
    }


    private bool CheckMoney(int cost)
    {
        if (DataHolder.cash >= cost)
        {
            return true;
        }
        else return false;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
