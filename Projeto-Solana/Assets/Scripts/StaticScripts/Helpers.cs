using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    //ao apertar Move.up - W ou "frente" - o personagem se move em 45� (o frente do Isometrico);
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
