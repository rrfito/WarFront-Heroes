using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public AudioSource src;
    public AudioClip btnItem;
     public AudioClip successSound; // Suara untuk berhasil
    public AudioClip failureSound; // Suara untuk gagal
    public AudioClip savePoint;
    public AudioClip PickUpSound;


    public void btnitem(){
        src.clip = btnItem;
        src.Play();
    }
    public void PlaySuccessSound()
    {
        src.clip = successSound;
        src.Play();
    }

    public void PlayFailureSound()
    {
        src.clip = failureSound;
        src.Play();
    }
    public void PlaySavePointSound()
    {
        src.clip = savePoint;
        src.Play();
    }
    public void PlayPickUpSound()
    {
        src.clip = PickUpSound;
        src.Play();
    }

}


