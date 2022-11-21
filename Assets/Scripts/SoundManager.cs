using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    static SoundManager instance;

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayers;
    public int nextPlayer;

    public AudioClip[] hitClips;
    public AudioClip startClip;
    public AudioClip overClip;
    public AudioClip failClip;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame

    public static void BgmStart()
    {
        instance.bgmPlayer.Play();
    }
    
    public static void BgmStop()
    {
        instance.bgmPlayer.Stop();
    }
    
    public static void PlaySound(string name)
    {
        switch (name)
        {
            case "Start":
                instance.sfxPlayers[instance.nextPlayer].clip = instance.startClip;
                break;
            case "Over":
                instance.sfxPlayers[instance.nextPlayer].clip = instance.overClip;
                break;
            case "Hit":
                int ran = Random.Range(0, instance.hitClips.Length);
                instance.sfxPlayers[instance.nextPlayer].clip = instance.hitClips[ran];
                break;
            case "Fail":
                instance.sfxPlayers[instance.nextPlayer].clip = instance.failClip;
                break;
        }
        instance.sfxPlayers[instance.nextPlayer].Play();
        instance.nextPlayer = (instance.nextPlayer + 1) % instance.sfxPlayers.Length;
    }
    
}
