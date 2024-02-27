using UnityEngine;

public class SugarObject : MonoBehaviour
{
    [Header("X軸の消える範囲指定"), SerializeField]
    float xDisappearPoint;

    [Header("Y軸の消える範囲指定"), SerializeField]
    float yDisappearPoint;



    private void Update()
    {
        //画面外に出るとオブジェクトを消す
        if(xDisappearPoint<=transform.position.x||-xDisappearPoint>=transform.position.x||yDisappearPoint<=transform.position.y||-yDisappearPoint>=transform.position.y)
        {
            Destroy(gameObject);
        }
    }


}
