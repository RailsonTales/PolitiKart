using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartIA : MonoBehaviour {
    [Header("References")]
    [SerializeField] KartStats _stats;
    [SerializeField] Rigidbody _rb;
    [SerializeField] TrailRenderer trailIA;
    [SerializeField] Transform[] pontosParaSeguir;
    [SerializeField] UnityEngine.AI.NavMeshAgent auxPosicaoNavMesh;
    //[SerializeField] GameObject corIA;
    
    [SerializeField] bool faixa1 = false, faixa2 = false, faixa3 = false, jogador2 = false;
     
    [SerializeField] float frente, lado, velocidade, tempo = 0, burstTime;    

    [SerializeField] int seguir, voltas = 0;
   
    [SerializeField] bool burst;

    [SerializeField] int cMovimentacao;


    void Start()
    {
        _rb= GetComponent<Rigidbody>();
        lado = 50 * Time.deltaTime;
   	}
    
    void Update(){
    }

    void Teclado()
    {
    }


    void FixedUpdate()
    {
       
    }

    void Seguir()    {
    }

	void Movimentacao()
	{
	}

    void OnTriggerEnter(Collider colisao)
    {

    }


    void OnGUI()
    {

    }
}