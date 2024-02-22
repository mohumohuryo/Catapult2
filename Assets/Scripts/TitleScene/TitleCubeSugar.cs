using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCubeSugar : MonoBehaviour
{
    [Header("CubeSugarのPrefab"),SerializeField]
    GameObject cubeSugar;

    [Header("生成間隔 (秒)"), SerializeField]
    float GenerationInterval;

    [Header("生成する位置"), SerializeField]
    Vector3 generatingPositionn;

    [Header("生成する位置のランダムX軸の範囲 (0～〇)"), SerializeField]
    float xAxisRange;

    //生成時間を計る
    float timer;

    void Update()
    {
        //時間を減らしていく
        timer -= Time.deltaTime;

        //時間が0になったら
        if(timer<=0)
        {
            //x軸のランダムな値を作る
            float random_x = Random.Range(0, xAxisRange);

            //生成する位置にｘ軸のランダムの値を足す
            Vector3 position = new Vector3(generatingPositionn.x + random_x, generatingPositionn.y, generatingPositionn.z);

            //ランダムなｘの位置にPrefabを生成する
            GameObject sugar=Instantiate(cubeSugar, position,Quaternion.identity);



            //時間をリセット
            timer = GenerationInterval;
        }
    }
}
