using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewAudioBox", menuName = "ScriptableObject/AudioBox", order = 1)]
public class AudioBox : ScriptableObject
{
    [Header("MainBGM")]
    public AudioClip MainBgm;
    [Header("InPlayBGM")]
    public AudioClip[] AllBGM;
}