using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float TimeToDestruct;
    public float StarPoinOfDamageReduction;
    public float FinalDamageInPercent;
    public Keyframe DamageReductionGraph;
    public int StartSpeed = 50;
    public GameObject ParticleHit;

    private Vector3 PerviousStep;
    private float StartTime; 
    private float EndTime; 

    private void Awake()
    {
        EndTime = 5;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.TransformDirection(Vector3.forward*StartSpeed);
        PerviousStep = gameObject.transform.position;
        DamageReductionGraph = new Keyframe(1, FinalDamageInPercent / 100);
    }

    void Update()
    {
        DestroyTime();
        Quaternion CurrentStep = gameObject.transform.rotation;
        transform.LookAt(PerviousStep, transform.up);

        OnDistance();

        gameObject.transform.rotation = CurrentStep;
        PerviousStep = gameObject.transform.position;
    }

    void OnDistance()
    {
        RaycastHit hit = new RaycastHit();
        float Distance = Vector3.Distance(PerviousStep, transform.position);
        if (Distance == 0.0f)
        {
            Distance = 1e-05f;
        }
        if (Physics.Raycast(PerviousStep, transform.TransformDirection(Vector3.back), out hit, Distance * 0.999f) && (hit.transform.gameObject != gameObject))
        {
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Ship>().Health--;
    }

    void DestroyTime()
    {

        StartTime += 0.1f * Time.deltaTime;

        if (StartTime >= EndTime)
        {
            Destroy(gameObject);
        }
    }
}
