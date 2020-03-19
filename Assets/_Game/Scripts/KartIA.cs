using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartIA : MonoBehaviour {
    [SerializeField] KartStats _stats;
    [SerializeField] Rigidbody movRigidbody;
    [SerializeField] GameObject corIA;
    //[SerializeField] KartIA ia;
    
    [SerializeField] float frente, lado, velocidade, tempo = 0, burstTime;    

    [SerializeField] int seguir, voltas = 0;
    [SerializeField] bool faixa1 = false, faixa2 = false, faixa3 = false, jogador2 = false;
    
    [SerializeField] float count, contador, limiteVelocidade, limiteVelocidadeNormal, limiteRe, limiteVelocidadeBurst;
    [SerializeField] float aceleracao, desaceleracao,contadorMoedas;

    [SerializeField] bool burst;

    [SerializeField] float gravidade, marchare;
    [SerializeField] int cMovimentacao;
    [SerializeField] TrailRenderer trailIA;
    [SerializeField] Transform[] pontosParaSeguir;
    [SerializeField] UnityEngine.AI.NavMeshAgent auxPosicaoNavMesh;
    
    [SerializeField] GUIStyle guiStyle1 = new GUIStyle();

    void Start()
    {
        movRigidbody = GetComponent<Rigidbody>();
        lado = 50 * Time.deltaTime;
        _stats.topSpeed = 0.1f;
        _stats.minimumSpeed = 80;
        _stats.reverseSpeed = -10;
        _stats.acceleration = 0.21f;
        _stats.reverseAcceleration = 0.15f;
        limiteVelocidadeBurst = 100;
        contadorMoedas = 0;
    }
    
    void Update()
    {
        if ( _stats.topSpeed < 1)
            velocidade = 40;

        if (voltas >= 3){

        }

        if (voltas == 1)
            corIA.tag = "Azul";

        if (voltas == 2)
            corIA.tag = "Violeta";
        if (voltas == 3)
        {
            corIA.tag = "Rosa";
        }

		//Movimentacao();

        //Seguir();

        Teclado();

        if (burst == false)
        {
            if (velocidade > limiteVelocidadeNormal)
                velocidade = limiteVelocidadeNormal;
        }

        if (burst == true)
        {
            velocidade = limiteVelocidadeBurst;
            if (velocidade > limiteVelocidadeBurst)
                velocidade = limiteVelocidadeBurst;

            burstTime += Time.deltaTime;
        }

        if (burstTime > 3)
        {
            burst = false;
        }

        if (velocidade < limiteRe)
            velocidade = limiteRe;
    }

    void Teclado()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -lado, 0);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, lado, 0);
        }
    }


    void FixedUpdate()
    {
        movRigidbody.AddForce(0, -20, 0, ForceMode.Impulse);
        movRigidbody.velocity = transform.forward * velocidade;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocidade += aceleracao * 1.9f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocidade -= aceleracao;            
        }

        else
        {
            if (velocidade > 0)
                velocidade -= desaceleracao;
        }
    }

    void Seguir()
    {

        switch (seguir)
        {
            case 1:
                auxPosicaoNavMesh.destination = pontosParaSeguir[0].position;
                break;
            case 2:
                auxPosicaoNavMesh.destination = pontosParaSeguir[6].position;
                break;
            case 3:
                auxPosicaoNavMesh.destination = pontosParaSeguir[12].position;
                break;
            case 4:
                auxPosicaoNavMesh.destination = pontosParaSeguir[1].position;
                break;
            case 5:
                auxPosicaoNavMesh.destination = pontosParaSeguir[7].position;
                break;
            case 6:
                auxPosicaoNavMesh.destination = pontosParaSeguir[13].position;
                break;

            /////////////////////////////////////

            case 7:
                auxPosicaoNavMesh.destination = pontosParaSeguir[2].position;
                break;
            case 8:
                auxPosicaoNavMesh.destination = pontosParaSeguir[8].position;
                break;
            case 9:
                auxPosicaoNavMesh.destination = pontosParaSeguir[14].position;
                break;
            case 10:
                auxPosicaoNavMesh.destination = pontosParaSeguir[3].position;
                break;
            case 11:
                auxPosicaoNavMesh.destination = pontosParaSeguir[9].position;
                break;
            case 12:
                auxPosicaoNavMesh.destination = pontosParaSeguir[15].position;
                break;

            //////////////////////////////////q

            case 13:
                auxPosicaoNavMesh.destination = pontosParaSeguir[4].position;
                break;
            case 14:
                auxPosicaoNavMesh.destination = pontosParaSeguir[10].position;
                break;
            case 15:
                auxPosicaoNavMesh.destination = pontosParaSeguir[16].position;
                break;
            case 16:
                auxPosicaoNavMesh.destination = pontosParaSeguir[5].position;
                break;
            case 17:
                auxPosicaoNavMesh.destination = pontosParaSeguir[11].position;
                break;
            case 18:
                auxPosicaoNavMesh.destination = pontosParaSeguir[17].position;
                break;
        }
    }

	void Movimentacao()
	{
		if (transform.position.z > 15 || transform.position.z < -15)
		{
			transform.Rotate(0, lado, 0);
		}
	}

    void OnTriggerEnter(Collider colisao)
    {

        if (colisao.gameObject.tag.Contains("Faixa1"))
        {
            faixa1 = true;
            if (faixa3)
            {
                voltas += 1;
                //faixa1 = false;
                faixa2 = false;
                faixa3 = false;
            }
        }

        if (colisao.gameObject.tag.Contains("Faixa2") && faixa1 == true)
        {
            faixa2 = true;
        }

        if (colisao.gameObject.tag.Contains("Faixa3") && faixa2 == true)
        {
            faixa3 = true;
        }

        if (colisao.gameObject.tag.Contains("Ponto1") || colisao.gameObject.tag.Contains("Ponto1-1") || colisao.gameObject.tag.Contains("Ponto2-1"))
        {
            seguir = Random.Range(4,7);
        }
        if (colisao.gameObject.tag.Contains("Ponto2") || colisao.gameObject.tag.Contains("Ponto1-2") || colisao.gameObject.tag.Contains("Ponto2-2"))
        {
            seguir = Random.Range(7, 10);
        }
        if (colisao.gameObject.tag.Contains("Ponto3") || colisao.gameObject.tag.Contains("Ponto1-3") || colisao.gameObject.tag.Contains("Ponto2-3"))
        {
            seguir = Random.Range(10, 13);
        }
        if (colisao.gameObject.tag.Contains("Ponto4") || colisao.gameObject.tag.Contains("Ponto1-4") || colisao.gameObject.tag.Contains("Ponto2-4"))
        {
            seguir = Random.Range(13, 16);
        }
        if (colisao.gameObject.tag.Contains("Ponto5") || colisao.gameObject.tag.Contains("Ponto1-5") || colisao.gameObject.tag.Contains("Ponto2-5"))
        {
            seguir = Random.Range(16, 19);
        }
        if (colisao.gameObject.tag.Contains("Ponto6") || colisao.gameObject.tag.Contains("Ponto1-6") || colisao.gameObject.tag.Contains("Ponto2-6"))
        {
            seguir = Random.Range(1, 4);
        }
        
        ////////// colidindo com ele mesmo

        if (voltas == 1)
        {
            if (colisao.gameObject.tag.Contains("Ciano"))
            {
                velocidade += 1f;
                Destroy(colisao.gameObject);
            }
        }
        if (voltas == 2)
        {
            if (colisao.gameObject.tag.Contains("Azul") || colisao.gameObject.tag.Contains("Ciano"))
            {
                velocidade += 1f;
                Destroy(colisao.gameObject);
            }
        }
        if (voltas == 3)
        {
            if (colisao.gameObject.tag.Contains("Violeta") || colisao.gameObject.tag.Contains("Azul") || colisao.gameObject.tag.Contains("Ciano"))
            {
                velocidade += 1f;
                Destroy(colisao.gameObject);
            }
        }

        ////////// colisão Carro

        if (colisao.gameObject.tag.Contains("Vermelho"))
        {
            velocidade -= 1f;
            Destroy(colisao.gameObject);
        }
        if (colisao.gameObject.tag.Contains("Laranja"))
        {
            velocidade -= 1f;
            Destroy(colisao.gameObject);
        }
        if (colisao.gameObject.tag.Contains("Amarelo"))
        {
            velocidade -= 1f;
            Destroy(colisao.gameObject);
        }
    }


    void OnGUI()
    {
        guiStyle1.fontSize = 40;
        guiStyle1.normal.textColor = Color.white;
        GUI.Label(new Rect(0, Screen.height - 40, 1500, 20), "Velocidade: " + (int)velocidade, guiStyle1);
        //GUI.Label(new Rect(0, 40, 1500, 20), "Volta IA: " + voltas);
    }
}