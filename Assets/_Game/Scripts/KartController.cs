using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class KartController : MonoBehaviour{

    [Header("References")]
	[SerializeField] protected KartStats _stats;
	[SerializeField] protected Rigidbody _rb;
	//[SerializeField] protected CapsuleCollider collider;
	//private Collider[] colliderBuffer = new Collider[0];
	//public LayerMask collidingLayers;
	//RaycastHit[] raycastHitBuffer = new RaycastHit[0];
	//[SerializeField] GameObject cor;

    [Header("Dev data ")]
    [SerializeField] protected Vector3 input, inputDir;
    public bool m_driftInput;

    public float m_currentSpeed = 0;
    public Vector3 m_lateralVelocity;
    public float m_interpolationVelocity = 0, m_driftState;

	private Vector3 rigidbodyPosition;
	//private GroundInfo currentGroundInfo;

    public Vector3 m_gravityVector, m_malusBuffer;

    public float m_colliderHeightAtStart, m_currentGravityFactor;

    private float driftTimeThreasHold1 = 1f, driftTimeThreasHold2 = 4f, driftTimeThreasHold3 = 7f;

	//private Vector3 currentVelocity = Vector3.zero;

	//const int maxPenetrationSolves = 3;

	private bool hasControl = true,isDrifting = false, isGrounded, isOnRamp;

	//private float deadzone = 0.01f ,airborneOrientationSpeed = 60;
	//const float velocityNormalAirboneDot = 0.5f;
	
	private float driftingTime = 0;

	private bool startUp;
	private float startBoostTime = 0;

    //[Header("Wheels")]
    //[SerializeField] protected Transform _wheelFrontLeftT, _wheelFrontRightT;
    //[SerializeField] protected Transform _wheelBackLeftT, _wheelBackRightT;

    protected float m_distanceToFloor;

	private void Awake(){

	}

    private void Start(){
        _rb = GetComponent<Rigidbody>();
        m_distanceToFloor = GetComponent<CapsuleCollider>().radius * transform.localScale.y;
        m_colliderHeightAtStart = GetComponent<CapsuleCollider>().height;
        //_rb.freezeRotation = true;
        //_rb.useGravity = false;
        m_currentGravityFactor =_stats.gravity * 0.5f;

    }

     protected void Update(){
    	input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputDir = input.normalized;

     }

    protected void FixedUpdate(){
        m_gravityVector.Set(0, 0, 0);

        AlignToGround();

        m_gravityVector += m_malusBuffer;

        if (isGrounded)
        {
            ApplyRotation();
            CalculateDrift();
            CalculateSpeed();

            Vector3 movVector = Vector3.Lerp(transform.forward, m_lateralVelocity, m_interpolationVelocity) * m_currentSpeed;
            if (!isOnRamp)
                m_gravityVector += -Vector3.up;

            _rb.velocity = movVector + m_gravityVector;
            m_currentGravityFactor = _stats.gravity * 0.5f;
        }
        else
        {
            isDrifting = false;
            m_driftInput = false;
            Vector3 movVector = transform.forward * m_currentSpeed;
            m_currentGravityFactor = Mathf.Lerp(m_currentGravityFactor, _stats.gravity, Time.fixedDeltaTime);
            m_gravityVector += -m_currentGravityFactor * Vector3.up;
            _rb.velocity = movVector + m_gravityVector;
        }
    }

    private void AlignToGround()
    {
        Debug.DrawRay(transform.position + m_distanceToFloor * transform.up, -transform.up * 1.2f, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + m_distanceToFloor * transform.up, -transform.up, out hit, Mathf.Infinity))
        {
            isGrounded = hit.distance <= 1.2f;

            isOnRamp = hit.collider.gameObject.CompareTag("Ramp") && isGrounded;

            if (isOnRamp)
            {
                m_gravityVector -= hit.normal;
                GetComponent<CapsuleCollider>().height = 0;
            }
            else
            {
                GetComponent<CapsuleCollider>().height = m_colliderHeightAtStart;
            }

            Vector3 lookDir = Vector3.Cross(hit.normal, -transform.right);
            Quaternion lookRotation = Quaternion.LookRotation(lookDir, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * (30 / Quaternion.Angle(transform.rotation, lookRotation)) * (isOnRamp ? 5 : 1));
        }
        else
        {
            isGrounded = false;
            Vector3 lookDir = Vector3.Cross(Vector3.up, -transform.right);
            Quaternion lookRotation = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * (30 / Quaternion.Angle(transform.rotation, lookRotation)) * (isOnRamp ? 5 : 1));
        }
    }

    void ApplyRotation()
    {
        if (Mathf.Abs(inputDir.z) > 0.01f)
        {
            transform.Rotate(new Vector3(0, inputDir.z * _stats._rotationSensibility, 0));

            //_wheelFrontLeftT.eulerAngles = new Vector3(0, inputDir.z * 45, 0) + transform.eulerAngles;
            //_wheelFrontRightT.eulerAngles = new Vector3(0, inputDir.z * 45, 0) + transform.eulerAngles;
        }

        if (isDrifting)
            transform.Rotate(m_driftState * new Vector3(0, -(Mathf.Pow(m_interpolationVelocity, 3) + 1f), 0));
    }

    private void CalculateDrift()
    {
        if (m_driftInput &&
            Mathf.Abs(m_driftState) < 0.2f &&
            Mathf.Abs(m_currentSpeed) > _stats._maxSpeed * _stats._percentOfSpeedToDrift * 0.01f)
        {
            if (!isDrifting && Mathf.Abs(inputDir.z) > 0.01f)
            {
                StartCoroutine(DriftCoroutine());
            }
        }
        else
        {
            if (!isDrifting)
            {
                m_driftState *= 0.95f;
                if (Mathf.Abs(m_driftState) < 0.01f)
                    m_driftState = 0;

                m_interpolationVelocity -= 0.02f;
                m_interpolationVelocity = Mathf.Max(0, m_interpolationVelocity);
            }
        }

        m_lateralVelocity = transform.right * m_driftState * _stats._driftInfluence;
    }

    void CalculateSpeed()
    {
        if (Mathf.Abs(inputDir.z) > 0.01f)
        {
            if (inputDir.z * m_currentSpeed < 0)
                m_currentSpeed *= 0.9f;
            else
            {
                m_currentSpeed += inputDir.z * Time.fixedDeltaTime * _stats._acceleration;
                m_currentSpeed = m_currentSpeed > 0
                    ? Mathf.Min(m_currentSpeed, _stats._maxSpeed)
                    : Mathf.Max(m_currentSpeed, -_stats._maxSpeed);
            }
        }
        else
        {
            m_currentSpeed -= _stats._acceleration * Time.fixedDeltaTime * Mathf.Sign(m_currentSpeed);
        }

        if (Mathf.Abs(m_currentSpeed) < 0.1f)
            m_currentSpeed = 0;

        //_wheelBackRightT.Rotate(m_currentSpeed * new Vector3(1, 0, 0));
       	//_wheelBackLeftT.Rotate(m_currentSpeed * new Vector3(1, 0, 0));
    }

    IEnumerator DriftCoroutine()
    {
        while (m_driftInput)
        {
            if (Mathf.Abs(inputDir.z) > 0.01f)
            {
                if (!isDrifting && isGrounded)
                {
                    m_driftState = -1 * Mathf.Sign(inputDir.z);
//                    m_malusBuffer += transform.up;
                    isDrifting = true;
                }

                m_interpolationVelocity += 0.02f;
                m_interpolationVelocity = Mathf.Min(0.4f, m_interpolationVelocity);
            }

            yield return null;
        }

        isDrifting = false;

        if (m_interpolationVelocity > 0.39)
        {
            _stats._maxSpeed += _stats._afterDriftBonusSpeed;
            _stats._acceleration += _stats._afterDriftBonusSpeed;
            StartCoroutine(AfterDriftCoroutine());
        }
    }

    IEnumerator AfterDriftCoroutine()
    {
        for (int i = 0; i < _stats._afterDriftBonusDuration; ++i)
        {
            _stats._maxSpeed -= _stats._afterDriftBonusSpeed / _stats._afterDriftBonusDuration;
            _stats._acceleration -= _stats._afterDriftBonusSpeed / _stats._afterDriftBonusDuration;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void SpeedUp(float p_speedPower, float p_speedDuration)
    {
        StartCoroutine(SpeedUpCoroutine(p_speedPower, p_speedDuration));
    }

    public void HitByMalus(float p_malusPower)
    {
        StartCoroutine(HitByMalusCoroutine(p_malusPower));
    }

    IEnumerator SpeedUpCoroutine(float p_speedPower, float p_speedDuration)
    {
        _stats._maxSpeed += p_speedPower;
        _stats._acceleration += p_speedPower;

        for (int i = 0; i < p_speedDuration; ++i)
        {
            _stats._maxSpeed -= p_speedPower / p_speedDuration;
            _stats._acceleration -= p_speedPower / p_speedDuration;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator HitByMalusCoroutine(float p_malusPower)
    {
        m_currentSpeed = 0.0f;
        isDrifting = false;
        m_driftInput = false;

        m_malusBuffer += Vector3.up * p_malusPower * 2;
        yield return new WaitForSeconds(0.5f);

        m_malusBuffer.Set(0, 0, 0);
    }

    public bool NoControle { get; set; }

}