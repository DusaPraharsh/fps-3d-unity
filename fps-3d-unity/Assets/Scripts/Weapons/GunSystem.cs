using UnityEngine;
// using TMPro;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magSize, bulletPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    
    bool shooting, readyToShoot, reloading;

    [Header("References")]
    public Camera cam;
    public Transform attackPoint;
    public RaycastHit raycastHit;
    public LayerMask enemy;

    [Header("Camera Shake")]
    // public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake cameraShake;
    public float cameraShakeMagnitude, cameraShakeDuration;
    // public TextMeshProUGUI text;

    private void Awake()
    {
        bulletsLeft = magSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        // text.SetText(bulletsLeft + "/" + magSize);
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKeyDown(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(cam.transform.position, direction, out raycastHit, range, enemy))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                raycastHit.collider.GetComponent<Target>().TakeDamage(damage);
            }
        }

        cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude);
        // Instantiate(bulletHoleGraphic, raycastHit.point, Quaternion.Euler(0, 90, 0));
        // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

        Debug.Log("SHOTS FIRED!!!");
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }
}
