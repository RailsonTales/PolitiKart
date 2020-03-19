using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="KartStats", menuName="KartStats")]
public class KartStats : ScriptableObject
{
	//[Head("Acceleration")]
	public float acceleration;
	public float reverseAcceleration;
	//[ToolTip("The rate at which the kart decreases it forwards speed when the brake button is held")]
	public float braking;

	//[Head("Speed")]
	public float minimumSpeed;
	public float topSpeed;
	public float maximumTopSpeed;
	public float reverseSpeed;

	//[Head("Drag")]
	public float coastingDrag;
	public float idleDrag;
	public float grip;

	//[Head("Gravity")]
	public float gravity;
	public float jumpHeigth;
	public float weight;

	public float turnSpeed;

    //public void KartStats()
    //{
        
    //}

    public void AddStat(KartStats stat)
    {
        
    }
}
