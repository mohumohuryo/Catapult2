using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [Header("飛ばす力の最大"), SerializeField]
    float maximumPower;

    [Header("飛ばすのに必要な最低限の力"), SerializeField]
    float minimumPower;

    [Header("スワイプ感度調整 (値が高いほどスワイプで細かい力調整が出来る)"), SerializeField]
    float swipeSensitivity;

    [Header("飛ばしたいオブジェクトを引っ張った時の下がり具合"), SerializeField]
    float maximumPullForce = 13;

    [Header("発射後のsugarの再装填までの時間"), SerializeField]
    float reloadingTime;

    [Header("Multiple Rubberの参照"), SerializeField]
    Rubber rubber;

    [Header("飛したいオブジェクトの参照"), SerializeField]
    GameObject cubeSugarPrefab;

    //今装填中のcubeSugarの参照を入れる
    GameObject flyingSugarCube;


    //飛ばすcubeSugarの発射位置
    Vector3 firingPosition;


    //マウスのposition
    Vector3 startPos;
    Vector3 endPos;


    ////引っ張った長さ
    //float swipeLength;

    //発射出来るかのフラグ
    bool fly = false;

    //単位ベクトル
    Vector3 unitVector;


    //再装填時間を計る
    float timer = 0;





    private void Start()
    {
        //cubeSugarの生成場所を決める               slingshotの位置から ( - 2 , 1.2f , -7 )を(　x軸　,　y軸　,　z軸　)足すことで中心にする事が出来る
        firingPosition = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y + 1.2f,transform.position.z -7);
    }



    void Update()
    {

        //飛ばす準備が出来てない場合は次の弾を準備
        if (fly == false)
        {
            //時間を計る
            timer -= Time.deltaTime;

            //時間が0でcubeSugarの再装填
            if (timer <= 0)
            {
                //発射準備OK
                fly = true;

                GenerateCubeSugar();

                //時間をリセット
                timer = reloadingTime;
            }

        }


        //マウスを左クリックした時の処理
        if (Input.GetMouseButtonDown(0))
        {
            //マウスのスクリーン座標を取得
            startPos = Input.mousePosition;
        }



        //左マウスを長押している間の処理   クリックして無い場合は以下の処理をしない
        if (Input.GetMouseButton(0))
        {

            //マウスのスクリーン座標を取得
            endPos = Input.mousePosition;

            //startPosからendPosの単位ベクトルを取得
            unitVector = Vector3.Normalize(endPos - startPos);



            //発射準備が出来ている場合は、cubeSugarの角度変更と後ろに引く処理を行う
            if (fly)
            {
                //flyBallの角度の変更と後ろに引く関数
                ChangeSugarAngleAndPull();
            }
        }


        //左マウスを離した時の処理
        if (Input.GetMouseButtonUp(0) && fly)
        {

            //引っ張った長さに応じてcubeSugarを発射する     発射できれば true     発射出来てない false
            if (FiringObjects())
            {
                //２回連続で飛ばせないようにする為にfalseにしておく
                fly = false;
            }
        }
    }



    /// <summary>
    /// CubeSugarの角度の変更と後ろに引く関数
    /// </summary>
    void ChangeSugarAngleAndPull()
    {

        //スワイプしたベクトルを使い発射物の角度を変える
        flyingSugarCube.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(unitVector.y, unitVector.x) * 180 / Mathf.PI);


        //引っ張る位置の調整   方向ベクトル * 伸ばせる最大距離 * ゴムの伸びの割合 + 元々の位置    で飛ばしたいオブジェクトを後ろに引く 
        float positionAdjustment_x = unitVector.x * rubber.swipeRatio * maximumPullForce + firingPosition.x;
        float positionAdjustment_y = unitVector.y * rubber.swipeRatio * maximumPullForce + firingPosition.y;

        //調整した位置を代入
        flyingSugarCube.transform.position = new Vector3(positionAdjustment_x, positionAdjustment_y, firingPosition.z);
    }



    /// <summary>
    /// 引っ張った長さに応じてCubeSugarを発射する     戻り値：　発射できれば true     発射出来てない false
    /// </summary>
    bool FiringObjects()
    {

        //引っ張った長さを計算            swipeSensityvityは、スワイプ感度調整
        float swipeLength = Vector3.Distance(startPos, endPos) / swipeSensitivity;


        //引っ張った長さが飛ばすのに必要な力以上なら飛ばす処理に行く
        if (swipeLength >= minimumPower)
        {

            //Rigidbodyの参照を取得する
            Rigidbody rb = flyingSugarCube.GetComponent<Rigidbody>();

            //物理的挙動をするようにする
            rb.isKinematic = false;



            //方向ベクトル * 飛ばす力の最大値 * ゴムの伸びの割合   で飛ばしたいオブジェクトを発射する
            //方向ベクトルはスワイプした方向とは逆方向に飛ばしたいからマイナスを付けて方向を逆にする
            rb.AddForce(-unitVector * maximumPower * rubber.swipeRatio, ForceMode.Impulse);


            return true;

        }
        else
        {
            //引っ張った距離が短いと引っ張ったオブジェクトを元の位置に戻す
            flyingSugarCube.transform.position = firingPosition;

            //角度も０に戻す
            flyingSugarCube.transform.localEulerAngles = Vector3.zero;


            return false;
        }
    }


    /// <summary>
    /// cubeSugarの生成とcubeSugarの物理挙動をしないようにする
    /// </summary>
    void GenerateCubeSugar()
    {
        //CubeSugarを生成
        flyingSugarCube = Instantiate(cubeSugarPrefab, firingPosition, Quaternion.identity);

        //発射前にオブジェクトが落ちないようにする為に、物理的挙動をしないようにしておく
        flyingSugarCube.GetComponent<Rigidbody>().isKinematic = true;

        //flyingSugarCudeを子オブジェクトにしておくことで、ステージを切り替えの時まとめて消せるため
        flyingSugarCube.transform.SetParent(transform);
    }


}
