using System;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// プレイヤーの移動とアニメーション
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMove : MonoBehaviour
    {
        //Animatorの値
        private static int _hashSpeed = Animator.StringToHash("Speed");
        private static int _hashFallSpeed = Animator.StringToHash ("FallSpeed");
        private static int _hashGroundDistance = Animator.StringToHash("GroundDistance");
        private static int _hashDamage = Animator.StringToHash ("Damage");
        private static int _hashAttack1 = Animator.StringToHash ("Attack1");
        
        //必要なコンポーネント
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        //移動に関係する当たり判定
        [SerializeField] private CircleCollider2D verticalOffset, horizontalOffset;
        [SerializeField] private LayerMask groundMask;
        
        //ダメージ用
        private bool _isDamaged = false;
        private Vector2 _damageWay;
        
        //移動の緩急用カーブ
        [SerializeField] private AnimationCurve gravityCurve;
        [SerializeField] private AnimationCurve jumpCurve;

        //ジャンプについての変数
        private bool _isGround = true;
        private bool _isJump = false;
        private float _fallTime = 0;
        private float _jumpTime = 0;
        [SerializeField] private float jumpLargeTime = 0.4f;
        [SerializeField] private float jumpSmallTime = 0.25f;
        [SerializeField] private float jumpSmallKeyTime = 0.015f; //小ジャンプの猶予時間
        private float _jumpEndTime = 0f;

        //走りについての変数
        [SerializeField] private AnimationCurve runCurve;
        private float _runTime = 0;

        //移動に使用するベクトルの要素
        private float _nextVecX = 0, _nextVexY = 0;
        
        //効果音
        private PlayerAudioSE _playerAudioSe;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerAudioSe = this.transform.GetComponentInChildren<PlayerAudioSE>();
        }

        private void Update()
        {
            if(Time.timeScale < 1) return;
            if(_isDamaged) return;
            
            var myPos = this.transform.position;
            var myPos2D = new Vector2(myPos.x, myPos.y);
            _nextVecX = 0;
            _nextVexY = 0;
            //縦移動のベクトル計算
            //入力でジャンプ開始
            if (Input.GetButtonDown("Jump") && _isGround)
            {
                _isJump = true;
                _jumpTime = 0;
                _playerAudioSe.JumpSe();
                //デフォルトで大きいジャンプ
                _jumpEndTime = jumpLargeTime;
            }
            //ジャンプの上昇部分
            if (_isJump)
            {
                _jumpTime += Time.deltaTime;
                //短押しで小ジャンプに切替
                if (Input.GetButtonUp("Jump") && _jumpTime < jumpSmallKeyTime)
                {
                    _jumpEndTime = jumpSmallTime;
                }
                //滞空時間を超えたら落下開始
                if (_jumpTime >= _jumpEndTime)
                {
                    _isJump = false;
                }
                _nextVexY = JumpValue();
            }
            //ジャンプ上昇時時以外、空中では落下
            else if (!_isGround)
            {
                _fallTime += Time.deltaTime;
                _nextVexY = GravityValue();
            }

            //横移動のベクトル計算
            var inputAxis = Input.GetAxisRaw ("Horizontal");
            if (inputAxis != 0)
            {
                if(_runTime == 0 && _isGround == true) _playerAudioSe.RunSe();
                _runTime += Time.deltaTime;
                _nextVecX = RunValue(inputAxis);
            }
            else
            {
                _runTime = 0;
                _nextVecX = 0;
            }

            //ここから衝突判定
            var nextVec = new Vector2(_nextVecX, _nextVexY);
            var nextPos = myPos2D + nextVec;
            
            //TODO:ここから少し雑なので直したい
            //接地判定
            const float verticalBoxSize = 0.08f;
            var distanceGround = GoBoxCast(myPos2D, verticalBoxSize, Vector2.down);
            var footDistance = verticalOffset.radius - verticalBoxSize / 2;

            //接地と滞空が切り替わったときはジャンプをリセット
            var isNewGround = distanceGround.distance < footDistance;
            if (_isGround != isNewGround) _fallTime = 0;
            if (_isGround == false && isNewGround == true) _playerAudioSe.LandingSe();
            _isGround = isNewGround;
            
            //接地しているときの調整
            var diffFoot = (distanceGround.point + Vector2.up * footDistance) - myPos2D;
            if (_isGround && !_isJump)
            {
                _nextVexY = diffFoot.y;
            }
            
            //天井に引っかかったときジャンプを終了します
            var distanceCeiling = GoBoxCast(myPos2D, verticalBoxSize, Vector2.up);
            var headDistance = verticalOffset.radius - verticalBoxSize / 2;
            if (distanceCeiling.distance < headDistance)
            {
                _isJump = false;
            }
            
            //左右の壁との衝突判定
            //左側に行こうとするときの壁
            if (inputAxis < 0)
            {
                HorizontalLimit(myPos2D, nextPos, Vector2.left);
            }
            //右側に行こうとするときの壁
            if (inputAxis > 0)
            {
                HorizontalLimit(myPos2D, nextPos, Vector2.right);
            }

            //攻撃モーション中は左右の移動を減衰させる
            if (AnimState(_animator,"Attack1") || 
                AnimState(_animator,"Attack2") || 
                AnimState(_animator,"SpecialAttack"))
            {
                _nextVecX /= 2;
            }
            else if (inputAxis > 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (inputAxis < 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }

            //最終的な移動ベクトルを計算
            nextVec = new Vector2(_nextVecX, _nextVexY);
            //プレイヤー位置の決定
            this.transform.Translate(nextVec);
            
            //アニメーションの設定
            _animator.SetFloat (_hashFallSpeed, _nextVexY);
            _animator.SetFloat(_hashSpeed,Mathf.Abs(inputAxis));
            //0.04は魔法の数字。groundBoxSize/2と数字が一致しているからここが悪さしてる
            //あとで関数に分けるときに考える
            _animator.SetFloat (_hashGroundDistance, distanceGround.distance - footDistance + 0.04f);
            
            //攻撃モーション
            if (Input.GetButtonDown("Fire1"))
            {
                _animator.SetTrigger(_hashAttack1);
            }
        }

        private void LateUpdate()
        {
            if(!_isDamaged) return;
            this.transform.position += new Vector3(_damageWay.x, _damageWay.y, 0) * Time.deltaTime;
        }

        private bool AnimState(Animator animator, string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        //左右の壁との適切な距離を返す
        private void HorizontalLimit(Vector2 nowVec, Vector2 nextPos, Vector2 moveDirection)
        {
            const float sideBoxSize = 0.1f;
            var cast = GoBoxCast(nextPos, sideBoxSize, moveDirection);
            var sideDistance = horizontalOffset.radius - sideBoxSize / 2;

            //移動後の位置が壁にめり込んでいるか
            var isSideOver = cast.distance < sideDistance;
            //壁との適性距離と現在位置の差
            var diffSide = (cast.point + -moveDirection * sideDistance) - nowVec;
            //めり込んでいる場合、壁との適性距離に調整する
            if (isSideOver)
            {
                _nextVecX = diffSide.x;
            }
        }

        //上下左右の接地判定を取るために使う
        RaycastHit2D GoBoxCast(Vector2 pos,float boxSize,Vector2 direction)
        {
            var hit =   
                Physics2D.BoxCast(pos, Vector2.one * boxSize ,
                    0, direction,Mathf.Infinity ,groundMask);
            return hit;
        }
        //横方向成分
        float RunValue(float ax)
        {
            return ax * runCurve.Evaluate(_runTime) * Time.deltaTime;
        }
        //上方向成分
        float JumpValue()
        {
            return jumpCurve.Evaluate(_jumpTime) * Time.deltaTime;
        }
        //下方向成分
        float GravityValue()
        {
            return -1 * gravityCurve.Evaluate(_fallTime) * Time.deltaTime;
        }
        
        //ダメージ処理
        public void DamagedMove(Vector2 moveVector)
        {
            _isDamaged = true;
            _damageWay = moveVector;
            _playerAudioSe.DamageSe();
            _animator.SetTrigger(_hashDamage);
        }

        public void CompleteRespawn()
        {
            _isDamaged = false;
        }
    }
}
