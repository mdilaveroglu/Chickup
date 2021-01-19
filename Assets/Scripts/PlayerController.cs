using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float boostForce;
    public float lowBoostForce;
    [Range(0,1)]
    public float horizontalBoostUpForce;

    public Sprite[] legSprites;
    public GameObject eggPrefab;

    GameObject legsGameObject;
    private GameObject pivot;
    private Rigidbody2D rb;

    private Vector2 boostDirection = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CreateLegs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            boostDirection = Vector2.left + Vector2.up;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            boostDirection = Vector2.right + Vector2.up;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            boostDirection = Vector2.left + Vector2.up * horizontalBoostUpForce;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            boostDirection = Vector2.right + Vector2.up * horizontalBoostUpForce;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            boostDirection = Vector2.up;
        } else
        {
            boostDirection = Vector2.zero;
        }

        ChangeLegSpriteBasedOnDirection(boostDirection);


        if (Input.GetMouseButtonDown(0))
        {
            AddVelocity(boostForce);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            AddVelocity(lowBoostForce);
        }
    }

    private void AddVelocity(float boostForce)
    {
        if (boostDirection == Vector2.zero)
        {
            return;
        }

        rb.velocity = boostDirection * boostForce;
        CreateEgg(-boostDirection);
    }

    private void CreateLegs()
    {
        pivot = new GameObject();

        pivot.transform.parent = transform;
        pivot.transform.position = transform.position + Vector3.down * .919f;

        GameObject initialLegGameObject = new GameObject();
        SpriteRenderer renderer = initialLegGameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = legSprites[0];

        legsGameObject = Instantiate(initialLegGameObject, pivot.transform.position + Vector3.down * .077f, Quaternion.identity);
        legsGameObject.transform.parent = pivot.transform;

        Destroy(initialLegGameObject);
    }

    private void CreateEgg(Vector2 direction)
    {
        eggPrefab.GetComponent<Egg>().direction = direction;
        Instantiate(eggPrefab, pivot.transform.position, Quaternion.identity);
    }

    private void ChangeLegSpriteBasedOnDirection(Vector2 direction)
    {

        if (direction == Vector2.left + Vector2.up || direction == Vector2.right + Vector2.up)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[2];
        }
        else if (direction == Vector2.right + Vector2.up * horizontalBoostUpForce || direction == Vector2.left+ Vector2.up * horizontalBoostUpForce)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[1];
        }
        else if (direction == Vector2.up)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[3];
        }
        else
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            LevelManager.SpawnPlayer();
            Destroy(gameObject);
        }
    }

}
