using TMPro;
using UnityEngine;

public class CheckObject : MonoBehaviour
{

    [Header("クリアまでに必要なCubeSugar数"), SerializeField]
    int clearCount;

    [Header("入った数をカウントしているText"), SerializeField]
    TextMeshProUGUI countText;

    [Header(""), SerializeField]
    Stage2 stage2;

    //入った数をカウント
    int cubeSugarCount = 0;



    void Start()
    {
        //テキストの更新
        countText.text = cubeSugarCount.ToString() + "/" + clearCount.ToString();
    }




    private void OnCollisionEnter(Collision collision)
    {

        //接触したのがCubeSugarの場合の処理
        if (collision.gameObject.CompareTag("CubeSugar"))
        {

            //CubeSugarの数をカウントする
            cubeSugarCount++; 

            //入った分は消す
            Destroy(collision.gameObject);


            //目標の数だけ入ったらの処理
            if (cubeSugarCount == clearCount)
            {

                //テキストを消す
                countText.text = "";

                //ステージクリアか確認してクリアの場合は次のステージ
                stage2.CheckStageClear();

            }
            else if(cubeSugarCount<clearCount)
            {
                //テキストの更新
                countText.text = cubeSugarCount.ToString() + "/" + clearCount.ToString();
            }

        }
    }
}
