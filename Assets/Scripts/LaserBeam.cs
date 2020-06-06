using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Color laserColor;
    public float laserInitialWidth = 0.1f;
    public float laserFinalWidth = 0.2f;
    public float laserMaxDistance = 50f;

    private LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        //CreateLeaser();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (EnemyMover.em.shootLaserBeam)
        {
            Debug.Log("Shooting");
            shootLaser();
            
        }*/
        ShootLaser();
    }


    private void ShootLaser()
    {
        //CreateLeaser();
        Vector3 updateLaserDistance = transform.position + transform.forward * laserMaxDistance;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserMaxDistance))
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, hit.point);
            //now have to attack player
            // if hit the player then player will be damaged
            
        }
        else
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, updateLaserDistance);
        }
    }

    private void CreateLeaser()
    {
        // creating line render to create laser.
        laser = gameObject.AddComponent<LineRenderer>();
        laser.material = new Material(Shader.Find("Standard"));
        laser.SetColors(laserColor, laserColor);
        laser.SetWidth(laserInitialWidth, laserFinalWidth);
        laser.SetVertexCount(2); //setting line segments; but sure though

        
    }
}
