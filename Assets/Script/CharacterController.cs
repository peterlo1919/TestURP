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
    private Rigidbody rb;
    private Vector3 moveVector;
    [HeaderAttribute("Player Setting")]
    public float moveSpeed;
    public float rotateSpeed;
    [SpaceAttribute]
    public float maxHP;
    public float currentHP;
    private AnimatorControlle animatorControlle;
    #endregion

    #region Hurt_UI
    [HeaderAttribute("UI Setting")]
    [SerializeField] private Slider hp_slider;
    [SerializeField] private Text hp_Text;
    [SerializeField] private Image hp_Fill;
    [SerializeField] private Color less_50percent;
    [SerializeField] private Color less_30percent;
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

    [HeaderAttribute("Press Setting")]
    public float pressTime;             //单击
    public float pressLastingTime;      //持久点击
    public float dragDistance;          //拖动大于多少才开始生效

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
        hp_slider.maxValue = maxHP;
        currentHP = maxHP;
        hp_slider.value = currentHP;

        rb = GetComponent<Rigidbody>();
        animatorControlle = GetComponent<AnimatorControlle>();
        combatSystem = GetComponent<CombatSystem>();

        StartPressEvent += StartPress;
        EndPressEvent += EndPress;

        StartDragEvent += StartDrag;
        EndDragEvent += EndDrag;

        StartLastingEvent += StartLasting;
        EndLastingEvent += EndLasting;
    }

    private void StartPress(Vector3 v)
    {
        Debug.Log("开始单击事件");
        isPress = true;
        combatSystem.OnCombo(isPress);
        combatSystem.OnAttack(isPress);
    }

    private void EndPress(Vector3 v)
    {
        Debug.Log("结束单击事件");
    }

    private void StartDrag(Vector3 v)
    {
        Debug.Log("开始拖动事件");
        Move();
    }

    private void EndDrag(Vector3 v)
    {
        Debug.Log("结束拖动事件");
        animatorControlle.PlayIdle();
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

    void HP_UI()
    {
        hp_Text.text = hp_slider.value.ToString();
        hp_slider.value = currentHP;

        if(currentHP <= maxHP * 0.3f)
        {
            hp_Fill.color = less_30percent;
        }
        else if(currentHP <= maxHP * 0.5f)
        {
            hp_Fill.color = less_50percent;
        }
    }
    #endregion

    void Update()
    {
        HP_UI();

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
                //拖动
                if (EndDragEvent != null) EndDragEvent(Input.mousePosition);
                isDrag = false;
            }
            else
            {
                //单击
                if (EndPressEvent != null) EndPressEvent(Input.mousePosition);
            }
            /*else if (isLasting)
            {
                //持久点击
                if (EndLastingEvent != null) EndLastingEvent(Input.mousePosition);
                isLasting = false;
            */

        }

        if (isCounter)
        {
            //开始计数
            pressTimer += Time.deltaTime;
        }
        else
        {
            if (pressTimer > 0 && pressTimer < pressTime)
            {
                Debug.Log("单击");
                if (StartPressEvent != null) StartPressEvent(Input.mousePosition);

            }

            pressTimer = 0f;
        }

        if (isCounter && Mathf.Abs(Vector3.Distance(startMouseDown, Input.mousePosition)) > dragDistance && isLasting == false)
        {
            Debug.Log("正在拖动");
            isDrag = true;

            if (StartDragEvent != null) StartDragEvent(Input.mousePosition);

            //让人物跟谁手指的方向移动
            return;
        }

        /*if (isCounter && pressTimer > pressLastingTime && isDrag == false)
        {
            Debug.Log("持久点击");
            isLasting = true;

            if (StartLastingEvent != null) StartLastingEvent(Input.mousePosition);

            //出现技能图标,然后滑动到技能哪里就可以触发技能

            return;
        }*/


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy_HitBox")
        {
            state_UI_anim.SetBool("GetHurt", true);
            currentHP = currentHP - 100;
        }
    }

}