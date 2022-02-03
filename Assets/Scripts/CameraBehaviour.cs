using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public void RotateCamera()
    {
        transform.Rotate(0, 0, 90);
    }
}
