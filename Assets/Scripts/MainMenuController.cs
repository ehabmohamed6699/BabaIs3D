using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    float Selection;

    [Space(10)]
    [Header("Start")]
    public GameObject StartSprite;
    public GameObject StartSelected;

    [Space(10)]
    [Header("Settings")]
    public GameObject SettingsSprite;
    public GameObject SettingsSelected;

    [Space(10)]
    [Header("Credits")]
    public GameObject CreditsSprite;
    public GameObject CreditsSelected;

    [Space(10)]
    [Header("Exit")]
    public GameObject ExitSprite;
    public GameObject ExitSelected;

    [Space(10)]
    [Header("Selected")]
    public GameObject Selected;

    public GameObject MainMenuScreen;
    public GameObject SettingsScreen;
    public GameObject CreditsScreen;
    public GameObject MapScreen;
    public void StartGame()
    {
        MapScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void Settings()
    {
        SettingsScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void Credits()
    {
        CreditsScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Main menu");
        MainMenuScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("levelsUnlocked", 1);
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
                Selection = 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Selected.position.y -= 45;
            Selection += 1;
            if (Selection > 4)
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
                    StartGame();
                    break;
                case 2:
                    //activate options screen
                    Settings();
                    break;
                case 3:
                    //activate credits screen
                    Credits();
                    break;
                case 4:
                    //exit the game
                    ExitGame();
                    break;
                default:
                    //activate game screen
                    StartGame();
                    break;
            }
        }
        switch (Selection)
        {
            case 1:
                Selected.transform.position = new Vector3(Selected.transform.position.x, StartSprite.transform.position.y, Selected.transform.position.z);
                StartSprite.SetActive(false);
                StartSelected.SetActive(true);
                SettingsSprite.SetActive(true);
                SettingsSelected.SetActive(false);
                CreditsSprite.SetActive(true);
                CreditsSelected.SetActive(false);
                ExitSprite.SetActive(true);
                ExitSelected.SetActive(false);
                break;
            case 2:
                Selected.transform.position = new Vector3(Selected.transform.position.x, SettingsSprite.transform.position.y, Selected.transform.position.z);
                StartSprite.SetActive(true);
                StartSelected.SetActive(false);
                SettingsSprite.SetActive(false);
                SettingsSelected.SetActive(true);
                CreditsSprite.SetActive(true);
                CreditsSelected.SetActive(false);
                ExitSprite.SetActive(true);
                ExitSelected.SetActive(false);
                break;
            case 3:
                Selected.transform.position = new Vector3(Selected.transform.position.x, CreditsSprite.transform.position.y, Selected.transform.position.z);
                StartSprite.SetActive(true);
                StartSelected.SetActive(false);
                SettingsSprite.SetActive(true);
                SettingsSelected.SetActive(false);
                CreditsSprite.SetActive(false);
                CreditsSelected.SetActive(true);
                ExitSprite.SetActive(true);
                ExitSelected.SetActive(false);
                break;
            case 4:
                Selected.transform.position = new Vector3(Selected.transform.position.x, ExitSprite.transform.position.y, Selected.transform.position.z);
                StartSprite.SetActive(true);
                StartSelected.SetActive(false);
                SettingsSprite.SetActive(true);
                SettingsSelected.SetActive(false);
                CreditsSprite.SetActive(true);
                CreditsSelected.SetActive(false);
                ExitSprite.SetActive(false);
                ExitSelected.SetActive(true);
                break;
            default:
                break;
        }
    }
}
