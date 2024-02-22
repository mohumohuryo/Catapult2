using UnityEngine;

public class SugarObject : MonoBehaviour
{

    //コライダーに接触したときの処理
    private void OnCollisionEnter(Collision collision)
    {
        //画面外の壁に当たると
        if (collision.gameObject.CompareTag("SugarRemoval"))
        {
            //自オブジェクト消す
            Destroy(gameObject);
        }
    }
}
