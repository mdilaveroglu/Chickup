using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float eggSpeed;
    public float eggRotationSpeed;

    public Vector2 direction;
    public Sprite[] sprites;

    private Rigidbody2D rb;
    private float eggRotationDegree;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, 3)];
        rb = GetComponent<Rigidbody2D>();
        eggRotationDegree = Random.Range(90, 180);

        Destroy(gameObject, 3);
    }

     void Update()
    {
        transform.Rotate(0f, 0f, eggRotationDegree * eggRotationSpeed * Time.deltaTime, Space.Self);
        transform.Translate(direction * Time.deltaTime * eggSpeed, Space.World);
    }
}
