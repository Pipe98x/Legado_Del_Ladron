
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NivelManager : MonoBehaviour {

    private float puntos;
    public bool iniciar;
    [SerializeField]
    private GameObject fondoMenu, botonIniciar, titulo, gameOver;
    [SerializeField]
    private TextMeshProUGUI puntosText;
    public bool gameOverBool;


	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

        puntos = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>().transform.position.x - 7;
	}

    public void Iniciar()
    {
        fondoMenu.GetComponent<Animator>().SetInteger("Estado", 1);
        botonIniciar.GetComponent<Animator>().SetInteger("Estado", 1);
        titulo.GetComponent<Animator>().SetInteger("Estado", 1);
        iniciar = true;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        puntosText.text = puntos.ToString("F2") + "mts";
        Time.timeScale = 0f;
        gameOverBool = true;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
