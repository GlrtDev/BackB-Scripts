using UnityEngine;

public class Cube : MonoBehaviour
{
    public Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 HitScale = new Vector3(0.25f,0.25f,0.25f);
    public Vector3 zeroScale = Vector3.zero;
    [HideInInspector]
    public int ballsNeededToAction;

    public virtual void Awake()
    {
        CubeMixin[] mixins = GetComponents<CubeMixin>();
        foreach (CubeMixin mixin in mixins)
        {
            mixin.cube = this;
            mixin.LoadMixin();
        }
    }
}
