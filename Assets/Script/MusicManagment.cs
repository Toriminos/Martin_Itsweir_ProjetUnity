using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicManagment : MonoBehaviour
{
    public int firstMusic = 0;
    public bool showToFillOnStart = true;
    public DragScrollSpinnerChoice toFill;
    public List<Music> musics;
    private AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
        if(musics.Count != 0){
            List<string> musicNames = new List<string>();
            foreach (Music music in musics)
            {
                musicNames.Add(music.musicName);
            }
            toFill.gameObject.SetActive(true);
            toFill.values = musicNames;
            toFill.Set(firstMusic);
            if(!showToFillOnStart)
                toFill.gameObject.SetActive(false);
        }
    }

    public void ChangeMusic(int index){
        source.clip = musics[index].musique;
        source.Play();
    }    
}

[Serializable]
public class Music{
    public string musicName;
    public AudioClip musique;
}
