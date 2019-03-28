using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public GameObject Lazer;

    public Transform shotPoint;

    public AudioClip shotSound;

    public float shotSpeed = 5;

    private AudioSource SoundManager;

    private Health myHealth;

    public int ScoreToAdd = 10;

    float Min_X, Max_X;
    float Min_Y, Max_Y;


    public float padding = 1;

    public float Movement_Speed = 5;

    private bool movingLeft, movingDown;

    float timeBetweenShots = 1;

    // Use this for initialization
    void Start()
    {

        timeBetweenShots = Random.Range(0.5f, 2);

        movingDown = true;

        movingLeft = (Random.Range(0, 1) == 1);

        SoundManager = GetComponent<AudioSource>();

        myHealth = GetComponent<Health>();

        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 ScreenMin = Camera.main.ViewportToWorldPoint(new Vector3(0, (Random.Range(0.65f, 0.85f)), distance));
        Vector3 ScreenMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        Min_X = ScreenMin.x + padding;
        Max_X = ScreenMax.x - padding;

        Min_Y = ScreenMin.y + padding;
        Max_Y = ScreenMax.y - padding;

        if (EnemySpawner.enemies.Contains(this))
        {
            return;
        }

        EnemySpawner.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth.isDead())
        {
            Die();
        }
        ManageMovement();

        HandleShooting();
    }

    void HandleShooting()
    {
        Invoke("Shoot", timeBetweenShots);
    }

    void ManageMovement()
    {
        if (!movingDown)
        {
            transform.Translate((((movingLeft) ? Vector3.left : Vector3.right) * Movement_Speed) * Time.deltaTime);
        }
        else
        {
            transform.Translate((Vector3.down * Movement_Speed) * Time.deltaTime);
        }

        if (transform.position.y <= Min_Y)
        {
            movingDown = false;
        }

        if (transform.position.x >= Max_X)
        {
            movingLeft = true;
        }
        else if (transform.position.x <= Min_X)
        {
            movingLeft = false;
        }
    }

    private void Shoot()
    {

        CancelInvoke("Shoot");

        GameObject shot = Instantiate(Lazer, shotPoint.position, Quaternion.identity) as GameObject;

        if (shot.GetComponent<Rigidbody2D>() != null)
        {
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shotSpeed);

            SoundManager.clip = shotSound;

            //if(SoundManager.isPlaying == false){
            SoundManager.Play();
            //}
        }
        else
        {

        }
    }

    public void Die()
    {

        myHealth.SpawnExplosion();
        if (EnemySpawner.enemies.Contains(this))
        {
            EnemySpawner.enemies.Remove(this);
        }

        FindObjectOfType<ScoreManager>().ModifyScore(ScoreToAdd);
        Destroy(gameObject);

    }

}
