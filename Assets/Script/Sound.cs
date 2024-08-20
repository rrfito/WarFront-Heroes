using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource lagu;
   public void mulai()
   {
    lagu.Play();
   }
   public void stopLagu()
   {
    lagu.Stop();
   }
}
