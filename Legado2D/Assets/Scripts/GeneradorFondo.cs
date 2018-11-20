using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorFondo : MonoBehaviour {

    [SerializeField]
    private GameObject[] casasPrefab, montañas, casasFondo;  // arreglo que contiene las secciones 
    private Transform jugadorTransform;
    private float spawn = 30.59f;
    private float distancia = 19.59f; // a que distancia debe aparecer la siguiente seccion
    private int cantidad = 3; // cuantas secciones habran al tiempo
    private float zonasegura = 20.0f; // a que distancia (relativa al jugador) debe agregarse y eliminarse una seccion
    private int randomprefab = 0;
    private List<GameObject> plataformasactivas, casasFondoActivas; // lista para almacenar las secciones que esten activas
    private float distanciaMontañas = 100, posicionMontañas = 260, distanciaCasasFondo = 30, posicionCasasFondo = 73;

    // Use this for initialization
    void Start()
    {
        plataformasactivas = new List<GameObject>();
        casasFondoActivas = new List<GameObject>();
        jugadorTransform = GameObject.FindGameObjectWithTag("Player").transform; // defir la variable como los componentes "transform" del jugador

        for (int i = 0; i < cantidad; i++)
        {    // un ciclo for que generara las plataformas iniciales
            generarCasas();
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (jugadorTransform.position.x - zonasegura > (spawn - cantidad * distancia))
        {  // con este if definimos cuando se agregará y eliminara una seccion

            generarCasas();  // agrega una seccion al azar
            borrarPlataforma();             // elimina la primera seccion de la lista
        }

        if(jugadorTransform.transform.position.x - 50 > distanciaMontañas)
        {
            Instantiate(montañas[0], new Vector3( posicionMontañas, 7.454235f, -2.293465f), transform.rotation);
            distanciaMontañas += 102;
            posicionMontañas += 204;
        }

        if (jugadorTransform.transform.position.x  > distanciaCasasFondo)
        {
            GameObject go = Instantiate(casasFondo[0], new Vector3(posicionCasasFondo, 2.854101f, -1.487115f), transform.rotation);
            distanciaCasasFondo += 40;
            posicionCasasFondo += 43;
            casasFondoActivas.Add(go);
        }

        if(casasFondoActivas.Count > 2)
        {
            Destroy(casasFondoActivas[0]);
            casasFondoActivas.RemoveAt(0);
        }
    }

    private void generarCasas() // funcion para crear secciones
    {
        GameObject go;
        go = Instantiate(casasPrefab[randomindex()]) as GameObject; // crea la plataforma como un gameobject
        go.transform.SetParent(transform);                              // le da los valores del "transform" que tenga el padre (en este caso el padre es el objeto "manager")	
        go.transform.position = new Vector3(spawn, -0.7f, 1.29f);                        // en que lugar de x lo generara
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
        if (plataformasactivas.Count <= 1)
        {  // esto para que las dos primeras secciones en crearse esten vacias
            return 0;
        }
        else
        {
            randomprefab = Random.Range(0, casasPrefab.Length); // numero random entre 0 y la cantidad de elementos de el arreglo
            return randomprefab;
        }

    }

}
