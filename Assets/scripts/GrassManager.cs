using UnityEngine;
using System.Collections;

public class GrassManager : MonoBehaviour {

    public Transform prefab;
    public float delta;
    public float count;
    public float rangex;
    public float startx;
    public float starty;
    public float rangey;
    

    // Use this for initialization
    void Start () {
        for (int n = 0; n < count; n++) {
            float x = startx + delta * n + Random.Range(-rangex, rangex);
            float y = starty + Random.Range(-rangey, rangey);

            Transform newgrass = (Transform)Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            Animator anim = newgrass.GetComponent<Animator>();
            anim.Play("grass", -1, Random.Range(0f, 1f));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
