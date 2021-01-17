using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float upForceAmount;
    public float sideForceAmount;

    public Sprite[] legSprites;
    public GameObject eggPrefab;

    private bool canMoveRight = true;
    private bool canMoveLeft = true;

    GameObject legsGameObject;
    private GameObject pivot;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CreateLegs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && canMoveLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            canMoveRight = true;
        }

        if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            canMoveLeft = true;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = rb.position;
        RaycastHit2D hit = Physics2D.Linecast(mousePosition, playerPosition);

        if (hit.collider != null && hit.collider.GetType() == typeof(CircleCollider2D))
        {
            float areaNumber = AreaNumber(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                AddVelocity(areaNumber);
            }

            ChangeLegSpriteBasedOnArea(areaNumber);
        }
    }

    private void AddVelocity(float areaNumber)
    { 
        if (areaNumber == 1 || areaNumber == 2 || areaNumber == 3 || areaNumber == 10 || areaNumber == 11 || areaNumber == 12)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } 
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


        if (areaNumber == 1 || areaNumber == 12)
        {
            rb.velocity = Vector2.up * upForceAmount + Vector2.left * 3 * sideForceAmount;
            CreateEgg(Vector2.down + Vector2.right * 3);
        }
        else if (areaNumber == 2 || areaNumber == 11)
        {
            rb.velocity = Vector2.up * 2 * upForceAmount + Vector2.left * 2 * sideForceAmount;
            CreateEgg(Vector2.down * 2 + Vector2.right * 2);
        }
        else if (areaNumber == 3 || areaNumber == 10)
        {
            rb.velocity = Vector2.up * 3 * upForceAmount + Vector2.left * sideForceAmount;
            CreateEgg(Vector2.down * 3 + Vector2.right);
        }
        else if (areaNumber == 4 || areaNumber == 9)
        {
            rb.velocity = Vector2.up * 3 * upForceAmount + Vector2.right * sideForceAmount;
            CreateEgg(Vector2.down * 3 + Vector2.left);
        }
        else if (areaNumber == 5 || areaNumber == 8)
        {
            rb.velocity = Vector2.up * 2 * upForceAmount + Vector2.right * 2 * sideForceAmount;
            CreateEgg(Vector2.down * 2 + Vector2.left * 2);
        }
        else if (areaNumber == 6 || areaNumber == 7)
        {
            rb.velocity = Vector2.up * upForceAmount + Vector2.right * 3 * sideForceAmount;
            CreateEgg(Vector2.down + Vector2.left * 3);
        }    
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
     //   eggGameObject.GetComponent<Rigidbody2D>().velocity = pivot.transform.TransformDirection(Vector3.down) * eggSpeed;
    }

    private void ChangeLegSpriteBasedOnArea(float areaNumber)
    {

        if (areaNumber == 1 || areaNumber == 12)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[1];
        }
        else if (areaNumber == 2 || areaNumber == 11)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[2];
        }
        else if (areaNumber == 3 || areaNumber == 10)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[3];
        }
        else if (areaNumber == 4 || areaNumber == 9)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[3];
        }
        else if (areaNumber == 5 || areaNumber == 8)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[2];
        }
        else if (areaNumber == 6 || areaNumber == 7)
        {
            legsGameObject.GetComponent<SpriteRenderer>().sprite = legSprites[1];
        }
    }

    private int AreaNumber(Vector3 hitPoint)
    {
        //Area 1
        Vector3 area1Bound1 = Vector3.right * 5f ;
        Vector3 area1Bound2 = new Vector3(.866f, .5f) * 5f;

        //Area 2
        Vector3 area2Bound1 = new Vector3(.866f, .5f) * 5f;
        Vector3 area2Bound2 = new Vector3(.5f, .866f) * 5f;

        //Area 3
        Vector3 area3Bound1 = new Vector3(.5f, .866f) * 5f;
        Vector3 area3Bound2 = Vector3.up * 5f;

        //Area 4
        Vector3 area4Bound1 = Vector3.up * 5f;
        Vector3 area4Bound2 = new Vector3(-.5f, .866f) * 5f;

        //Area 5
        Vector3 area5Bound1 = new Vector3(-.5f, .866f) * 5f;
        Vector3 area5Bound2 = new Vector3(-.866f, .5f) * 5f;

        //Area 6
        Vector3 area6Bound1 = new Vector3(-.866f, .5f) * 5f;
        Vector3 area6Bound2 = Vector3.left * 5f;

        //Area 7
        Vector3 area7Bound1 = Vector3.left * 5f;
        Vector3 area7Bound2 = new Vector3(-.866f, -.5f) * 5f;

        //Area 8
        Vector3 area8Bound1 = new Vector3(-.866f, -.5f) * 5f;
        Vector3 area8Bound2 = new Vector3(-.5f, -.866f) * 5f;

        //Area 9
        Vector3 area9Bound1 = new Vector3(-.5f, -.866f) * 5f;
        Vector3 area9Bound2 = Vector3.down * 5f;

        //Area 10
        Vector3 area10Bound1 = Vector3.down * 5f;
        Vector3 area10Bound2 = new Vector3(.5f, -.866f) * 5f;

        //Area 11
        Vector3 area11Bound1 = new Vector3(.5f, -.866f) * 5f;
        Vector3 area11Bound2 = new Vector3(.866f, -.5f) * 5f;

        //Area 12
        Vector3 area12Bound1 = new Vector3(.866f, -.5f) * 5f;
        Vector3 area12Bound2 = Vector3.right * 5f;

        if (IsPointInsideArea(hitPoint, area1Bound1, area1Bound2))
        {
            return 1;
        } 
        else if (IsPointInsideArea(hitPoint, area2Bound1, area2Bound2))
        {
            return 2;
        }
        else if (IsPointInsideArea(hitPoint, area3Bound1, area3Bound2))
        {
            return 3;
        }
        else if (IsPointInsideArea(hitPoint, area4Bound1, area4Bound2))
        {
            return 4;
        }
        else if (IsPointInsideArea(hitPoint, area5Bound1, area5Bound2))
        {
            return 5;
        }
        else if (IsPointInsideArea(hitPoint, area6Bound1, area6Bound2))
        {
            return 6;
        }
        else if (IsPointInsideArea(hitPoint, area7Bound1, area7Bound2))
        {
            return 7;
        }
        else if (IsPointInsideArea(hitPoint, area8Bound1, area8Bound2))
        {
            return 8;
        }
        else if (IsPointInsideArea(hitPoint, area9Bound1, area9Bound2))
        {
            return 9;
        }
        else if (IsPointInsideArea(hitPoint, area10Bound1, area10Bound2))
        {
            return 10;
        }
        else if (IsPointInsideArea(hitPoint, area11Bound1, area11Bound2))
        {
            return 11;
        }
        else if (IsPointInsideArea(hitPoint, area12Bound1, area12Bound2))
        {
            return 12;
        }

        return -1;
    }

    private bool IsPointInsideArea(Vector3 point, Vector3 bound1, Vector3 bound2)
    {
        float angleArea = Vector3.Angle(bound1, bound2);
        return Vector3.Angle(bound1, point - transform.position) < angleArea && Vector3.Angle(bound2, point - transform.position) < angleArea;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;

        if (collision.gameObject.CompareTag("Wall"))
        {

            if (normal == Vector2.right)
            {
                canMoveLeft = false;
            }

            if (normal == Vector2.left)
            {
                canMoveRight = false;
            }
        }
    }

}
