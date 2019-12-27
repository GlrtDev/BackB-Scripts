using UnityEngine;

public class moveMixin : CubeMixin
{
    public Vector3 moveVector = Vector3.zero;
    public float speed = 20.0f;
    public override void LoadMixin()
    {
        iTween.MoveBy(cube.gameObject, iTween.Hash(
            "amount", moveVector,
            "speed", speed,
            "easetype", iTween.EaseType.easeInOutExpo,
            "looptype", iTween.LoopType.pingPong
            ));
    }
}
