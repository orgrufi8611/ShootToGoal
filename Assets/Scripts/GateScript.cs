using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GateScript : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] ParticleSystem scoreEffect;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(Scored(other));
        }


    }

    IEnumerator Scored(Collider collision)
    {
        Debug.Log("GOAL!!");
        Vector3 contactPoint = collision.transform.position;
        scoreEffect.transform.position = contactPoint;
        Destroy(collision.gameObject);
        scoreEffect.Play();
        yield return new WaitForSeconds(1.5f);
        gameLogic.Scored();
    }
}
