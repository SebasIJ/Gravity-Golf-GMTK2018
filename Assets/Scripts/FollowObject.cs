using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform followTransform;

    private void Update() {
        //updates transform to the followed transform
        transform.position = followTransform.position;
    }
}
