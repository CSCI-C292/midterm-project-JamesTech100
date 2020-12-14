using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _speed = 5;
    private Rigidbody2D playerBody;
    private Vector3 movement;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    public GameObject charSpriteR;
    public GameObject charSpriteL;
    public GameObject gunSpriteR;
    public GameObject gunSpriteL;
    public GameObject gunSpriteU;
    public GameObject gunSpriteD;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float shootHor = Input.GetAxis("Fire - Horizontal");
        float shootVert = Input.GetAxis("Fire - Vertical");
        if((shootHor != 0 || shootVert !=0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        //movement
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement != Vector3.zero)
        {
            MovePlayer();
        }

        updateSprite(shootHor, shootVert, movement.x);
    }

    void updateSprite(float shootHoriz, float shootVertic, float moveHoriz)
    {
        if (shootHoriz > 0)                     // Changes the sprite by whichever direction the character shoots
        {
            gunSpriteR.gameObject.SetActive(true);
            gunSpriteL.gameObject.SetActive(false);
            gunSpriteU.gameObject.SetActive(false);
            gunSpriteD.gameObject.SetActive(false);
        } else if (shootHoriz < 0)
        {
            gunSpriteR.gameObject.SetActive(false);
            gunSpriteL.gameObject.SetActive(true);
            gunSpriteU.gameObject.SetActive(false);
            gunSpriteD.gameObject.SetActive(false);
        } else if (shootVertic > 0)
        {
            gunSpriteR.gameObject.SetActive(false);
            gunSpriteL.gameObject.SetActive(false);
            gunSpriteU.gameObject.SetActive(true);
            gunSpriteD.gameObject.SetActive(false);
        } else if (shootVertic < 0)
        {
            gunSpriteR.gameObject.SetActive(false);
            gunSpriteL.gameObject.SetActive(false);
            gunSpriteU.gameObject.SetActive(false);
            gunSpriteD.gameObject.SetActive(true);
        }

        if(moveHoriz == 0)                      // Changes character sprite by how the character moves horizontally
        {
            return;
        } else if(moveHoriz > 0)
        {
            charSpriteR.SetActive(true);
            charSpriteL.SetActive(false);
        } else if(moveHoriz < 0)
        {
            charSpriteR.SetActive(false);
            charSpriteL.SetActive(true);
        }


    }

    void MovePlayer()
    {
        playerBody.MovePosition(
            transform.position + movement.normalized * _speed * Time.deltaTime
            );
        //Debug.Log(movement * _speed * Time.deltaTime);
    }

    void Shoot(float x, float y)
    {
        //Debug.Log("Bullet Sent: (" + x + "," + y + ")");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0);

        if (y != 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0,0,90);
            //Debug.Log("it should've rotated"); //update - it rotated at last :D
        }
    }
}
