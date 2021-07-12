using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Controller;

    public Transform PatentItems;
    public Transform PatentFragment;

    public Transform PatentItemsl { get; internal set; }

    private void Awake()
    {
        if (Controller == null)
        {
            Controller = this;
            return;
        }

        Destroy(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
