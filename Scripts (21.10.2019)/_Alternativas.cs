using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Alternativas : MonoBehaviour
{
    public GameMaster gm;
    public GameObject alternativa;
    public GameObject Particle_A;
    public GameObject Particle_B;
    public GameObject Particle_C;

    private void Start()
    {
        Particle_A.SetActive(false);
        Particle_B.SetActive(false);
        Particle_C.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (alternativa.tag == "A")
            {
                Particle_A.transform.position = alternativa.transform.position;
                Particle_A.SetActive(true);
                Particle_B.SetActive(false);
                Particle_C.SetActive(false);
            }
            if (alternativa.tag == "B")
            {
                Particle_B.transform.position = alternativa.transform.position;
                Particle_A.SetActive(false);
                Particle_B.SetActive(true);
                Particle_C.SetActive(false);
            }
            if (alternativa.tag == "C")
            {
                Particle_C.transform.position = alternativa.transform.position;
                Particle_A.SetActive(false);
                Particle_B.SetActive(false);
                Particle_C.SetActive(true);
            }
            if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return))
            {
                gm.receberResposta(alternativa.tag);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Particle_A.SetActive(false);
            Particle_B.SetActive(false);
            Particle_C.SetActive(false);
        }
    }
}
