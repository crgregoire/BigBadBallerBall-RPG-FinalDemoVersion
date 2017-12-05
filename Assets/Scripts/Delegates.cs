using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Various delegates.
public delegate bool Condition();
public delegate void Actor();
public delegate void MoveDel(Vector3 direction, Rigidbody rb, float speed);
public delegate void Producer(int level, GameObject from);