using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public List<Material> backgrounds;
    private MeshRenderer rendererRef;

    private void Start() {
        rendererRef = GetComponent<MeshRenderer>();
    }

    public void changeBackGround(int index){
        rendererRef.material = backgrounds[index];
    }
}
