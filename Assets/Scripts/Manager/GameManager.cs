using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("目標のフレームレート"),SerializeField]
    int targetFrameRate = 60;



    [Header("配列にステージのプレハブを遊ぶ順番に入れる"), SerializeField]
    GameObject[] stage;

    [Header("現在のステージ番号"), SerializeField]
    int stageNumber = 0;

    //現在のステージプレハブの参照を入れてステージの切り替えを行う用
    GameObject currentStage;



    [Header("UIManagerの参照"), SerializeField]
    UIManager uIManager;





    //シングルトン用インスタンス
   private static GameManager instance;

    private void Awake()
    {

        // フレームレートを設定
        Application.targetFrameRate = targetFrameRate;

        //instanceがnullだったら
        if (instance == null)
        {
            //instanceに自オブジェクトの参照を入れる
            instance = this;

            //シーン遷移しても破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //すでにinstanceが参照を持っている場合は自オブジェクト消す
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        //フェードイン
        StartCoroutine(uIManager.FadeIn());

        //シーン遷移時にやりたい関数を登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    /// <summary>
    /// インスタンスがnullの場合は、新しくGameManagerをインスタンス化、DontDestroyOnLoadに入れて、その参照を返す
    /// </summary>
    /// <returns>GameManagerインスタンスの参照</returns>
    public static GameManager GetOrCreateInstance()
    {
        //インスタンスの参照がなかった場合の処理
        if (instance == null)
        {
            //GameManagerをインスタンス化する
            GameObject obj = new GameObject(typeof(GameManager).Name);

            //今作ったGameManagerオブジェクトに<GameManager>スクリプトを付ける
            instance = obj.AddComponent<GameManager>();

            //今作ったGameManagerオブジェクトをシーンを遷移しても破棄されないようにする
            DontDestroyOnLoad(obj);
        }


        return instance;
    }


    /// <summary>
    /// 次のステージを生成する。次のステージが無い場合はクリアシーンに遷移する
    /// </summary>
    /// <returns></returns>
    public IEnumerator NextStage()
    {
        //フェードアウト
        yield return uIManager.FadeOut();

        //前のステージは邪魔だから場外に出す      (消すとこの後の処理が呼ばれなくなるから)
        currentStage.transform.position = new Vector3(0f, 1000f, 0f);

        //ステージの番号を１つ増やす
        stageNumber++;


        //次のステージがある場合は、次のステージを生成する、ステージをすべてクリアしている場合は、ゲームクリアシーンに遷移する
        if (stageNumber < stage.Length)
        {
            //前のステージの参照を後で消すから一時保存しておく
            GameObject previousStage = currentStage;

            //次のステージの生成しcurrentStageに参照を入れておく
            currentStage=Instantiate(stage[stageNumber]);

            //フェードイン
            yield return uIManager.FadeIn();

            //全ての処理が終わったので前のステージは消す
            Destroy(previousStage);
        }
        else
        {
            //ゲームクリアシーンに遷移する
            SceneManager.LoadScene("ClearScene");
        }
    }



    /// <summary>
    /// 今進んでいるところのステージを作成
    /// </summary>
    private void CurrentStage()
    {
        if (stageNumber < stage.Length)
        {
            //今進んでいるところのステージを作成し、ステージのインスタンスをcurrentStageに代入する
            currentStage = Instantiate(stage[stageNumber]);
        }
        else
        {
            //進行度を１に戻す  0は、チュートリアルだから２週目以降はスキップする
            stageNumber = 1;

            //今進んでいるところのステージを作成し、ステージのインスタンスをcurrentStageに代入する
            currentStage = Instantiate(stage[stageNumber]);

        }
    }



    /// <summary>
    /// タイトルシーンに遷移する
    /// </summary>
    public IEnumerator TitleScene()
    {
        //フェードアウト
        yield return uIManager.FadeOut();

        //タイトルシーンに遷移する
        SceneManager.LoadScene("TitleScene");
    }



    /// <summary>
    /// フェードアウトした後、GamePlaySceneに遷移する
    /// </summary>
    public IEnumerator GamePlayScene()
    {
        //フェードアウト
        yield return uIManager.FadeOut();

        //ゲームが遊べるシーンに遷移する
        SceneManager.LoadScene("GamePlayScene");
    }



    /// <summary>
    /// ゲームクリアシーンに遷移する
    /// </summary>
    public IEnumerator ClearScene()
    {
        //フェードアウト
        yield return uIManager.FadeOut();

        //ゲームクリアシーンに遷移する
        SceneManager.LoadScene("ClearScene");
    }



    /// <summary>
    /// シーン遷移時に自動的に呼ばれる関数
    /// 処理の内容
    /// ・フェードイン
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ここに新しいシーンが読み込まれたときの処理を追加
        StartCoroutine(uIManager.FadeIn());

        //遷移したシーンがGamePlaySceneだったら
        if(scene.name=="GamePlayScene")
        {
            //ステージを生成する
            CurrentStage();
        }
    }
}

