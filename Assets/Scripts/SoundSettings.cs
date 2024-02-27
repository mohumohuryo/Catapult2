using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{

    [Header("Mixerの参照")]
    [SerializeField] AudioMixer audioMixer;


    [Header("スライダーの参照")]
    [SerializeField] Slider slider;

    [Header("スライダーで調整したいAudioMixerのGroup名"), SerializeField]
    string mixerGroupName;






    private void Start()
    {
        //スライダーにMixerの値をセットする
        SetUpSlider();

    }




    /// <summary>
    /// スライダーのvalueの値をMixerのvolumeに代入する（対数スケール）
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        //ルート求める
        volume = Mathf.Sqrt(volume);

        //[0,1]→[-80,0]の範囲にする
        float db = (volume * 80f) - 80f;

        //AudioMixerに音量を設定する
        audioMixer.SetFloat(mixerGroupName, db);
    }




    /// <summary>
    /// スライダーのvalueにMixerのVolumeの値を代入する（対数スケール）
    /// </summary>
    void SetUpSlider()
    {
        //AudioMixerからvolumeを受け取る
        audioMixer.GetFloat(mixerGroupName, out float volume);

        //[-80,0]→[0,1]の範囲にする
        float pos = (volume + 80f) / 80f;

        // 二乗
        slider.value = pos * pos; 
    }

}
