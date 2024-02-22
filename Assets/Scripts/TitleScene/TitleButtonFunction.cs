using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleButtonFunction : MonoBehaviour
{
    [Header("ボタンに付いているTextの参照"), SerializeField]
    TextMeshProUGUI text;

    [Header("ボタンにカーソルが乗った時に変更する色"), SerializeField]
    Color[] colorChange;

    //元の色を記録するよう
    Color originalColor;

    [Header("鳴らしたいSE"), SerializeField]
    AudioClip[] playSE;

    [Header("AudioSourceの参照"),SerializeField]
    AudioSource audioSource;





    //GameManagerの参照を入れておくよう
    GameManager gameManager;

    //UIManagerの参照を入れておくよう
    UIManager uIManager;






    private void Start()
    {
        //元の色を取得する
        originalColor = text.color;

        //シングルトンインスタンスの参照を取得する
        gameManager = GameManager.GetOrCreateInstance();

        //シングルトンインスタンスの参照を取得する
        uIManager = UIManager.GetOrCreateInstance();
    }



    /// <summary>
    /// ゲームが遊べるシーンを遷移する
    /// </summary>
    /// <param name="sceneName"></param>
    public void OnClickGamePlaySceneTransition()
    {
        //フェードアウトした後、GamePlaySceneに遷移する
        StartCoroutine(gameManager.GamePlayScene());
    }


    /// <summary>
    /// colorChange配列に登録してある色に変更する
    /// </summary>
    /// <param name="cangeTextColorNumber">変更したい cangeTextColorNumber配列の番号</param>
    public void ChangeTextColor(int cangeTextColorNumber)
    {
        if (cangeTextColorNumber < playSE.Length || cangeTextColorNumber >= 0)
        {
            //色の変更
            text.color = colorChange[cangeTextColorNumber];
        }
    }



    /// <summary>
    /// テキストの色を元の色に戻す
    /// </summary>
    public void RestoreTextColor()
    {
        //元の色に戻す
        text.color = originalColor;
    }




    /// <summary>
    ///  playSE配列に入っているSEを再生する
    /// </summary>
    /// <param name="playSE_Number">再生したいplaySEの番号</param>
    public void PlaySE(int playSE_Number)
    {
        //登録してある配列外の番号は無効
        if (playSE_Number < playSE.Length || playSE_Number >= 0)
        {
            //SEを鳴らす
            audioSource.PlayOneShot(playSE[playSE_Number]);
        }
    }



    /// <summary>
    /// 設定画面の切り替えとテキストの色を元の色に戻す
    /// </summary>
    /// <param name="isVisible">true= 表示    false=非表示</param>
    public void ToggleSettingsPanel(bool isVisible)
    {

        //テキストを元の色に戻す
        RestoreTextColor();

        //設定画面の表示の切り替え
        StartCoroutine(uIManager.SetSettingsCanvasVisibility(isVisible));

    }

    /// <summary>
    /// 音量設定画面の切り替えとテキストの色を戻す
    /// </summary>
    /// <param name="isVisble"></param>
    public void ToggleAudioSettingsPanel(bool isVisble)
    {
        //テキストを元の色に戻す
        RestoreTextColor();

        //音量設定画面の表示の切り替え
        uIManager.AudioSettingsCanvasVisibility(isVisble);
    }

    /// <summary>
    /// ゲームの終了
    /// </summary>
    public void QuitGame()
    {
        // ゲーム終了処理
        Debug.Log("Quitting game...");

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false; // エディタ内では停止する

#else
        Application.Quit(); // 実行中のビルドではアプリケーションを終了する
#endif
    }

}
