using UnityEngine;
using System.Collections;
public class moveMixin : CubeMixin
{
    public Vector3 moveVector = Vector3.zero;
    public float speed = 20.0f;
    public float delay = 0.05f;
    public override void LoadMixin()
    {
        iTween.MoveBy(cube.gameObject, iTween.Hash(
            "amount", moveVector,
            "speed", speed,
            "delay", delay,
            "easetype", iTween.EaseType.easeInOutExpo,
            "looptype", iTween.LoopType.pingPong
            ));
    }
    //IEnumerator ApplyAnimation()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    iTween.MoveBy(cube.gameObject, iTween.Hash(
    //        "amount", moveVector,
    //        "speed", speed,
    //        "easetype", iTween.EaseType.easeInOutExpo,
    //        "looptype", iTween.LoopType.pingPong
    //        ));
    //}
}
