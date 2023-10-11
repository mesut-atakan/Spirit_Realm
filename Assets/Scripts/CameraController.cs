using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



internal class CameraController : MonoBehaviour
{
#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

    [Header("Camera Properties")]


    [Tooltip("Camera Movement Speed")]
    [SerializeField] [Range(0.0001f, 0.2f)]private float cameraMoveSpeed = 0.05f;

    [Tooltip("Camera Componentini bu degiskene atayin!")]
    [SerializeField] private new Camera camera;
    



    [Tooltip("Assign the character's transform component to this variable!")]
    [SerializeField] private Transform characterTransform;
    
    
    

#endregion ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||






#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||


    private Vector2 _screenResolution;


#endregion ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||





    private void Awake() {
        this._screenResolution = GetScreenResolution();
    }


    private void LateUpdate() {
        CameraMove();
    }





    internal void CameraMove()
    {
        this.transform.position = Vector3.Lerp(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector3(this.characterTransform.position.x, this.characterTransform.position.y, this.transform.position.z), this.cameraMoveSpeed);
    }





    /// <summary>
    /// With this method, you will be able to find out the resolution of the player's screen!
    /// </summary>
    /// <returns>Player's screen resolution will be restored!</returns>
    internal Vector2 GetScreenResolution()
    {
        // ~~ Variables ~~
        Vector2 _screenResolution;

        _screenResolution = new Vector2(Screen.width, Screen.height);

        return _screenResolution; 
    }
}