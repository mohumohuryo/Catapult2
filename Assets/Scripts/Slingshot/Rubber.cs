using UnityEngine;

public class Rubber : MonoBehaviour
{

    [Header("ゴムが伸びるの最大値"), SerializeField]
    float maximumRubberStretch;

    [Header("ゴムが伸びるの最低値"), SerializeField]
    float minimumRubberStretch;

    [Header("スワイプ感度調整 (値が高いほどスワイプで細かい力調整が出来る)"), SerializeField]
    float swipeSensitivity;


    //マウスのposition 
    Vector3 startPos;
    Vector3 endPos;

    //引っ張った長さ
    float swipeLength;


    //割合（引っ張った長さ　/　引っ張れる範囲)     SlingShotClassで飛ばす威力調整で使う
    [HideInInspector]
    public float swipeRatio;








    void Update()
    {


        //マウスを左クリックした時の処理
        if (Input.GetMouseButtonDown(0))
        {
            //クリックしたスクリーン座標を取得
            startPos = Input.mousePosition;
        }



        //左マウスを押している間の処理
        if (Input.GetMouseButton(0))
        {

            //長押し中のマウスのスクリーン座標を取得
            endPos = Input.mousePosition;

            //引っ張り具合でゴムの長さを調整する
            AdjustRubberLength();

            //割合（引っ張った長さ　/　引っ張れる範囲)     SlingShotClassで飛ばす威力調整で使う
            swipeRatio = swipeLength / (maximumRubberStretch - minimumRubberStretch);

            //引っ張った角度でゴムの角度を変更する
            AdjustRubberAngle();

        }



        //左マウスを離した時の処理
        if(Input.GetMouseButtonUp(0))
        {
            //ゴムの伸びを戻す
            Vector3 scale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            transform.localScale = scale;
        }
    }



    /// <summary>
    /// 引っ張り具合でゴムの長さを調整する
    /// </summary>
    void AdjustRubberLength()
    {

        //最初に押した位置から今のマウスの位置の長さを計算          swipeSensitivityはスワイプ感度
        swipeLength = Vector3.Distance(startPos, endPos) / swipeSensitivity;

        //長さの調整
        swipeLength = Mathf.Clamp(swipeLength, minimumRubberStretch, maximumRubberStretch);

        //調整した長さでゴムの伸び具合を決める
        transform.localScale = new Vector3(swipeLength, transform.localScale.y, transform.localScale.z);
    }




    /// <summary>
    /// 引っ張った角度でゴムの角度を変更する
    /// </summary>
    void AdjustRubberAngle()
    {
        //向きを取得
        Vector3 v = Vector3.Normalize(endPos - startPos);

        //ゴムの角度を決める     180はゴムを引っ張った方向に伸びてほしいから角度を反転させている
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 180);

    }

}
