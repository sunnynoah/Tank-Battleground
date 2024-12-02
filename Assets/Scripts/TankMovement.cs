using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    InputControls controls;

    Vector2 movement;

    Vector2 turn;

    Rigidbody rb;

    public float speed;

    public float tankTurn;

    GameObject turret;
    GameObject firePoint;

    public GameObject shootParticles;

    public GameObject bulletPrefab;

    float shootCooldown;

    public bool firing = false;

    public AudioClip shootSound;

    public string powerup;

    public float powerupTime;

    private PlayerInput playerInput;
    private InputAction moveAction, turnAction, shootAction;

    private void Start()
    {
        // Input Stuff
        playerInput = GetComponent<PlayerInput>();
        var map = playerInput.actions.FindActionMap(playerInput.defaultActionMap);

        moveAction = map.FindAction("Move");
        turnAction = map.FindAction("TurretTurn");
        shootAction = map.FindAction("Fire");

        moveAction.performed += ctx => movement = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movement = Vector2.zero;

        turnAction.performed += ctx => turn = ctx.ReadValue<Vector2>();
        turnAction.canceled += ctx => turn = Vector2.zero;

        shootAction.performed += ctx => firing = true;
        shootAction.canceled += ctx => firing = false;

        // Other Stuff

        rb = GetComponent<Rigidbody>();

        turret = transform.GetChild(0).gameObject;

        firePoint = turret.transform.GetChild(0).gameObject;

    }
    private void Update()
    {
        if (powerupTime > 0)
        {
            powerupTime -= Time.deltaTime;
            if (powerupTime <= 0)
            {
                powerup = null;
            }
        }
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (firing)
        {
            TankShoot();
        }
    }

    private void FixedUpdate()
    {
        float finalSpeed = speed;
        float finalTurn = tankTurn;
        if (powerup == "Increased Speed")
        {
            finalSpeed *= 2f;
            finalTurn *= 2f;
        }
        Vector3 rotation = transform.forward * finalSpeed * movement.y;
        rb.velocity = new Vector3(rotation.x, rb.velocity.y, rotation.z);

        Vector3 currentEulerAngles = transform.rotation.eulerAngles;
        currentEulerAngles.y += movement.x * finalTurn;
        transform.rotation = Quaternion.Euler(currentEulerAngles);

        if (movement == Vector2.zero)
        {
            Vector3 turretAngle = turret.transform.rotation.eulerAngles;
            turretAngle.y += turn.x * (finalTurn * 1.5f);
            turret.transform.rotation = Quaternion.Euler(turretAngle);
        }

        //TankShoot();
    }

    private void TankShoot()
    {
        if (shootCooldown <= 0)
        {
            PlaySound(shootSound);
            if (powerup == "Increased Firerate")
            {
                shootCooldown = 0.6f;
            }
            else
            {
                shootCooldown = 1;
            }
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

            float bulletSpeed = 30;
            if (powerup == "Huge Bullet")
            {
                bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                bulletSpeed = 50;
            }

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletSpeed;

            GameObject shootPars = Instantiate(shootParticles, firePoint.transform.position, turret.transform.rotation);
            if (this.name == "P2 Tank")
            {
                shootPars.transform.rotation = Quaternion.Euler(shootPars.transform.rotation.x, 180 + shootPars.transform.rotation.eulerAngles.y, shootPars.transform.rotation.z);
            }
            //shootPars.transform.SetParent(turret.transform);
            Destroy(shootPars, 2);
        }
    }

    void PlaySound(AudioClip clip)
    {
        AudioSource source = GetComponent<AudioSource>();
        //source.clip = clip;
        //source.Play();
        source.PlayOneShot(clip);
    }

}
