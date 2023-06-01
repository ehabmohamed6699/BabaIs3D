using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    float Selection;

    [Space(10)]
    [Header("Controls")]
    public GameObject ControlsSprite;
    public GameObject ControlsSelected;

    [Space(10)]
    [Header("Enable Grid")]
    public GameObject EnableGridSprite;
    public GameObject EnableGridSelected;

    [Space(10)]
    [Header("Return")]
    public GameObject ReturnSprite;
    public GameObject ReturnSelected;

    [Space(10)]
    [Header("Selected")]
    public GameObject Selected;

    public GameObject MainMenuScreen;
    public GameObject SettingsScreen;


    public void Controls()
    {
        Debug.Log("Controls");
    }

    public void EnableGrid()
    {
        Debug.Log("Enable Grid");
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Main menu");
        MainMenuScreen.SetActive(true);
        SettingsScreen.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Selection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selection -= 1;
            //Selected.position.y += 45;
            if (Selection < 1)
            {
                Selection = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Selected.position.y -= 45;
            Selection += 1;
            if (Selection > 3)
            {
                Selection = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (Selection)
            {
                case 1:
                    //activate game screen
                    Controls();
                    break;
                case 2:
                    //activate options screen
                    EnableGrid();
                    break;
                case 3:
                    //activate credits screen
                    ReturnToMainMenu();
                    break;
                default:
                    //activate game screen
                    ReturnToMainMenu();
                    break;
            }
        }
        switch (Selection)
        {
            case 1:
                Selected.transform.position = new Vector3(Selected.transform.position.x, ControlsSprite.transform.position.y, Selected.transform.position.z);
                ControlsSprite.SetActive(false);
                ControlsSelected.SetActive(true);
                EnableGridSprite.SetActive(true);
                EnableGridSelected.SetActive(false);
                ReturnSprite.SetActive(true);
                ReturnSelected.SetActive(false);
                break;
            case 2:
                Selected.transform.position = new Vector3(Selected.transform.position.x, EnableGridSprite.transform.position.y, Selected.transform.position.z);
                ControlsSprite.SetActive(true);
                ControlsSelected.SetActive(false);
                EnableGridSprite.SetActive(false);
                EnableGridSelected.SetActive(true);
                ReturnSprite.SetActive(true);
                ReturnSelected.SetActive(false);
                break;
            case 3:
                Selected.transform.position = new Vector3(Selected.transform.position.x, ReturnSprite.transform.position.y, Selected.transform.position.z);
                ControlsSprite.SetActive(true);
                ControlsSelected.SetActive(false);
                EnableGridSprite.SetActive(true);
                EnableGridSelected.SetActive(false);
                ReturnSprite.SetActive(false);
                ReturnSelected.SetActive(true);
                break;
            default:
                break;
        }
    }
}
