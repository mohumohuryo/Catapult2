using UnityEngine;
using TMPro;

public class Stage1 : StageManager
{

    [Header("クリアまでに必要なCubeSugar数"),SerializeField]
    int clearCount;

    //入った数をカウント
    int cubeSugarCount=0;

    [Header("入った数をカウントしているText"), SerializeField]
    TextMeshProUGUI countText;


    void Start()
    {
        //GameManagerの参照を取得する
        gameManager = GameManager.GetOrCreateInstance();

        //テキストの更新
        countText.text= cubeSugarCount.ToString() + "/" + clearCount.ToString();
    }
    


    /// <summary>
    /// CubeSugarがコップに入るとカウントして目標の数入るとクリア
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    { 

        //接触したのがCubeSugarの場合の処理
        if (collision.gameObject.CompareTag("CubeSugar"))
        {

            //入った分は消す
            Destroy(collision.gameObject);

            //CubeSugarの数をカウントする
            cubeSugarCount++;



            //目標の数だけ入ったらの処理
            if (cubeSugarCount == clearCount)
            {

                //テキストを消す
                countText.text = "";

                //ゲームクリアの処理
                StageClear();
            }
            else if (cubeSugarCount < clearCount)
            {
                //テキストの更新
                countText.text = cubeSugarCount.ToString() + "/" + clearCount.ToString();
            }
        }
    }
}
