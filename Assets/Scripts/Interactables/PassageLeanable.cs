using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageLeanable : FixedLeanable
{
    private BoxCollider oppositeWallCollider = null;

    public BoxCollider OppositeWall { get => oppositeWallCollider; }

    // TODO Snap- und Resettransforms automatisch adden  --> Validate-Ticket

    // TODO sch�n machen  --> Validate-Ticket
    protected override void Validate()
    {
        base.Validate();

        // TODO M�glichkeit finden zweite Wand zu detecten und ggf. hinzuzuf�gen, sonst per Skript pr�fen (Skript anh�ngen, dass Cube definiert) oder Prefab  --> Validate-Ticket
        BoxCollider[] children = transform.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider child in children)
        {
            if(child.CompareTag("OppositeWall"))
            {
                oppositeWallCollider = child;
            }
        }


        foreach(BoxCollider child in children)
        {
            if(child.gameObject.transform.CompareTag("OppositeWall") == false)
            {
                snapCollider = child;
            }
        }

        if(oppositeWallCollider == null)
        {
            Debug.LogError("No opposite wall found!");
        }

        if(snapCollider == null)
        {
            GameObject emptyGameObject = new GameObject("Collider");
            emptyGameObject.transform.SetParent(transform);

            emptyGameObject.AddComponent(objectCollider.GetType());
            emptyGameObject.transform.localPosition = Vector3.zero;
            snapCollider = emptyGameObject.GetComponent<Collider>();
        }
    }
}
