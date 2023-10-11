using UnityEngine;



namespace Character
{
    internal abstract class CharacterProperties : MonoBehaviour
    {
#region Serialize Fields

        [Header("Character Properties")]

        [Tooltip("Character Move Speed")]
        [SerializeField] private float characterMoveSpeed;



        
        [Tooltip("Character Dead")]
        [SerializeField] private bool dead = false;
        
#endregion




#region Properties

        internal float _characterMoveSpeed
        {
            get { return this.characterMoveSpeed; }
        }

        internal bool _dead
        {
            get { return this.dead; }
            set { this.dead = value; }
        }

#endregion





        internal abstract void Move();


        internal abstract void Damage(CharacterProperties characterProperties);

        internal abstract void TakeDamage(CharacterProperties characterProperties);
    }
}