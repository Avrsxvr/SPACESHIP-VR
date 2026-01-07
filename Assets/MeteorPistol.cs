using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MeteorPistol : MonoBehaviour
{
    public ParticleSystem particles;
    public LayerMask layerMask;
    public Transform shootSOurce;
    public float distance = 10;

    private bool rayActivate = false;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => FireMeteor(x)); 
        grabInteractable.deactivated.AddListener(x => StopFiringMeteor(x));
        
    }

    public void FireMeteor(ActivateEventArgs args)
    {
        particles.Play();
        rayActivate = true;
        Debug.Log("Firing Meteor!");
    }

    public void StopFiringMeteor(DeactivateEventArgs args)
    {
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        rayActivate = false;
        Debug.Log("Stopped Firing Meteor!");
    }

    void Update()
    {
        if (rayActivate)
             RaycastCheck();
    }


    void RaycastCheck()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(shootSOurce.position, shootSOurce.forward, out hit, distance, layerMask);
        if (hasHit)
        {
            hit.transform.gameObject.SendMessage("Break", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Hit: " + hit.collider.name); 

    }
}
}
