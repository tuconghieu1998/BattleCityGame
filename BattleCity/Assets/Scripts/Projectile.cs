using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    public bool destroySteel = false;
    bool toBeDestroyed = false;
    GameObject brickGameObject, steelGameObject;
    Tilemap tilemap;
    public int speed = 1;
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
        brickGameObject = GameObject.FindGameObjectWithTag("Brick");
        steelGameObject = GameObject.FindGameObjectWithTag("Steel");
    }
    void OnEnable()
    {
        if (rb2d != null)
        {
            rb2d.velocity = transform.up * speed;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector2.zero;
        tilemap = collision.gameObject.GetComponent<Tilemap>();
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage();
        }
        if ((collision.gameObject == brickGameObject) || (destroySteel && collision.gameObject == steelGameObject))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
        //keep the projectile inactive if hit anything. this will allow the projectile to be reused instead of wasting resource for garbage collector to clear it from memory
        this.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        if (toBeDestroyed)
        {
            Destroy(this.gameObject);
        }
    }
    //function called from Tank to destroy the projectile when the tank is destroyed
    public void DestroyProjectile()
    {
        //if the projectile is already inactive, destroy the projectile gameobject
        if (gameObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
        //set flag toBeDestroyed so that if projectile is still active checking the flag toBeDestroyed during onDisable to destroy the projectile
        toBeDestroyed = true;
    }
}