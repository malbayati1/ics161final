﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour, IHoldable
{
    private const float PICKUPCOOLDOWN = 1f;

    public bool isHeld;

    private bool canBePickedUp;

    protected GameObject heldBy;

    protected virtual void Start()
    {
        canBePickedUp = true;
    }

    public virtual bool Use(GameObject user)
    {
		return true;
    }

    public virtual void Drop(GameObject droppedBy)
    {
		this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        StartCoroutine(PickupCooldown());
    }

    IEnumerator PickupCooldown()
    {
        isHeld = false;
        heldBy = null;
        yield return new WaitForSeconds(PICKUPCOOLDOWN);
        canBePickedUp = true;
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        //Debug.Log("entering");
        if (canBePickedUp && col.gameObject.CompareTag("Player"))
        {
            if (col.transform.parent.GetComponent<PlayerInteraction>().TryToPickUp(gameObject, this))
            {
                GetPickedUp(col);
            }
        }
    }

    protected virtual void GetPickedUp(Collider col)
    {
        canBePickedUp = false;
        isHeld = true;
        heldBy = col.transform.parent.gameObject;
    }
}
