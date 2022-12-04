using BaseTemplate.Behaviours;
using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed;
    [SerializeField] private float sidedSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sensibility;
    [SerializeField] GameObject playerHead;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rayIsGroundedDistance;
    [SerializeField] private float jumpDelay;
    [SerializeField] private float divideSpeedWhenJump;
    [SerializeField] private GameObject currentGameObjectGun;
    [SerializeField] private float shootRange;
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private LayerMask layerCanPlayerShoot;
    [SerializeField] private List<AudioClip> fireSounds;

    public bool canShoot = false;
   
    private PlayerInput playerInput;

    private float currentSpeedValue;
    private float currentSidedSpeedValue;
    private Vector2 currentMouseValue;
    private bool isWalking;
    private bool isSidedWalking;
    private bool isJumping;
    private bool canJump = true;

    private float baseSpeed;
    private float baseSidedSpeed;
    private float baseDrag;
    private float baseSensibility;

    private bool isGrounded;
    private bool isScoping;
    bool stopScope = false;
    private bool isShooting;
    private int currentAmmo;
    private float currentShootingForce;

    private bool isTireRateFinish = true;
    private bool isReloading;

    private GameObject crosshair;



    // Start is called before the first frame update
    public void Init()
    {

      /*  if (GameManager.Instance.gameState == GameState.START)
        {
            return;
        }*/
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Player.Walk.started += OnWalkPressed;
        playerInput.Player.Walk.canceled += OnWalkPressed;

        playerInput.Player.SidedWalk.started += OnSidedWalkPressed;
        playerInput.Player.SidedWalk.canceled += OnSidedWalkPressed;

        playerInput.Player.MouseValue.started += GetValueMouse;
        playerInput.Player.MouseValue.canceled += GetValueMouse;
        playerInput.Player.MouseValue.performed += GetValueMouse;

        playerInput.Player.Jump.started += OnJumpPressed;
        playerInput.Player.Jump.canceled += OnJumpPressed;

        playerInput.Player.Shoot.started += OnShootPressed;
        //playerInput.Player.Shoot.performed += OnShootPressed;
        //playerInput.Player.Shoot.canceled += OnShootPressed;

        playerInput.Player.Scope.started += OnScopePressed;
        playerInput.Player.Scope.canceled += OnScopePressed;

        baseSpeed = speed;
        baseSidedSpeed = sidedSpeed;
        baseDrag = rb.drag;
        baseSensibility = sensibility;

        crosshair = PlayerManager.Instance.playerCrosshair;

        currentAmmo = (int)PlayerData.Instance.PlayerWeapon._ammo;
        //UIManager.Instance.GameCanvas.RefreshAmmo(currentAmmo);

    }

    void FixedUpdate()
    {        
        transform.RotateAround(transform.position, new Vector3(0, 1, 0), currentMouseValue.x * sensibility * Time.fixedDeltaTime);
        playerHead.transform.Rotate(Vector3.left, currentMouseValue.y * (sensibility / 1.5f) * Time.fixedDeltaTime,Space.Self);

        if (isWalking)
        {
            rb.velocity += transform.forward * speed * Time.fixedDeltaTime * currentSpeedValue;
        }

        if (isSidedWalking)
        {
            rb.velocity += transform.right * sidedSpeed * Time.fixedDeltaTime * currentSidedSpeedValue;
        }


        bool hit = Physics.Raycast(transform.position, Vector3.down, rayIsGroundedDistance, groundLayerMask);
        isGrounded = hit;

            if (hit)
            {
                rb.drag = baseDrag;
                speed = baseSpeed;
                sidedSpeed = baseSidedSpeed;
            } else
            {
                rb.drag = 0;
                speed = baseSpeed / divideSpeedWhenJump;
                sidedSpeed = baseSidedSpeed / divideSpeedWhenJump;
            }
    }


    void OnWalkPressed(InputAction.CallbackContext context)
    {
        currentSpeedValue = context.ReadValue<float>() * 2;
        if (UIManager.Instance.GameCanvas.canRunning)
        {
            if (currentSpeedValue >= 1) currentSpeedValue = 1;
            if (currentSpeedValue <= -1) currentSpeedValue = -1;

            isWalking = currentSpeedValue != 0;
        }
    }

    void OnSidedWalkPressed(InputAction.CallbackContext context)
    {
        currentSidedSpeedValue = context.ReadValue<float>() * 2;
        if (UIManager.Instance.GameCanvas.canRunning)
        {
            if (currentSidedSpeedValue >= 1) currentSidedSpeedValue = 1;
            if (currentSidedSpeedValue <= -1) currentSidedSpeedValue = -1;

            isSidedWalking = currentSidedSpeedValue != 0;
        }
    }

    void OnJumpPressed(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValue<float>() > 0;
        if (isJumping && isGrounded && canJump && UIManager.Instance.GameCanvas.canRunning)
        {
            canJump = false;
            rb.AddForce(0, jumpForce * 1000, 0);
            StartCoroutine(waitBeforeJumpAutorizeToJump());
        }

    }
    void GetValueMouse(InputAction.CallbackContext context)
    {

        currentMouseValue = context.ReadValue<Vector2>();
        /*        print(transform.eulerAngles.x);
                playerHead.transform.Rotate(-currentMouseValue.y, 0, 0, Space.Self);
               playerHead.transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.normalized.x, -70, 70), transform.eulerAngles.y, transform.eulerAngles.z);
        */

        if (playerHead.transform.localRotation.x * 100 >= 70 &&
            currentMouseValue.y > 0)
        {
            currentMouseValue.y = 0f;
            if (playerHead.transform.localRotation.x * 100 >= 80) playerHead.transform.Rotate(Vector3.right, 69.9f);
        }

        if (playerHead.transform.localRotation.x * 100 <= -70 &&
            currentMouseValue.y < 0)
        {
            currentMouseValue.y = 0f;
            if (playerHead.transform.localRotation.x * 100 <= -80) playerHead.transform.Rotate(Vector3.right, -69.9f);
        }

    }

    void OnShootPressed(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValue<float>() > 0;
        print(isShooting);
        if (isShooting && isTireRateFinish && !isReloading && canShoot)
        {
            currentAmmo--;
            UIManager.Instance.GameCanvas.RefreshAmmo(currentAmmo);
            AudioManager.Instance.PlayClipAt(fireSounds[Random.Range(0, fireSounds.Count)], transform.position);
/*            if (currentShootingForce > 0) currentShootingForce += 1.5f;
            else currentShootingForce = PlayerData.Instance.PlayerWeapon.WeaponRecoil;

            if (playerHead.transform.rotation.x > 0) currentShootingForce = -currentShootingForce;

            playerHead.transform.DOLocalRotate(Vector3.right * (playerHead.transform.rotation.x - currentShootingForce),
                                                        PlayerData.Instance.PlayerWeapon.weaponRecoilDuration * 0.4f)
                  .SetEase(Ease.OutQuart)
                  .OnComplete(() =>
                  {
                      playerHead.transform.DOLocalRotate(Vector3.right * (playerHead.transform.rotation.x + currentShootingForce), 
                                                    PlayerData.Instance.PlayerWeapon.weaponRecoilDuration * 0.6f)
                      .SetEase(Ease.OutSine)
                      .OnComplete(() => currentShootingForce = 0);
                  });
*/
            playerHead.transform.DOShakeRotation(1f, 1,10, 0,true,ShakeRandomnessMode.Full );

            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, shootRange, layerCanPlayerShoot))
            {
                hit.rigidbody.AddForceAtPosition(fpsCamera.transform.forward * PlayerData.Instance.PlayerWeapon.bulletForce, hit.point);
                TargetManager.Instance.TargetHit(hit.collider.gameObject);
            }
            if (currentAmmo <= 0) StartCoroutine(Reload());
            else StartCoroutine(WaitBeforeShoot());
        }
    }
    
    IEnumerator WaitBeforeShoot()
    {
        isTireRateFinish = false;
        yield return new WaitForSeconds(PlayerData.Instance.PlayerWeapon._fireRate);
        isTireRateFinish = true;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(PlayerData.Instance.PlayerWeapon._reloadTime);
        currentAmmo = (int)PlayerData.Instance.PlayerWeapon._ammo;
        UIManager.Instance.GameCanvas.RefreshAmmo(currentAmmo);
        StartCoroutine(WaitBeforeShoot());
        isReloading = false;
    }

    void OnScopePressed(InputAction.CallbackContext context)
    {
        isScoping = context.ReadValue<float>() > 0;
        if (isScoping && !stopScope)
        {
            stopScope = true;
            currentGameObjectGun.transform.DOLocalMoveX(0, 0.5f).OnComplete(() =>
            {
                crosshair.SetActive(false);
                speed /= 2f;
                sidedSpeed /= 2f;
                sensibility /= 1.5f;
            });/*.OnKill(() =>
            {
                crosshair.SetActive(true);
                speed = baseSpeed;
                sensibility = baseSensibility;

            });*/
        } else
        {
            currentGameObjectGun.transform.DOKill();
            crosshair.SetActive(true);
            speed = baseSpeed;
            sidedSpeed = baseSidedSpeed;
            sensibility = baseSensibility;
            currentGameObjectGun.transform.DOLocalMoveX(0.3f, 0.5f).OnComplete(() => {
                stopScope = false;
            });

        }
    } 

    IEnumerator waitBeforeJumpAutorizeToJump()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    public void ActivatePlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb.constraints = (int) RigidbodyConstraints.FreezeAll -
                         RigidbodyConstraints.FreezePositionX -
                         RigidbodyConstraints.FreezePositionZ -
                         RigidbodyConstraints.FreezePositionY;
    }
}
