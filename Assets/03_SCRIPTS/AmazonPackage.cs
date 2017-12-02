using UnityEngine;

public class AmazonPackage : MonoBehaviour
{
	public GameObject packageContents;

	private void OnCollisionEnter( Collision collision )
	{
		packageContents.transform.position = transform.position;
		packageContents.transform.rotation = Quaternion.identity;
		packageContents.SetActive( true );

		packageContents.GetComponent<Rigidbody>().AddForce( Vector3.up * 20, ForceMode.Impulse );
		Destroy( gameObject );
	}
}
