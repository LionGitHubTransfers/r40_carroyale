using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private Collider _childCollider;
    private Rigidbody _childRigidbody;
    private Transform _transform;

    private bool _frizY = false;

    public void Awake()
    {
        _childCollider = GetComponent<Collider>();
        _childRigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    public void Init()
    {
        transform.parent = GameController.Controller.ControllerLevel.PatentFragment;
        //_childCollider.enabled = true;
        _childRigidbody.useGravity = true;
        _childRigidbody.isKinematic = false;
        //_childRigidbody.mass = 150;
        //_childRigidbody.velocity = new Vector3(Random.Range(-1,1) * GameController.Controller.Config.VelocityFragments,
        //                                        Random.Range(-1, 1) * GameController.Controller.Config.VelocityFragments,
        //                                        Random.Range(-1, 1) * GameController.Controller.Config.VelocityFragments);

        _childRigidbody.velocity = Random.insideUnitSphere * GameController.Controller.Config.VelocityFragments;
        StartCoroutine(ChangeScale());

        // Invoke("DisableCollider", GameController.Controller.Config.TimeLifeFragment);
    }

    private void Update()
    {
        if (_transform.position.y < 0 && _frizY == false)
        {
            _childRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            _frizY = true;
        }
    }

    public void DisableCollider()
    {
        _childCollider.enabled = false;
        Invoke("DestroyFragment", 2);
    }

    public void DestroyFragment()
    {
        Destroy(gameObject);
    }

    private IEnumerator ChangeScale()
    {
        Vector3 startVector = _transform.localScale;
        yield return new WaitForSeconds(GameController.Controller.Config.TimeLifeFragment);
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            _transform.localScale = Vector3.Lerp(startVector, Vector3.zero, time);
            yield return null;
        }

        DestroyFragment();
    }
}
