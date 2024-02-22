using TMPro;
using UnityEngine;

public class StageTutorial : StageManager
{
    [Header("クリアまでに必要なCubeSugarを的に当てる数"), SerializeField]
    int clearCount;

    //当たった数をカウント
    int cubeSugarCount = 0;

    [Header("入った数をカウントしているText"), SerializeField]
    TextMeshProUGUI countText;




    private void Start()
    {

        //GameManagerの参照を取得する
        gameManager = GameManager.GetOrCreateInstance();

        //初期テキストを表示
        countText.text = cubeSugarCount.ToString() + "/" + clearCount.ToString();
    }



    private void OnCollisionEnter(Collision collision)
    {

        //CubeSugarが当たった時の処理
        if (collision.gameObject.CompareTag("CubeSugar"))
        {
            //物理的挙動をしないようにして、的にくっつけたままにする
            collision.rigidbody.isKinematic = true;

            //当たった数をカウントする
            cubeSugarCount++;


            if (cubeSugarCount == clearCount)
            {
                //テキストを消す
                countText.text = "";

                //ゲームクリアの処理
                StageClear();

            }
            else if (cubeSugarCount < clearCount)
            {
                //テキスト更新
                countText.text = cubeSugarCount.ToString() + "/" + clearCount.ToString();
            }
        }


        
    }
}
