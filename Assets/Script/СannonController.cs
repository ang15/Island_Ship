using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СannonController : MonoBehaviour
{
    private float sensitivetyHor=9.0f;
    private float _rotationY=0f;
    private float move = 1f;
    public Transform transformCannon;
    private bool MouseX=true;
    void Update()
    {
#if UNITY_EDITOR
        if (MouseX == true)
        {
            MouseX = false;
            float delta = Input.GetAxis("Mouse X") * sensitivetyHor;
            StartCoroutine(RotationY(delta));
        }
#endif
    }

    IEnumerator RotationY(float delta)
    {
        for (int i=0;i<3;i++) 
        {
            yield return new WaitForSeconds(0.1f);
            _rotationY = RestrictAngle(transformCannon.localEulerAngles.y + delta);
            transformCannon.localEulerAngles = new Vector3(0, _rotationY, 0);
        }
        MouseX =true;
    }


    private  float RestrictAngle(float angle)
    {
        if (angle > transformCannon.localEulerAngles.y)
            angle +=move;
        if(angle< transformCannon.localEulerAngles.y)
            angle -= move;
        return angle;
    }
}

