using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManagerBGM : AudioManagerGeneral {
  [SerializeField]
  List<Sound> SoundList;
  [HideInInspector]
  public Sound currentBGM;
  float volumeSettingStart;
  bool changingBGM = false;
  void Awake() {
    SetAudioSources(SoundList, gameObject);
    volumeSettingStart = SettingsManager.volumeTheme;
    foreach (Sound s in SoundList) {
      s.source.ignoreListenerPause = true;
    }
    PlayAudio("MenuTheme");
  }
  void Update() {
    if (volumeSettingStart != SettingsManager.volumeTheme) {
      currentBGM.source.volume = SettingsManager.volumeTheme;
    }
  }
  void PlayAudio(string soundname) {
    Sound sound = FindSound(soundname, SoundList);
    sound.source.Play();
    sound.source.volume = SettingsManager.volumeTheme * sound.volume;
    currentBGM = sound;
  }
  public void ChangeBGM(string newBGMname) {
    if (currentBGM.name == newBGMname) {
      return;
    }
    if (changingBGM) {
      StopCoroutine("BGMFadeOutIn");
    }
    StartCoroutine("BGMFadeOutIn", newBGMname);
  }
  IEnumerator BGMFadeOutIn(string newBGM) {
    changingBGM = true;
    float duration = 1f;
    float currTime = 0f;
    float volumeLvl = SettingsManager.volumeTheme;
    AudioSource source = currentBGM.source;
    float volumeLvlInitial = source.volume;
    while (currTime < duration) {
      currTime += Time.unscaledDeltaTime;
      currentBGM.source.volume = Mathf.Lerp(volumeLvlInitial, 0f, currTime / duration);
      yield return null;
    }
    currTime = 0f;
    currentBGM.source.Stop();
    PlayAudio(newBGM);
    while (currTime < duration) {
      currTime += Time.unscaledDeltaTime;
      currentBGM.source.volume = Mathf.Lerp(0f, volumeLvl, currTime / duration);
      yield return null;
    }
    currentBGM.source.volume = volumeLvl;
    changingBGM = false;
  }
}
