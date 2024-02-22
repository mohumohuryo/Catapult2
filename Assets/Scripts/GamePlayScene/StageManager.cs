using UnityEngine;

public class StageManager : MonoBehaviour
{

    //GameManagerの参照
    protected GameManager gameManager;

    [Header("クリア後に出てくる手の参照((　´∀｀)bｸﾞｯ!)"), SerializeField]
    protected MoveToTargetPosition moveToTargetPosition;

    [Header("ステージ内のSlingshotの参照"), SerializeField]
    protected Slingshot slingshot;

    [Header("ステージ内のRubberの参照"), SerializeField]
    protected Rubber rubber;



    /// <summary>
    /// 弾を撃てなくする。
    /// クリアの(　´∀｀)bｸﾞｯ!を表示する。
    /// 次のステージに切り替える。
    /// </summary>
    protected void  StageClear()
    {
        //ゲームクリアしたら弾の装填、発射をできなくする
        slingshot.enabled = false;

        //ゴムを引っ張れなくする
        rubber.enabled = false;

        //(　´∀｀)bｸﾞｯ!　　の画像を出す
        StartCoroutine(moveToTargetPosition.Move());

        //次のステージ
        StartCoroutine(gameManager.NextStage());

    }
}
