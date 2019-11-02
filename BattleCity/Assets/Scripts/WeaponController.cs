using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    GameObject canonBall;
    Projectile canon;
    [SerializeField]
    int speed;
    void Start()
    {
        canonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        canon = canonBall.GetComponent<Projectile>();
        canon.speed = speed;
    }
    public void Fire()
    {
        if (canonBall.activeSelf == false)
        {
            canonBall.transform.position = transform.position;
            canonBall.transform.rotation = transform.rotation;
            canonBall.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        if (canonBall != null) canon.DestroyProjectile();
    }
}
