using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Title
{
    /// <summary>
    /// UIの入力からオーディオのオンオフを切り替えます
    /// </summary>
    public class ApplyAudioMute : MonoBehaviour
    {
        [SerializeField] private Toggle voiceToggleOn, voiceToggleOff, seToggleOn, seToggleOff;
        [SerializeField] private AudioMixer audioMixer;

        private void Start()
        {
            //UIのボタンを更新するためにミュート状態からUIの更新
            audioMixer.GetFloat("VolumeOfVoice", out var volumeOfVoice);
            if(volumeOfVoice >= 0) UnmuteAudioVoice();
            else MuteAudioVoice();
        
            audioMixer.GetFloat("VolumeOfSE", out var volumeOfSe);
            if(volumeOfSe >= 0) UnmuteAudioSe();
            else MuteAudioSe();
        }

        //UIのToggleから呼び出す専用
        public void ToggleVoiceMute(Toggle toggle)
        {
            if (toggle.isOn) MuteAudioVoice();
        }
        public void ToggleVoiceUnmute(Toggle toggle)
        {
            if(toggle.isOn) UnmuteAudioVoice();
        }
        public void ToggleMuteSe(Toggle toggle)
        {
            if(toggle.isOn) MuteAudioSe();
        }
        public void ToggleUnmuteSe(Toggle toggle)
        {
            if(toggle.isOn) UnmuteAudioSe();
        }
    
        //音のミュート&設定UIのトグルをオンオフ
        public void MuteAudioVoice()
        {
            audioMixer.SetFloat("VolumeOfVoice", -80);
            voiceToggleOn.isOn = false;
            voiceToggleOff.isOn = true;
        }
        public void UnmuteAudioVoice()
        {
            audioMixer.SetFloat("VolumeOfVoice", 0);
            voiceToggleOn.isOn = true;
            voiceToggleOff.isOn = false;
        }

        public void MuteAudioSe()
        {
            audioMixer.SetFloat("VolumeOfSE", -80);
            seToggleOn.isOn = false;
            seToggleOff.isOn = true;
        }
        public void UnmuteAudioSe()
        {
            audioMixer.SetFloat("VolumeOfSE", 0);
            seToggleOn.isOn = true;
            seToggleOff.isOn = false;
        }

    }
}
