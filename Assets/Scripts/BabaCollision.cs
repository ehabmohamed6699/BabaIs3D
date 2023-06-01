using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaCollision : MonoBehaviour
{
    int count = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Menu")
        {
            Menu();
        }
        if (collision.gameObject.name == "level1" && count == 1)
        {
            NextLevel(count);
            count++;
        }
        if (collision.gameObject.name == "level2" && count == 2)
        {
            NextLevel(count);
            count++;
        }
        if (collision.gameObject.name == "level3" && count == 3)
        {
            NextLevel(count);
            count++;
        }
        if (collision.gameObject.name == "level4" && count == 4)
        {
            NextLevel(count);
            count++;
        }
        if (collision.gameObject.name == "leve5" && count == 5)
        {
            NextLevel(count);
            count++;
        }



    }
    private void Menu()
    {

    }
    private void NextLevel(int numLevel)
    {

    }
}
