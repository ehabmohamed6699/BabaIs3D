using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainMenuScreen;
    public GameObject CreditsScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }
    public void ReturnToMainMenu()
    {
        MainMenuScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }
}
