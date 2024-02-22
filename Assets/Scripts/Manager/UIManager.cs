using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIManager : MonoBehaviour
{
    [Header("設定画面のCanvasの参照"), SerializeField]
    GameObject SettingsCanvas;

    [Header("音量設定画面のCanvasの参照"),SerializeField]
    GameObject AudioSettingsCanvas;

    //menuのアニメーターの参照
    Animator menuAnimator;

    [Header("Menuの参照"), SerializeField]
    GameObject menuObject;

    [Header("ButtonBlockerPanelの参照(ボタンを汎用させないようにするPanel)"), SerializeField]
    GameObject buttonBlockerPanel;



    [Header("フェードアウトにかかる時間"), SerializeField]
    float fadeOutDuration;

    [Header("フェードインにかかる時間"), SerializeField]
    float fadeInDuration;

    [Header("フェード用のImageの参照"), SerializeField]
    Image fadePanel;


    //シングルトン用インスタンス
    private static UIManager instance;
    private void Awake()
    {

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
        //アニメーターの参照を取得する
        menuAnimator = menuObject.GetComponent<Animator>();
    }


    /// <summary>
    /// インスタンスがnullの場合は、新しくUIManagerをインスタンス化、DontDestroyOnLoadに入れて、その参照を返す
    /// </summary>
    /// <returns>UIManagerインスタンスの参照</returns>
    public static UIManager GetOrCreateInstance()
    {
        //インスタンスの参照がなかった場合の処理
        if (instance == null)
        {
            //UIManagerをインスタンス化する
            GameObject obj = new GameObject(typeof(UIManager).Name);

            //今作ったUIManagerオブジェクトに<UIManager>スクリプトを付ける
            instance = obj.AddComponent<UIManager>();

            //今作ったUIManagerオブジェクトをシーンを遷移しても破棄されないようにする
            DontDestroyOnLoad(obj);
        }


        return instance;
    }


    /// <summary>
    /// フェードインする フェードインの間だけは、raycastTargetをtrueにして他のボタンなど押せないようにする
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeIn()
    {
        //フェード用UIの下のボタンなどを押せないようにする
        fadePanel.raycastTarget = true;

        //fadePanelに代入用変数
        Color color = fadePanel.color;

        //時間を計る用
        float timer = 0f;

        // 最初に透明度を最大に設定
        color.a = 1f;
        fadePanel.color = color;


        //設定した時間の間アルファ値を下げていく
        while (timer < fadeInDuration)
        {
            //時間を計る
            timer += Time.deltaTime;

            //(経過した時間 / 設定した時間)で変化させる時間の割合を出して、アルファ値を求める
            color.a = Mathf.Lerp(1f, 0f, timer / fadeInDuration);

            //求めたアルファ値をフェード用Imageのcolorに代入して色を変化
            fadePanel.color = color;

            //待機
            yield return new WaitForFixedUpdate();
        }

        //フェード用UIの下のボタンなどを押せるようにする
        fadePanel.raycastTarget = false;
    }

    /// <summary>
    /// フェードアウトする　フェードアウト中は、raycastTargetをtrueにして他のボタンなど押せないようにする
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut()
    {
        //フェード用UIの下のボタンなどを押せないようにする
        fadePanel.raycastTarget = true;

        //fadePanelに代入用変数
        Color alfa = fadePanel.color;

        //時間を計る用
        float timer = 0f;

        //設定した時間の間アルファ値を上げていく
        while (timer < fadeOutDuration)
        {
            //時間を計る
            timer += Time.deltaTime;

            //(経過した時間 / 設定した時間)で変化させる時間の割合を出して、アルファ値を求める
            alfa.a = Mathf.Lerp(0f, 1f, timer / fadeOutDuration);

            //求めたアルファ値をフェード用Imageのcolorに代入して色を変化
            fadePanel.color = alfa;

            //待機
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 設定画面を表示の切り替え
    /// </summary>
    /// <param name="isVisible">true=表示     false=非表示</param>
    /// <returns></returns>
    public IEnumerator SetSettingsCanvasVisibility(bool isVisible)
    {

        //設定画面の表示の切り替え
        SettingsCanvas.SetActive(isVisible);

        if (isVisible)
        {

            //メニューを上画面から下に降りてくるアニメーションを再生
            menuAnimator.SetBool("ShowMenu", true);

            // アニメーションが終了するまで待機
            while (menuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            //アニメーション終了後にパネルを消してボタンを押せるようにする
            buttonBlockerPanel.gameObject.SetActive(false);

        }
        else
        {
            //menuを画面上の元の位置に戻す
            menuAnimator.SetBool("ShowMenu", false);

            //アニメーション中は、ボタンを押せないようにするパネルをつける
            buttonBlockerPanel.SetActive(true);
        }
    }



    /// <summary>
    /// 音量設定画面を表示の切り替え
    /// </summary>
    /// <param name="isVisble">true=表示     false=非表示</param>
    public void AudioSettingsCanvasVisibility(bool isVisble)
    {

        //Audioの音量調整画面の表示の切り替え
        AudioSettingsCanvas.SetActive(isVisble);

        //Menuのアニメーションの切り替え 「！」でisVisbleを反転させてる
        menuAnimator.SetBool("ShowMenu", !isVisble);
    }
}
