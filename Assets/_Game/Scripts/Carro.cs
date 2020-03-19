using UnityEngine;
using System.Collections;

public class Carro : MonoBehaviour
{
    public float lado;    
    public float velocidade;
	public Rigidbody movRigidbody;
    public float tempo = 0, burstTime;
    public int voltas = 0;
    public bool faixa1 = false, faixa2 = false, faixa3 = false;
    public IA ia;
    public GameObject cor;
    public float count;
    public float contador;
    public float limiteVelocidadeNormal, limiteRe, limiteVelocidadeBurst;
    public float aceleracao, desaceleracao;
    public float contadorMoedas;
    public bool burst;
    public float testColisao;
    public TrailRenderer trail;
    public GUIStyle guiStyle1 = new GUIStyle();
    public GUIStyle guiStyle2 = new GUIStyle();
    public float gravidade;
    
    void Start()
    {
        movRigidbody = GetComponent<Rigidbody>();
        lado = 50 * Time.deltaTime;
        velocidade = 0.1f;
        limiteVelocidadeNormal = 80;
        limiteRe = -10;
        //aceleracao = 0.175f;
        aceleracao = 0.25f;
        desaceleracao = 0.15f;
        limiteVelocidadeBurst = 100;
        contadorMoedas = 0;
        count = 0.1f;
        contador = 0;        
        trail = gameObject.GetComponent<TrailRenderer>();
        gravidade = -20;
    }
    
    void Update()
    {
        if (velocidade < 1)
            velocidade = 40;

        if (voltas >= 3)
            Application.LoadLevel("menu");

        //if (Input.GetButtonDown("A"))
        //{
        //    velocidade += aceleracao;
        //}
        if (Input.GetAxis("Analogico") > 0.1)
        {
            transform.Rotate(0, lado, 0);
        }
        if (Input.GetAxis("Analogico") < -0.2)
        {
            transform.Rotate(0, -lado, 0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("menu");
        }
        tempo += Time.deltaTime;
        Teclas();

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
        

        if (voltas == 1)
        {
            cor.tag = "Laranja";
        }

        if (voltas == 2)
        {
            cor.tag = "Amarelo";
        }

        if (voltas == 3)
        {
            cor.tag = "Verde";
            
        }
    }
    void FixedUpdate()
    {
        movRigidbody.AddForce(0,gravidade,0, ForceMode.Impulse);

        movRigidbody.velocity = transform.forward * velocidade;

        if (Input.GetKey(KeyCode.S) || Input.GetButton("X"))
        {
            velocidade -= aceleracao;

        }

        if (Input.GetKey(KeyCode.W) || Input.GetButton("A"))
        {
            velocidade += aceleracao;

        }
        else
        {
            if (velocidade > 0)
                velocidade -= desaceleracao;
        }
    }

	void Teclas()
	{
        

        if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0, -lado, 0);

		}
        if (Input.GetKey(KeyCode.D))
		{
            transform.Rotate(0, lado, 0);

		}
	}
    
    void OnTriggerEnter(Collider colisao)
    {
        //if (colisao.gameObject.tag.Contains("Chao"))
        //{
        //    gravidade = 0;
        //}

        if (colisao.gameObject.tag.Contains("Moeda1"))
        {
            contadorMoedas = 1;
            Destroy(colisao.gameObject);
        }
        if (colisao.gameObject.tag.Contains("Moeda2"))
        {
            contadorMoedas = 2;
            Destroy(colisao.gameObject);
        }
        if (colisao.gameObject.tag.Contains("Moeda3"))
        {
            contadorMoedas = 3;
            Destroy(colisao.gameObject);
            burst = true;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////
        
        if (colisao.gameObject.tag.Contains("Faixa1"))
        {
            faixa1 = true;
            if (faixa3)
            {
                voltas += 1;
                faixa2 = false;
                faixa3 = false;
            }
        }

        if (colisao.gameObject.tag.Contains("Faixa2") && faixa1==true)
        {
            faixa2 = true;
        }

        if (colisao.gameObject.tag.Contains("Faixa3") && faixa2 == true)
        {
            faixa3 = true;
        }


        if (voltas == 1)
        {
            if (colisao.gameObject.tag.Contains("Vermelho"))
            {
                velocidade += 1f;
                Destroy(colisao.gameObject);
            }
        }
        if (voltas == 2)
        {
            if (colisao.gameObject.tag.Contains("Laranja") || colisao.gameObject.tag.Contains("Vermelho"))
            {
                velocidade += 1f;
                Destroy(colisao.gameObject);
            }
        }
        if (voltas == 3)
        {
            if (colisao.gameObject.tag.Contains("Amarelo") || colisao.gameObject.tag.Contains("Laranja") || colisao.gameObject.tag.Contains("Vermelho"))
            {
                velocidade += 1f;
                Destroy(colisao.gameObject);
            }
        }

        ////////// colisão IA

        if (colisao.gameObject.tag.Contains("Ciano"))
        {
            velocidade -= 1f;
            Destroy(colisao.gameObject);
        }
        if (colisao.gameObject.tag.Contains("Azul"))
        {
            velocidade -= 1f;
            Destroy(colisao.gameObject);
        }
        if (colisao.gameObject.tag.Contains("Violeta"))
        {
            velocidade -= 1f;
            Destroy(colisao.gameObject);
        }        
    }
    
    void OnGUI()
    {
        guiStyle1.fontSize = 40;
        guiStyle1.normal.textColor = Color.white;        
        guiStyle2.fontSize = 20;
        guiStyle2.normal.textColor = Color.white;
        GUI.Label(new Rect(0, 0, 1500, 20), "Tempo: " + (int)tempo, guiStyle2);
        GUI.Label(new Rect(0, Screen.height/2 - 40, 1500, 20), "Velocidade: " + (int)velocidade, guiStyle1);
    }
}