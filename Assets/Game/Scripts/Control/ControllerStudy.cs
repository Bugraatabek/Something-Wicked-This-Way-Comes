using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ControllerStudy : MonoBehaviour
{
    void Awake()
{
    // Enable EnhancedTouch.
    EnhancedTouchSupport.Enable();
}

void Update()
{
    foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        if (touch.began)
        { 
            Debug.Log($"Touch {touch.screenPosition} started this frame");
        }
        else if (touch.ended)
        {
            Debug.Log($"Touch {touch.screenPosition} ended this frame");
        }
            
        else 
        {
            if(touch.inProgress)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.screenPosition);
                Debug.Log(worldPosition);
            }
        }
}
}
