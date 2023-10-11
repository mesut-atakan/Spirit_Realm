using Character;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;



internal class GameManager : MonoBehaviour
{
#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

    [Header("Classes")]

    [Tooltip("Assigning the class designed for the player to manage his own character to this variable!")]
    [SerializeField]
    private PlayerController playerController;








    [Header("UI Objects")]
    

    [Tooltip("Coint Text")]
    [SerializeField] private TextMeshProUGUI coinText; 


    [Tooltip("GAME OVER PANEL")]
    [SerializeField] private GameObject gameOverPanel; 



    [Tooltip("GAME WIN PANEL")]
    [SerializeField] private GameObject gameWinPanel; 

#endregion ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||









#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||


    private ushort _playerCoin = 0;

    private ushort _coinMount = 5;


#endregion#endregion ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||






#region  ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PROPERTIES ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||


    internal TextMeshProUGUI _coinText
    {
        get { return this.coinText; }
        set { this.coinText = value; }
    }


#endregion














#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| MONOBEHAVIOUR FUNCTIONS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

    private void Awake() {
        if (this.playerController == null)
        {
            this.playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }


    private void Start() 
    {
        this.playerController._characterSpeedProperties = this.playerController._characterMoveSpeed;
    }








    private void Update() 
    {
        // ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PLAYER CONTROLLER||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

        if (this.playerController._dead == false)
        {
            this.playerController.Move();
        }

        //||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||
    }



    private void LateUpdate() {
        
    }




    /// <summary>
    /// Use this method to increase player's total rent
    /// </summary>
    internal void CoinCalculation()
    {
        this._playerCoin += this._coinMount;
        this.coinText.text = this._playerCoin.ToString();
    }








    internal IEnumerator GameOverPanel()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameLevel");
    }




    internal IEnumerator GameWinPanel()
    {
        this.gameWinPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameLevel");
    }


#endregion
}