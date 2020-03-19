using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KartType{
	Player,AI
}

[RequireComponent (typeof(Rigidbody))]
public class KartController : MonoBehaviour{
    
	[SerializeField] KartStats _stats;
	//[SerializeField] GameObject cor;
    //[SerializeField] KartIA ia;

    [SerializeField] KartType kartType;

    private float driftTimeThreasHold1 = 1f;
    private float driftTimeThreasHold2 = 4f; 
	private float driftTimeThreasHold3 = 7f;

	private Vector3 currentVelocity = Vector3.zero;

	private CapsuleCollider collider;

	private Collider[] colliderBuffer = new Collider[0];

	public LayerMask collidingLayers;

	const int maxPenetrationSolves = 3;

	private bool hasControl = true;
	private bool isGrounded;
	private int isDrifting = 0;

	[SerializeField] Rigidbody _rb;
	private Vector3 rigidbodyPosition;
	//private GroundInfo currentGroundInfo;

	private float airborneOrientationSpeed = 60;
	const float velocityNormalAirboneDot = 0.5f;
	private float deadzone = 0.01f;
	RaycastHit[] raycastHitBuffer = new RaycastHit[0];

	private float driftingTime = 0;

	private bool startUp;
	private float startBoostTime = 0;

	private void Awake(){

	}

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }
    
    void Update()
    {
  
    }
    void FixedUpdate()
    {

    }

    
    void OnTriggerEnter(Collider colisao)
    {
   
    }
    
    void OnGUI(){
    }
}