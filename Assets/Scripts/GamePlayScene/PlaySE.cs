using UnityEngine;

public class PlaySE : MonoBehaviour
{
    [Header("AudioSourceの参照"), SerializeField]
    AudioSource audio;

    [Header("CubeSugarが当たった時に再生したい音"),SerializeField]
    AudioClip[] clip;

    [Header("SEの音量"), Range(0, 1),SerializeField]
    float volume;

    [Header("配列のSEをランダム再生"),Tooltip("ランダムにしない場合は、0番目の配列のSEが再生される"), SerializeField]
    bool randomPlaySE;


    private void OnCollisionEnter(Collision collision)
    {
        //CubeSugarが当たった時の処理
        if (collision.gameObject.CompareTag("CubeSugar"))
        {

            //ランダム再生かどうか
            if (randomPlaySE)
            {
                //ランダムな値を生成     0～配列の要素数
                int random = Random.Range(0, clip.Length);

                //配列内のSEをランダム再生
                audio.PlayOneShot(clip[random], volume);

            }
            else
            {
                //配列内のSEを再生
                audio.PlayOneShot(clip[0], volume);
            }
        }
    }
}
