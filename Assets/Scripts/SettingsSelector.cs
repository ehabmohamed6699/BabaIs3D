using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingsSelector : MonoBehaviour
{
    float Selection;
    public GameObject Selected;
    private const int ELEMENTS_COUNT = 5;
    bool isGrid;

    const int MIN_VALUE = 0;
    const int MAX_VALUE = 100;
    const int VOLUME_STEP = 5;
    /*
     
    TODO: implement slider movement to change values using keyboard and mouse
     
     */

    // enable grid on the screen
    void EnableGrid()
    {
        Debug.Log($"grid : {isGrid}");
    }

    // back (to main menu if from main menu or to game if from pause menu)
    void Return()
    {
        Debug.Log("back");

    }

    // shows controls page
    void Controls()
    {
        Debug.Log("Controls");
    }


    // Start is called before the first frame update
    void Start()
    {
        Selection = 0;
        isGrid = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selection = ((Selection - 1 + ELEMENTS_COUNT) % ELEMENTS_COUNT );

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Selection = (Selection + 1) % ELEMENTS_COUNT;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (Selection)
            {
                case 2:
                    Controls();
                    break;
                case 3:
                    EnableGrid();
                    break;
                case 4:
                    Return();
                    break;
                default:
                    Return();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){

        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){

        }
    }
}
