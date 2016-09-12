using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

    public static readonly Utility _util = new Utility();


	// Use this for initialization
	void Start () {
	
	}
	
    public float RotateTowards(Vector3 targetDir, Transform start)
    {
        targetDir.z = start.transform.position.z;
        targetDir = targetDir.normalized;

        Vector3 rel = start.InverseTransformDirection(targetDir);
        float rotAngle = Mathf.Atan2(rel.y, rel.x) * Mathf.Rad2Deg;

        return rotAngle;
    }

}
