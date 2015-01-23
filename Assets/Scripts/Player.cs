using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject turret;
    public GameObject pelvis;

    public string playerName = "Player1";
    public float moveSpeed = 10;
    public float turnSpeed = 10;

    private CharacterController cc;

    void FixedUpdate()
    {
        if (GameModel.isPlaying)
            GetInputForPlayer();
    }

    private void GetInputForPlayer()
    {
        Vector3 moveDelta = Vector3.zero;
        moveDelta.x = Input.GetAxis(playerName + "MoveHor");
        moveDelta.z = Input.GetAxis(playerName + "MoveVert");

        if (moveDelta != Vector3.zero)
        {
            rigidbody.MovePosition(rigidbody.position + moveDelta * moveSpeed * Time.deltaTime);
            ChangeRotation(moveDelta, pelvis);
        }

        Vector3 turretRotateDir = Vector3.zero;
        turretRotateDir.x = Input.GetAxis(playerName + "ShootHor");
        turretRotateDir.z = Input.GetAxis(playerName + "ShootVert");

        if (turretRotateDir != Vector3.zero)
            ChangeRotation(turretRotateDir, turret);
    }

    private void ChangeRotation(Vector3 lookToVector, GameObject objToRotate)
    {
        Quaternion lookRot = Quaternion.LookRotation(lookToVector);
        Quaternion newRot = Quaternion.RotateTowards(objToRotate.transform.rotation, lookRot, turnSpeed);
        objToRotate.transform.rotation = newRot;
    }
}
