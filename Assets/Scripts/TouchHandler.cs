using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    void OnEnable()
    {
        var recognizer = new TKSwipeRecognizer();
        recognizer.gestureRecognizedEvent += PlayerBehavoir.MoveBalls;
        TouchKit.addGestureRecognizer(recognizer); //TO DO ADD SIMPLE TOUCH RECIGNIZER
    }

    void OnDisable()
    {
        TouchKit.removeAllGestureRecognizers();
    }

}
