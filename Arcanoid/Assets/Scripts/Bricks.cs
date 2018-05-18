using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public GameObject brickParticle;

    private GameObject cloneBrickParticle;

    void OnCollisionEnter(Collision other)
    {
        cloneBrickParticle = Instantiate(brickParticle, transform.position, Quaternion.identity);
        GameController.instance.DestroyBrick();
        Destroy(gameObject);
        Destroy(cloneBrickParticle, 1.5f);
    }
}
