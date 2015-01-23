using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject turret;
    public string playerName = "Player1";
    public float moveSpeed = 10;
    public float turnSpeed = 10;

    private CharacterController cc;

    void Update()
    {
        if (GameModel.isPlaying)
        {
            GetInputForPlayer();
        }
    }

    private void GetInputForPlayer()
    {
        Vector3 moveDelta = Vector3.zero;
        moveDelta.x = moveSpeed * Input.GetAxis(playerName + "MoveHor");
        moveDelta.z = moveSpeed * Input.GetAxis(playerName + "MoveVert");
        rigidbody.MovePosition(rigidbody.position + moveDelta * Time.deltaTime);
        
        Vector3 turretRotateDir = Vector3.zero;
        turretRotateDir.x = Input.GetAxis(playerName + "ShootHor");
        turretRotateDir.z = Input.GetAxis(playerName + "ShootVert");

        if (turretRotateDir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(turretRotateDir);
            Quaternion newRot = Quaternion.RotateTowards(turret.transform.rotation, lookRot, turnSpeed);
            turret.transform.rotation = newRot;

            Debug.Log("turretRotateDir =" + turretRotateDir + " : lookRot=" + lookRot.eulerAngles + " : newRot=" + newRot.eulerAngles);
        }


        //overall angle from the forward direction
        //float angle = Vector3.Angle(transform.forward, turretRotateDir);
        //angle = Vector3.Dot(Vector3.right, turretRotateDir) > 0.0 ? angle : -angle;

        //= ;
        //
        //= playerName + "MoveVert";
        //
        //= playerName + "ShootHor";
        //
        //= playerName + "ShootVert";
    }
}
