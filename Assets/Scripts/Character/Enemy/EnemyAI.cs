using UnityEngine;
using DG.Tweening;


namespace Character
{
    internal class EnemyAI : CharacterProperties
    {
#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

        [Header("Components")]

        [Tooltip("Karakterin rigidbody componentinin giriniz!")]
        [SerializeField]
        private Rigidbody2D rb;




        [Header("Points")]

        [Tooltip("Enter the first of the 2 points you want this character to go to!")]
        [SerializeField] private Transform aPoint;


        [Tooltip("Enter the 2nd of the 2 points you want the character to go to!")]
        [SerializeField] private Transform bPoint;

#endregion




#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

        private Transform _targetPosition;

#endregion






        private void Update() {
            Move();
        }




        internal override void Damage(CharacterProperties characterProperties)
        {
            
        }

        internal override void Move()
        {
            if (_targetPosition == null)
            {
                SelectTargetTransform();
            }

            this.rb.velocity = new Vector2(TargetPositionNegativeOrPositiveMultiply() * this._characterMoveSpeed, rb.velocity.y);


            if (TargetPositionOK())
            {
                SelectTargetTransform();
            }
        }
        

        internal override void TakeDamage(CharacterProperties characterProperties)
        {
            
        }








        /// <summary>
        /// The point with the furthest distance will be returned and at the same time, the value determined here will be entered into the `_targetTransform` variable!
        /// </summary>
        /// <returns>The Transform with the furthest distance will be returned!</returns>
        internal Transform SelectTargetTransform()
        {
            // ~~ Variables ~~
            float _aDistance, _bDistance;


            _aDistance = Mathf.Abs(this.transform.position.x - this.aPoint.position.x);
            _bDistance = Mathf.Abs(this.transform.position.x - this.bPoint.position.x);

            if (_aDistance >= _bDistance)
            {
                this._targetPosition = this.aPoint;
                return this.aPoint;
            }
            else if (_bDistance > _aDistance)
            {
                this._targetPosition = this.bPoint;
                return this.bPoint;
            }
            else
            {
                this._targetPosition = null;
                return null;
            }
        }


        /// <summary>
        /// This method will show in absolute value how far you are from the Transform component you entered as a parameter on the x-axis!
        /// </summary>
        /// <param name="targetPosition">Enter the Transform whose distance you are curious about as a parameter!</param>
        /// <returns>The x positions of the two transforms whose distances on the x axis are calculated will be returned in absolute value!</returns>
        internal float X_AxisDistance(Transform targetPosition)
        {
            return Mathf.Abs(targetPosition.position.x - this.transform.position.x);
        }




        /// <summary>
        /// If this object is close enough to the transform to be tracked, this method will return true!
        /// </summary>
        internal bool TargetPositionOK()
        {
            // ~~ Variables ~~ 
            float _distance;

            _distance = X_AxisDistance(this._targetPosition);
            return _distance <= 0.6f;
        }





        /// <summary>
        /// With this method, it will be determined which direction the character should walk!
        /// </summary>
        /// <returns>If the object is on the left side, this method will return `-1` value. If the object is on the right side, this method will return `+1` value!</returns>
        internal short TargetPositionNegativeOrPositiveMultiply()
        {
            // ~~ Variables ~~
            float _distance;
            if (this._targetPosition != null)
            {
                _distance = this.transform.position.x - this._targetPosition.position.x;
                if (_distance < 0)
                {
                    this.transform.localScale = new Vector2(+1, +1);
                    return +1;
                }
                else
                {
                    this.transform.localScale = new Vector2(-1, +1);
                    return -1;
                }
            }
            else
            {
                Debug.LogWarning($"<color=yellow>WARNING!</color> The `_targetPosition' element in this class is empty!", this.gameObject);
                return 0;
            }
        }
    }
}