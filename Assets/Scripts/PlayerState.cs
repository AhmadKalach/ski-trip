using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    [Header("Collision")]
    public Transform collisionCenter;
    public float collisionRadius;
    public LayerMask collisionLayers;

    [Header("Player")]
    public bool win;
    public GameObject leftTrail;
    public GameObject rightTrail;
    public GameObject cameraFollowPoint;
    public AudioSource winSfx;

    [Header("Death")]
    public ParticleSystem deathParticlesSnowman;
    public ParticleSystem deathParticlesDuck;
    public ParticleSystem deathParticlesCat;
    public AudioSource deathSfx;

    Movement playerMovement;
    EndUIFiller endUIFiller;
    LevelManager levelManager;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
        endUIFiller = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<EndUIFiller>();
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        deathParticlesSnowman.Stop();
        deathParticlesDuck.Stop();
        deathParticlesCat.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(collisionCenter.position, collisionRadius, collisionLayers);

        if (!win && colliders.Length > 0)
        {
            Debug.Log(colliders[0].gameObject.name);
            Die();
        }
    }

    public void Die()
    {
        levelManager.RespawnNewPlayer(cameraFollowPoint);
        leftTrail.transform.parent = null;
        rightTrail.transform.parent = null;
        deathSfx.transform.parent = null;
        PlayDeathParticles();
        deathSfx.Play();
        Destroy(this.gameObject);
    }


    void PlayDeathParticles()
    {
        if (gameManager.skinIndex == 0)
        {
            deathParticlesSnowman.transform.parent = null;
            deathParticlesSnowman.Play();
        }
        else if (gameManager.skinIndex == 1)
        {
            deathParticlesDuck.transform.parent = null;
            deathParticlesDuck.Play();
        }
        else
        {
            deathParticlesCat.transform.parent = null;
            deathParticlesCat.Play();
        }
    }

    public void Win()
    {
        playerMovement.stopMovement = true;
        playerMovement.winBrakes = true;
        win = true;
        endUIFiller.FillSaves();
        endUIFiller.OpenEndUI();
        winSfx.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Win();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(collisionCenter.position, collisionRadius);
    }
}
