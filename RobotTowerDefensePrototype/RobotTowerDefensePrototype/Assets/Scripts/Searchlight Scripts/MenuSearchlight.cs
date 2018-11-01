
using UnityEngine;

public class MenuSearchlight : MonoBehaviour
{
	private Ray _ray;
	private RaycastHit _mousePos;
	Vector3 screenPoint;
	Quaternion currentPos;
	private float speed = 1f;


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(_ray, out _mousePos,Mathf.Infinity,~(1 << LayerMask.NameToLayer("searchlight"))))
		{
			screenPoint = _mousePos.point;
		}

		//Vector3 direction = screenPoint - transform.position;
		Quaternion currentPos = this.transform.rotation;
		Quaternion rotation = Quaternion.LookRotation(screenPoint);
		transform.rotation = Quaternion.Lerp(currentPos, rotation, speed * Time.deltaTime);
	}
}
