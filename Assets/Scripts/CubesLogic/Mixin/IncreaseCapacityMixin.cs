using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCapacityMixin : CubeMixin
{
    public int setTo;
    public override void LoadMixin()
    {
        cube.ballsNeededToAction = setTo;
    }
}
