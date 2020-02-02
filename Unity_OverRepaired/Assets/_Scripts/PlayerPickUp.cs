using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private bool holding = false;
    private bool interacting = false;
    private bool throwing = false;
    GameObject pickedUpGameobject;
    GameObject interactObject;
    [SerializeField] Transform pickUpPosition;

    [SerializeField] float pickUpSpeed;

    [SerializeField] float pickUpRadius;
    [SerializeField] float interactRadius;
    [SerializeField] LayerMask collideWithPickUp, collideWithWorkBench;
    GameObject closestObject;
    [SerializeField] int pickdUpLayer, droppedLayer;
    [SerializeField] bool secondPlayer = false;

    GameObject throwableObject;

    bool findClosestFirst = false;

    private PlayerMovement pM;
    [SerializeField] Animator anim;

    Vector3 finalPosition;

    [SerializeField] float throwWaitTimer;

    [SerializeField] AudioManager audioManager;

    void Start()
    {
        pM = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Collider[] pickUpHits = Physics.OverlapSphere(transform.position, pickUpRadius, collideWithPickUp);
        Collider[] workBenchHits = Physics.OverlapSphere(transform.position, interactRadius, collideWithWorkBench);

        string interactName = !secondPlayer ? "Interact1" : "Interact2";
        string pickUpName = !secondPlayer ? "PickUp1" : "PickUp2";

        finalPosition = pickUpPosition.position;

        if (Input.GetButton(interactName) && holding)
        {
            if (!findClosestFirst)
            {
                var closest = GetClosest(workBenchHits);
                interactObject = closest?.gameObject;
                findClosestFirst = true;
            }

            if(interactObject != null && interactObject.layer == 10)
            {
                if(interactObject.GetComponent<BrokenRobot>().setPart(pickedUpGameobject.GetComponent<RobotPart>()))
                {
                    holding = false;
                    pickedUpGameobject = null;
                    interactObject = null;
                    findClosestFirst = false;
                    interacting = false;
                }
            }
            else
            {
                if (interactObject == null)
                {
                    DropObject(pickedUpGameobject);
                    holding = false;
                    pickedUpGameobject = null;
                }
                else
                {
                    interacting = true;

                    RepairStation repairS = interactObject.GetComponent<RepairStation>();

                    if (repairS.work(pickedUpGameobject.GetComponent<RobotPart>()))
                    {
                        pickedUpGameobject.transform.position = interactObject.transform.GetChild(0).position;
                        transform.position = interactObject.transform.GetChild(1).position;
                        transform.rotation = Quaternion.Euler(repairS.lookDirection);
                        pM.StopMovement(repairS.lookDirection.y);
                    }
                    else
                    {
                        pM.ContinueMovement();
                        interacting = false;
                    }
                }
            }
        }
        else if(Input.GetButtonUp(interactName))
        {
            interactObject?.GetComponent<RepairStation>()?.resetWork();

            interacting = false;
            interactObject = null;
            findClosestFirst = false;

            pM.ContinueMovement();
        }

        if (Input.GetButtonDown(pickUpName) && !interacting)
        {
            if(!holding && anim.GetCurrentAnimatorStateInfo(1).IsName("Nothing Scene"))
            {
                var closest = GetClosest(pickUpHits);

                pickedUpGameobject = closest?.gameObject.layer == 10 ? closest?.GetComponent<BrokenRobot>()?.getPart()?.gameObject : closest?.gameObject;

                if (pickedUpGameobject != null)
                {
                    PickUpObject(pickedUpGameobject);
                    holding = true;
                }
            }
            else if(holding)
            {
                //ThrowObject(pickedUpGameobject);
                throwableObject = pickedUpGameobject;
                throwing = true;
                holding = false;
                pickedUpGameobject = null;
            }
        }

        if (holding && !interacting)
        {
            pickedUpGameobject.transform.position = finalPosition;
            pickedUpGameobject.transform.rotation = pickUpPosition.rotation;
        }

        if(throwing)
        {
            throwableObject.transform.position = finalPosition;

            if(anim.GetCurrentAnimatorStateInfo(1).IsName("Throw") && anim.GetCurrentAnimatorStateInfo(1).normalizedTime > throwWaitTimer)
            {
                ThrowObject(throwableObject);
                throwableObject = null;
                throwing = false;
            }
        }
    }

    void PickUpObject(GameObject objectToPickUp)
    {
        objectToPickUp.GetComponent<Collider>().isTrigger = true;
        objectToPickUp.GetComponent<Rigidbody>().isKinematic = true;
        objectToPickUp.layer = pickdUpLayer;
        audioManager?.RobotPickup();
    }
    void ThrowObject(GameObject objectToThrow)
    {
        objectToThrow.GetComponent<Rigidbody>().isKinematic = false;
        objectToThrow.GetComponent<Rigidbody>().AddForce(transform.forward * 30, ForceMode.Impulse);

        objectToThrow.GetComponent<Collider>().isTrigger = false;
        objectToThrow.layer = droppedLayer;
        audioManager?.RobotThrow();
    }

    void DropObject(GameObject objectToDrop)
    {
        objectToDrop.GetComponent<Rigidbody>().isKinematic = false;
        objectToDrop.GetComponent<Collider>().isTrigger = false;
        objectToDrop.layer = droppedLayer;
        audioManager?.RobotDrop();
    }

    GameObject GetClosest(Collider[] sphereHits)
    {
        Vector3 newTransform = transform.position;
        float destination = Mathf.Infinity;
        GameObject closest = null;

        foreach (Collider r in sphereHits) // Find The Closest Object & Set It Into closestObject Gameobject
        {
            Vector3 diff = r.transform.position - newTransform;
            float newDistance = diff.sqrMagnitude;

            if (destination > newDistance && newDistance >= 0)
            {
                destination = newDistance;
                closest = r.transform.gameObject;
            }
        }

        return closest;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    public bool GetIfHolding()
    {
        return holding;
    }
    public bool GetIfInteracting()
    {
        return interacting;
    }

    public bool GetIfThrow()
    {
        return throwing;
    }

    public void SetThrowToFalse()
    {
        throwing = false;
    }
}