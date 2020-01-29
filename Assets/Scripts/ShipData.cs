using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/ShipData")]
public class ShipData : ScriptableObject
{
    public float rotationVelocity;
    public float acceleration;
    public int life;
    public Vector3 turretPos = new Vector3(0, 0, 1.354f);
}
