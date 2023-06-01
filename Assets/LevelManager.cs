using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public AudioSource music_src;
    public AudioClip menu_music;
    int levelsUnlocked;

    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        music_src.loop = false;
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked",1);
        for(int i = 0; i< buttons.Length; i++){
            buttons[i].interactable = false;
        }

        for(int i = 0;i< levelsUnlocked; i++){
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int levelIndex){
        music_src.clip = menu_music;
        music_src.Play();
        SceneManager.LoadScene(levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
