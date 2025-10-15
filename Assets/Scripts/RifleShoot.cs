using UnityEngine;

public class RifleShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzlePoint;
    public Animator playerAnimator;
    public float fireRate = 0.2f; // time between shots

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        // Fire animation
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("isFiring"); // should trigger RifleFire animation
        }

        // Spawn bullet
        if (bulletPrefab != null && muzzlePoint != null)
        {
            Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        }

        // Recoil effect
        StartCoroutine(RecoilKick());
    }

    System.Collections.IEnumerator RecoilKick()
    {
        Vector3 originalPos = transform.localPosition;
        transform.localPosition -= transform.forward * 0.05f; // small recoil
        yield return new WaitForSeconds(0.05f);
        transform.localPosition = originalPos;
    }
}
