using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="KartStats", menuName="KartStats")]
public class KartStats : ScriptableObject
{    

	[Header("Speed")]
	public float _minimumSpeed;
	//[SerializeField] protected float _topSpeed;
	public float _maxSpeed= 30f;
	public float _reverseSpeed;

	[Header("Acceleration")]
	public float _acceleration;
	public float _reverseAcceleration;
	//[ToolTip("The rate at which the kart decreases it forwards speed when the brake button is held")]
	public float braking;

	public float turnSpeed;
    public float _rotationSensibility = 1f;
    [SerializeField, Tooltip("How much the drift will make the car turn")] public float _driftInfluence = 1f;
    public float _afterDriftBonusSpeed = 20f;
    [Tooltip("In milliseconds")] public float _afterDriftBonusDuration = 10f;
    [Range(0f, 100f), Tooltip("Percent of the max speed to start a drift")] public float _percentOfSpeedToDrift = 80f;

	[Header("Drag")]
	public float coastingDrag;
	public float idleDrag;
	public float grip;

	[Header("Gravity")]
	public float gravity = 10f;
	public float jumpHeigth;
	public float weight;

    //public void KartStats()
    //{
        
    //}

    public void AddStat(KartStats stat)
    {
        
    }
}
