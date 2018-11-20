using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPlataformas : MonoBehaviour {
    [SerializeField]
    private GameObject[] plataformasprefab1, plataformasprefab2, plataformasprefab3;  // arreglo que contiene las secciones 
    private Transform jugadorTransform;
    private float spawn = 19.59f;
    private float distancia = 19.59f; // a que distancia debe aparecer la siguiente seccion
    private int cantidad = 3; // cuantas secciones habran al tiempo
    private float zonasegura = 20.0f; // a que distancia (relativa al jugador) debe agregarse y eliminarse una seccion
    private int randomprefab = 0;
    private List<GameObject> plataformasactivas; // lista para almacenar las secciones que esten activas
    public GameObject[] plataforma;

    // Use this for initialization
    void Start()
    {
        plataforma = plataformasprefab1;
        plataformasactivas = new List<GameObject>();
        jugadorTransform = GameObject.FindGameObjectWithTag("Player").transform; // defir la variable como los componentes "transform" del jugador

        for (int i = 0; i < cantidad; i++)
        {    // un ciclo for que generara las plataformas iniciales
             generarPlataformas();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(spawn > 250 && spawn <= 380)
        {
            plataforma = plataformasprefab3;
        }
        if ((spawn > 380))
        {
            plataforma = plataformasprefab2;
        }

        if (jugadorTransform.position.x - zonasegura > (spawn - cantidad * distancia))
        {  // con este if definimos cuando se agregará y eliminara una seccion

            generarPlataformas();  // agrega una seccion al azar
            borrarPlataforma();             // elimina la primera seccion de la lista
        }
    }

    private void generarPlataformas() // funcion para crear secciones
    {
        GameObject go;
        go = Instantiate(plataforma[randomindex()]) as GameObject; // crea la plataforma como un gameobject
        go.transform.SetParent(transform);                              // le da los valores del "transform" que tenga el padre (en este caso el padre es el objeto "manager")	
        go.transform.position = new Vector3 (spawn, -1.04f, 0);                        // en que lugar de x lo generara
        spawn += distancia;                                                 // se le suma la distancia para que la siguiente seccion se genere adelante de la ultima
        plataformasactivas.Add(go);                                     // se agrega la seccion creada a la lista
    }

    private void borrarPlataforma()   // funcion para borrar secciones
    {
        Destroy(plataformasactivas[0]);  // se destruye la seccion que se encuentre en el indice 0 de la lista 
        plataformasactivas.RemoveAt(0); // se remueve el indice 0 de la lista
    }

    private int randomindex()   // funcion para generar numeros aleatorios segun la cantidad de elementos que contenga el arreglo
    {
       
            randomprefab = Random.Range(0, plataforma.Length); // numero random entre 0 y la cantidad de elementos de el arreglo
            return randomprefab;
        

    }

}
