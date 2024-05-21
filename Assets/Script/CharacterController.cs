using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    private CharacterController() { }

    #region PlayerState
    [HideInInspector] [SerializeField] private FloatingJoystick joystick;
    public Rigidbody rb;
    private Vector3 moveVector;
    [HeaderAttribute("--------Player Setting--------")]
    public float moveSpeed;
    private float _moveSpeed;
    public float rotateSpeed;
    private float _rotateSpeed;
    [SpaceAttribute]
    public float maxHP;
    public float currentHP;
    public float maxMP;
    public float currentMP;
    private AnimatorControlle animatorControlle;
    [HeaderAttribute("--------UI Setting--------")]
    public Button skill1;
    public Button skill2;
    public Button skill3;
    [SerializeField] private MPBarController _MPBarController;
    [SerializeField] private VFXManager _VFXManager;
    #endregion

    #region Hurt_UI
    [SerializeField] private HealthBarController healthBarController;
    [SerializeField] private Animator state_UI_anim;
    #endregion

    private CombatSystem combatSystem;

    private Vector3 startMouseDown;
    private Vector3 lastMouseDown;
    private float pressTimer;
    private bool isCounter;             //开始计数
    private bool isPress;
    private bool isDrag;                //开始拖动
    private bool isLasting;             //开始持久点击

    [HeaderAttribute("--------Press Setting--------")]
    public float pressTime;             //单击
    public float pressLastingTime;      //持久点击
    public float dragDistance;          //拖动大于多少才开始生效

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 currentPosition;
    private bool stopTouch = false;
    private bool isRoll = false;
    private bool isRight = false;

    public GameObject skill_1;
    #region 事件
    public static Action<Vector3> StartPressEvent;
    public static Action<Vector3> EndPressEvent;

    public static Action<Vector3> StartDragEvent;
    public static Action<Vector3> EndDragEvent;

    public static Action<Vector3> StartLastingEvent;
    public static Action<Vector3> EndLastingEvent;
    #endregion

    #region 测试方法
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animatorControlle = GetComponent<AnimatorControlle>();
        combatSystem = GetComponent<CombatSystem>();

        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;

        currentHP = maxHP;
        healthBarController.hp_slider.maxValue = maxHP;
        healthBarController.hp_slider.value = currentHP;

        _MPBarController.mp_slider.maxValue = maxMP;
        _MPBarController.mp_slider.value = currentMP;

        StartPressEvent += StartPress;
        EndPressEvent += EndPress;

        StartDragEvent += StartDrag;
        EndDragEvent += EndDrag;

        StartLastingEvent += StartLasting;
        EndLastingEvent += EndLasting;
    }

    private void StartPress(Vector3 v)
    {
        isPress = true;
        combatSystem.OnAttack(isPress);
        combatSystem.OnCombo(isPress);
    }

    private void EndPress(Vector3 v)
    {
    }

    private void StartDrag(Vector3 v)
    {
        //Debug.Log("开始拖动事件");
        //Move();
    }

    private void EndDrag(Vector3 v)
    {
        //Debug.Log("结束拖动事件");
        //animatorControlle.PlayIdle();
    }

    private void StartLasting(Vector3 v)
    {
        Debug.Log("开始持续点击事件");
    }

    private void EndLasting(Vector3 v)
    {
        Debug.Log("结束持续点击事件");
    }
    #endregion

    #region Event
    void Move()
    {   
        if(combatSystem.attack == false)
        {
            moveVector = Vector3.zero;
            moveVector.x = joystick.Horizontal * moveSpeed * Time.deltaTime;
            moveVector.z = joystick.Vertical * moveSpeed * Time.deltaTime;

            if(joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);

                animatorControlle.PlayRun();
            }
            else if(joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                animatorControlle.PlayIdle();
            }

            rb.MovePosition(rb.position + moveVector);
        }
    }
    #endregion

    void Update()
    {
        MP_Check();

        if (isRoll && isRight)
        {
            rb.transform.position += Vector3.right * 0.02f;
        }
        else if (isRoll && !isRight)
        {
            rb.transform.position += Vector3.left * 0.02f;
        }

        if (pressTimer > 0.1f)
        {
            Move();
        }
        else if (pressTimer < 0.05f)
        {
            if (SwipeManager.swipeRight)
            {
                isRoll = true;
                isRight = true;
                transform.rotation = Quaternion.Euler(0, 90, 0);
                animatorControlle.PlayRoll();
            }
            if (SwipeManager.swipeLeft)
            {
                isRoll = true;
                isRight = false;
                transform.rotation = Quaternion.Euler(0, -90, 0);
                animatorControlle.PlayRoll();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            isCounter = true;
            startMouseDown = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastMouseDown = Input.mousePosition;
            isCounter = false;

            if (isDrag)
            {
                if (EndDragEvent != null) EndDragEvent(Input.mousePosition);
                isDrag = false;
            }
            else
            {
                if (EndPressEvent != null) EndPressEvent(Input.mousePosition);
            }
        }

        if (isCounter)
        {
            pressTimer += Time.deltaTime;
        }
        else
        {
            if (pressTimer > 0 && pressTimer < pressTime)
            {
                if (StartPressEvent != null) StartPressEvent(Input.mousePosition);

            }

            pressTimer = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy_HitBox")
        {
            state_UI_anim.SetBool("GetHurt", true);
            animatorControlle.PlayHurt();
            moveSpeed = 0f;
        }
    }

    public void ResetSpeedAndCombo()
    {
        moveSpeed = _moveSpeed;
        rotateSpeed = _rotateSpeed;
        combatSystem.attack = false;
        combatSystem.count_combo = 0;
    }

    public void SpwanSkill_1()
    {
        GameObject skill_obj = Instantiate(skill_1, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), this.transform.rotation, this.transform);
        skill_obj.GetComponent<Rigidbody>().AddForce(skill_obj.transform.forward * 1000f);
    }
    
    public void SwipeReset()
    {
        isRoll = false;
        isRight = false;
    }

    void MP_Check()
    {
        if (currentMP >= 10)
        {
            skill1.interactable = true;
            _VFXManager.skill1_VFX.Play();
        }
        else
        {
            skill1.interactable = false;
            _VFXManager.skill1_VFX.Stop();
        }

        if (currentMP >= 30)
        {
            skill2.interactable = true;
            _VFXManager.skill2_VFX.Play();
        }
        else
        {
            skill2.interactable = false;
            _VFXManager.skill2_VFX.Stop();
        }

        if (currentMP >= 100)
        {
            skill3.interactable = true;
            _VFXManager.skill3_VFX.Play();
        }
        else
        {
            skill3.interactable = false;
            _VFXManager.skill3_VFX.Stop();
        }
    }
}