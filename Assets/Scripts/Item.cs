using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform ItemTransform;
    public Rigidbody ItemRigidbody;
    public void Init()
    {
        gameObject.SetActive(true);
        StartCoroutine(SpawnEffect());
    }

    private IEnumerator SpawnEffect()
    {
        ItemTransform.parent = GameController.Controller.PatentItems;

        float time = 0;
        ItemRigidbody.AddForce(0, 5, 0);
        while (time < 0.2f)
        {
            time += Time.deltaTime;

            ItemTransform.localScale = Vector3.one * (time / 0.2f);

            yield return null;
        }
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
