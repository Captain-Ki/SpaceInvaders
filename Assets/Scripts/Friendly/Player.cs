using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float padding = 1;
    float Max_X, Min_X;

    public float Move_speed = 1;

    public GameObject Lazer;

    public AudioClip shotSound;

    private AudioSource SoundManager;

    public Transform shotPoint;

    public float shotSpeed = 1;

    private Health myHealth;

    // Use this for initialization
    void Start()
    {
        myHealth = GetComponent<Health>();

        SoundManager = GetComponent<AudioSource>();

        float distance = transform.position.z - Camera.main.transform.position.z;

        Vector3 ScreenMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 ScreenMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        Min_X = ScreenMin.x + padding;
        Max_X = ScreenMax.x - padding;
    }

    // Update is called once per frame
    void Update()
    {
        ManageHealth();
        ManageMovement();
        if (Input.GetButtonUp("Fire1"))
        {
            Shoot();
        }
    }


    private void ManageHealth()
    {

        if (myHealth.isDead())
        {
            if (SoundManager.isPlaying == false)
            {
                SoundManager.Play();
            }

            Die();
        }

    }

    private void Die()
    {

        GameManager.PlayerIsDead();

        myHealth.SpawnExplosion();

        Destroy(gameObject);
    }


    private void ManageMovement()
    {

        float Raw_X = (transform.position.x + (Move_speed * Time.deltaTime * Input.GetAxis("Horizontal")));

        float Move_X = Mathf.Clamp(Raw_X, Min_X, Max_X);

        transform.position = new Vector3(Raw_X, transform.position.y, transform.position.z);



        if (transform.position.x <= -8)
        {
            transform.position = new Vector3(-8, transform.position.y, transform.position.z);

        }
        else if (transform.position.x >= 8)
        {
            transform.position = new Vector3(8, transform.position.y, transform.position.z);

        }
    }

    public void Shoot()
    {
        GameObject shot = Instantiate(Lazer, shotPoint.position, Quaternion.identity) as GameObject;

        if (shot.GetComponent<Rigidbody2D>() != null)
        {
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);

            SoundManager.clip = shotSound;

            //if(SoundManager.isPlaying == false){
            SoundManager.Play();
            //}
        }
        else
        {

        }
    }
}
