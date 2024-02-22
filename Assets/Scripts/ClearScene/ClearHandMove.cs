using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ClearHandMove : MonoBehaviour
{
    [Header("動かしたい手の目標のx座標"), SerializeField]
    float target_x;

    [Header("手の移動時間"), SerializeField]
    float MoveDuration;

    [Header("手の上に落としたいテキストの参照"), SerializeField]
    Rigidbody textObject;

    [Header("手の上にテキストが乗ってから何秒待機してTitleSceneに遷移するか"), SerializeField]
    float TimeToTransition;

    //gameManagerの参照
    GameManager gameManager;
    private void Start()
    {
        //gameManagerの参照の取得
        gameManager = GameManager.GetOrCreateInstance();

        //手を画面内に動かす
        StartCoroutine(Move());
    }



    /// <summary>
    /// 手を指定の位置まで動かす
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        //今の位置を代入
        Vector3 startPos = transform.position;

        //時間を計る変数
        float timer = 0;

        //目標の時間内の間は処理を繰り返す
        while (timer <= MoveDuration)
        {

            //時間を計る
            timer += Time.deltaTime;

            //最初にいた位置から目標の位置の距離を、割合を使ってx軸を動かす
            transform.position = new Vector3(Mathf.Lerp(startPos.x, target_x, timer / MoveDuration), startPos.y, startPos.z);

            //1フレーム待機
            yield return new WaitForFixedUpdate();
        }

        // 移動が完了したら最終的な位置を確定させる
        transform.position = new Vector3(target_x, startPos.y, startPos.z);

        //Textが重力の影響を受けるようにして手の上に落とす
        textObject.isKinematic = false;

        //少し待機
        yield return new WaitForSeconds(TimeToTransition);

        //タイトルシーンに遷移する
        StartCoroutine(gameManager.TitleScene());
    }
}
