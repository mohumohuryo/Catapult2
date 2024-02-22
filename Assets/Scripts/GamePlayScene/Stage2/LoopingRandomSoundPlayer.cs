using UnityEngine;

public class LoopingRandomSoundPlayer : MonoBehaviour
{
    [Header("AudioSourceの参照"), SerializeField]
    AudioSource audioSource;

    [Header("再生したい音"), SerializeField]
    AudioClip[] clips;

    [Header("SEの音量"), Range(0, 1), SerializeField]
    float volume;

    //インデックス
    int randomIndex = 0;




    /// <summary>
    /// 配列内のSEをランダムに再生する
    /// </summary>
    void PlayRandomSE()
    {
        //配列の要素数内のランダムな値を取得
        randomIndex = Random.Range(0, clips.Length);

        //SEを再生する
        audioSource.PlayOneShot(clips[randomIndex],volume);
    }



    void Update()
    {

        //再生中じゃなかったら、次のSEを再生する
        if (audioSource.isPlaying == false)
        {
            //ランダムに配列内のSEを再生する
            PlayRandomSE();
        }
    }
}