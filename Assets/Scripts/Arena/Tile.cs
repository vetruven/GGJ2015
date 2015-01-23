using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public GameObject cap;
    public Vector2 coord;
    public bool isTaken { get { return !cap.activeInHierarchy; } }



}
