using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] ParticleSystem scoreEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(Missed(collision));
        }
    }

    IEnumerator Missed(Collision collision)
    {
        Debug.Log("Missed Gate");
        Vector3 contactPoint = collision.GetContact(0).point;
        scoreEffect.transform.position = contactPoint;
        Destroy(collision.gameObject);
        scoreEffect.Play();

        yield return new WaitForSeconds(1.5f);
        gameLogic.Missed();
    }
}
