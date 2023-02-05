using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
  main,rimshot,trombone

}

public class SoundManager : MonoBehaviour
{
  public AudioSource source;
  public AudioClip mainTheme;
  public AudioClip rimshot;
  public AudioClip sadTrombone;
  public AudioClip outroSound;
  public AudioClip weirdSound;

    public void PlaySound(Prompt _sound)
    {
      AudioClip clip = rimshot;
      if(source == null){return;}
      if(_sound == Prompt.intro){clip = mainTheme;}
      if(_sound == Prompt.sad){clip = sadTrombone;}
      if(_sound == Prompt.outro){clip = outroSound;}
      if(_sound == Prompt.weird){clip = weirdSound;}
      if(clip != null)
      {
        source.clip = clip;
        source.Play();
      }

    }
}
