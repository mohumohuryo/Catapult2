using UnityEngine;

public class Stage2 : StageManager
{

    [Header("お腹いっぱいにする鳥の数"),SerializeField]
    int fullUpBird;

    //お腹いっぱいになった数をカウントする
    int fullUpCount;



    private void Start()
    {
        //GameManagerの参照を取得する
        gameManager = GameManager.GetOrCreateInstance();
    }



    /// <summary>
    /// ステージクリアか確認して、クリアの場合はゲームクリアの処理を行う
    /// </summary>
    public void CheckStageClear()
    {
        //お腹いっぱいにのカウントを増やす
        fullUpCount++;

        //bird1とbird2がお腹いっぱいだったら
        if (fullUpCount>=fullUpBird)
        {
            //ゲームクリアの処理
            StageClear();
        }
    }
}
