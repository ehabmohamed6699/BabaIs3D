using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource music_src;
    public AudioClip menu_music;
    // Start is called before the first frame update
    void Start()
    {
        music_src.clip = menu_music;
        music_src.Play();
        music_src.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
