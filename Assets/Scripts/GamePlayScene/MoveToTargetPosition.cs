using System.Collections;
using UnityEngine;

public class MoveToTargetPosition : MonoBehaviour
{

    [Header("動かしたいオブジェクトの参照"), SerializeField]
    Transform moveObject;

    [Header("動かしたい目標の位置"), SerializeField]
    Vector3 targetPosition;

    [Header("何秒かけて動かすか"), SerializeField]
    float MoveDuration;






    /// <summary>
    /// 設定した位置まで設定した時間をかけて移動する
    /// </summary>
    /// <returns></returns>
    public IEnumerator Move()
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
            transform.position = Vector3.Lerp(startPos, targetPosition, timer / MoveDuration);

            //待機
            yield return new WaitForFixedUpdate();
        }

        // 移動が完了したら最終的な位置を確定させる
        transform.position = targetPosition;
    }

}
