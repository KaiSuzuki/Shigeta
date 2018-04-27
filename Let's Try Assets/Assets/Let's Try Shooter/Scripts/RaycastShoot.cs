using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;


    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserline;
    private float nextFire;
    

	void Start () {
        laserline = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserline.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weponRange))
            {
                laserline.SetPosition(1, hit.point);
            }
            else
            {
                laserline.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weponRange));
            }


        }
	}

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();

        laserline.enabled = true;
        yield return shotDuration;
        laserline.enabled = false;
    }
}
