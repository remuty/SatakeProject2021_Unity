using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateTransformData", order = 2)]
public class TransformData : ScriptableObject
{
    public Vector2[] initialPosition;
    public Vector2[] endPosition;
    public Vector2 initialScale;
    public Vector2 endScale;
}
