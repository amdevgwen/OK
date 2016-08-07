using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlowShitUp : MonoBehaviour {

    public List<Rigidbody> rocks = new List<Rigidbody>();

    public float boomSize = 30f, boomRange = 10f;

    public Transform wall;
    public void Boom()
    {
        foreach (var t in rocks)
        {
            var coll = t.GetComponent<Collider>();
            if (coll != null)
                coll.enabled = true;

            t.isKinematic = false;
            t.AddExplosionForce(boomSize, transform.position, boomRange);
            StartCoroutine(deathofasalesman(t.transform));
        }
        wall.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            Boom();
        }
    }

    public float lifetime;
    IEnumerator deathofasalesman(Transform k)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(k.gameObject);
    }
}
