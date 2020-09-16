using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool hasPowerUp;//триггер, показывающий, есть ли у игрока суперсила или нет
    public bool hasCheatPowerUp;//триггер, показывающий подобрал ли игрок ложную суперсилу
    public float speed=5.0f; // скорость движения игрока 
    public bool gameOver=false;//триггер "конца игры"
    private float PowerStrength = 15.0f;//супер сила, которую даруют бриллианты 
  
    //блок триггеров, отвечающих за начало этапов 
    public bool TriggerStartGame = false;//триггер, которая переключается, если игра началась
    public bool TriggerStartTraining = false;//триггер, который переключается, если тренировка началась
    public bool TriggerTrainingFirstStage = false;//триггер, который переключается при первом этапе тренировки
    public bool TriggerTrainingSecondStage = false;//триггер, который переключается при втором этапе тренировки
    public bool TriggerTrainingThirdStage = false;//триггер, который переключается при третьем этапе тренировки
   // public bool 
  

    //блок триггеро, который отвечают за появление сообщений 
    public bool TriggerMessageTraining = true;//триггер, который переключается, если появляется сообщение во время тренировки (касается всех сообщений)
    private bool TriggerMessageTrainingFirst = false;//триггер, который переключается, если появляется первое сообщение во время тренировки
    private bool TriggerMessageTrainingSecond = false;////триггер, который переключается, если появляется второе сообщение во время тренировки
    private bool TriggerMessageTrainingThird = false;////триггер, который переключается, если появляется третье сообщение во время тренировки
    private bool TriggerMessageTrainingFourth = false; //триггер, который переключается, если появляется третье сообщение во время тренировки
    private bool TriggerMessageBegin = true;//триггер появления самого первого сообщения (в начале игры)

    public bool TriggerCommandFromPlayerDestroyEnemy=false;//команда на уничтожение врагов 
    public int NumberContact;
    // public int GetContacts(ContactPoint[] contacts);

    //блок триггеров, отвечающие за этапы в игре
    public bool TriggerGameFirstStage = false;//триггер начала первого этапа игры
    public bool TriggerGameSecondStage = false;//триггер начала второго этапа игры
    public bool TriggerGameThirdStage = false;//триггер начала третьего этапа игры
    public bool TriggerGameFourthStage = false;//триггер начала четвёртого этапа игры
    public bool TriggerGameFifthStage = false;//триггер начала пятого этапа игры

    public bool TriggerMouseDouble = false;//триггер повторного нажатия ЛЕВОЙ кнопки мыши
   // private bool TriggerMouseDoubleRight = false;//триггер повторного нажатия ПРАВОЙ кнопки мыши

   // public float Rotate;
    public int RandomforwardInput;
    public int RandomhorizontalInput;
    public bool TimePauseStartStage=false;
    public bool TriggerWin = false;

    public GameObject CheatPowerUpIndicator;//индикатор ложной суперсилы
    public GameObject powerUpIndicator;//индикатор, свидетельствующий того, что игрок получил суперсилу
    public GameObject MessageTraining;
    public GameObject MessageTrainingFirst;
    public GameObject MessageTrainingSecond;
    public GameObject MessageTrainingThird;
    public GameObject MessageTrainingFourth;
    public GameObject MessageGameFirst;
    public GameObject MessageEnemyAmbush;
    public GameObject MessageGameOver;
    public GameObject MessageGameSecond;
    public GameObject MessageGameThird;
    public GameObject WithoutControl;
    public GameObject MessageGameFourth;
    public GameObject MessageGameFifth;
    public GameObject MessageTimer;
    public GameObject MessageGameWinText;
    public GameObject FinalText;
    public GameObject PowerIcon;
    public GameObject PowerIcon2;


    private Rigidbody playerRb;
    private GameObject focalPoint;//Точка, вокруг которой вращается камера
    private SpawnManager SpawnManagerScripts;
    private FX_MIST FX_MISTScript;
    
   // public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        SpawnManagerScripts = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        FX_MISTScript= GameObject.FindWithTag("Cloud").GetComponent<FX_MIST>();
        InvokeRepeating("MoveRandom", 1, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
       

       //второй этап игры
        if (SpawnManagerScripts.CountGameStage == 2 && !TriggerGameSecondStage &&!gameOver)
        {
            TriggerGameSecondStage = true;
            TriggerGameFirstStage = false;
            MessageGameFirst.gameObject.SetActive(false);
            MessageGameSecond.gameObject.SetActive(true);
            PlayerAppear(0);//в дальнейшем это надо изменить
            TimePauseStartStage = true;
            StartCoroutine(FunctionTimePauseStartStage());
        }

        //третий этап игры
        if (SpawnManagerScripts.CountGameStage == 3 && !TriggerGameThirdStage && !gameOver)
        {
            TriggerGameThirdStage = true;
            TriggerGameSecondStage = false;
            TriggerGameFirstStage = false;
            MessageGameFirst.gameObject.SetActive(false);
            MessageGameSecond.gameObject.SetActive(false);
            MessageGameThird.gameObject.SetActive(true);
            PlayerAppear(0);
            TimePauseStartStage = true;
            StartCoroutine(FunctionTimePauseStartStage());
        }

        //четвёртый этап игры
        if (SpawnManagerScripts.CountGameStage == 4 && !TriggerGameFourthStage && !gameOver)
        {
            CheatPowerUpIndicator.gameObject.SetActive(false);//и индикатор исчезнет
            WithoutControl.gameObject.SetActive(false);//выключаем сообщение о потере управления
            hasCheatPowerUp = false;
            TriggerGameFourthStage = true;
            TriggerGameThirdStage = false;
            TriggerGameSecondStage = false;
            TriggerGameFirstStage = false;
            MessageGameFirst.gameObject.SetActive(false);
            MessageGameSecond.gameObject.SetActive(false);
            MessageGameThird.gameObject.SetActive(false);
            MessageGameFourth.gameObject.SetActive(true);
            PlayerAppear(0);
            TimePauseStartStage = true;
            StartCoroutine(FunctionTimePauseStartStage());
        }
        //пятый этап игры
        if (SpawnManagerScripts.CountGameStage == 5 && !TriggerGameFifthStage && !gameOver &&!TriggerWin)
        {
            CheatPowerUpIndicator.gameObject.SetActive(false);//и индикатор исчезнет
            WithoutControl.gameObject.SetActive(false);//выключаем сообщение о потере управления
            hasCheatPowerUp = false;
            TriggerGameFourthStage = false;
            TriggerGameThirdStage = false;
            TriggerGameSecondStage = false;
            TriggerGameFirstStage = false;
            MessageGameFirst.gameObject.SetActive(false);
            MessageGameSecond.gameObject.SetActive(false);
            MessageGameThird.gameObject.SetActive(false);
            MessageGameFourth.gameObject.SetActive(false);
            PlayerAppear(0);
            MessageGameFifth.gameObject.SetActive(true);
            TriggerGameFifthStage = true;
            MessageTimer.gameObject.SetActive(true);
            

        }

     
        //конец игры
        if (TriggerWin)
        {
            MessageGameFifth.gameObject.SetActive(false);
            MessageTimer.gameObject.SetActive(false);
            TriggerGameFifthStage = false;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            MessageGameWinText.gameObject.SetActive(true);
            if (FX_MISTScript.transform.position.y > 5)
            {
                PlayerAppear(-100);
                FinalText.gameObject.SetActive(true);
                PowerIcon.gameObject.SetActive(true);
                PowerIcon2.gameObject.SetActive(true);
                MessageGameWinText.gameObject.SetActive(false);
            }

        }




        if (Input.GetKeyDown(KeyCode.Mouse0))
            TriggerMouseDouble = true; //если нажата левая кнопка мыши, то триггер это фиксирует
        
        //начало игры
        FunctionMessageBegin();
        FunctionInputMouse1AndStartGame();
        
        //тренировка
        FunctionTrainingStartFirstStage();
        FunctionTrainingEndFirstStage();
        FunctionTrainingStartSecondStage();
        FunctionTrainingEndSecondStage();
        FunctionTrainingStartThirdStage();
        FunctionTrainingEndThirdStage();

        //готовимся к началу игры (первый этап)
        if (TriggerMouseDouble && !TriggerStartGame && TriggerMessageTrainingFourth)//повторяем всё, как для первого сообщения
        {
            TriggerMessageTrainingFourth = false;
            TriggerMessageTraining = false;
            MessageTrainingFourth.gameObject.SetActive(false);
            TriggerStartGame = true;
            TriggerMouseDouble = false;
            if (SpawnManagerScripts.CountGameStage == 1)//только если у нас первый этап, тогда переходим к дальнейшей активации
            {
                TriggerGameFirstStage = true;//триггер начала первого этапа игры
                MessageGameFirst.gameObject.SetActive(true);
            }
            TimePauseStartStage = true;
            StartCoroutine(FunctionTimePauseStartStage());
        }
        
        
        
        //проработка условий проигрыша
        
        //если игрок застрял между шарами
        if (playerRb.velocity.x < 0.001 && playerRb.velocity.z < 0.001 && SpawnManagerScripts.TriggerEnemyAmbush)//если игрок окружен и почти не двигается
        {
            SpawnManagerScripts.TriggerEnemyAmbush = false;
            gameOver = true;
            MessageGameFirst.gameObject.SetActive(false);
            MessageGameSecond.gameObject.SetActive(false);
            MessageEnemyAmbush.gameObject.SetActive(true);
            TriggerMessageTraining = true;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            TriggerMouseDouble = false;
            SpawnManagerScripts.messageGameHelpFromGoodForce.gameObject.SetActive(false);
        }


        //если игрок улетел
        if (gameOver && transform.position.y < -10 && !TriggerMessageTraining)
        {
            TriggerMessageTraining = true;
            MessageGameOver.gameObject.SetActive(true);
            TriggerMouseDouble = false;
            SpawnManagerScripts.messageGameHelpFromGoodForce.gameObject.SetActive(false);
        }

        if (TriggerMouseDouble && gameOver && TriggerMessageTraining)
        {
           
            TriggerMessageTraining = false;
            TriggerCommandFromPlayerDestroyEnemy = true;
            MessageGameOver.gameObject.SetActive(false);
            MessageEnemyAmbush.gameObject.SetActive(false);
            PlayerAppear(0);
            TriggerMouseDouble = false;
            SpawnManagerScripts.TriggerAppearPowerUpFalse = false;
           
            hasPowerUp = false;//после того, как время выйдет, игрок уже не будет иметь суперсилу
            powerUpIndicator.gameObject.SetActive(false);//и индикатор исчезнет
            hasCheatPowerUp = false;//после того, как время выйдет, игрок уже не будет иметь ложную суперсилу
            CheatPowerUpIndicator.gameObject.SetActive(false);//и индикатор исчезнет
            WithoutControl.gameObject.SetActive(false);//выключаем сообщение о потере управления
           
        }



        PlayerMove();
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);//устанавливаем позицию индикатора суперсилы (она должна быть чуть ниже, чтобы находиться в плоскости земли) 
        CheatPowerUpIndicator.transform.position= transform.position + new Vector3(0, 1, 0);//устанавливаем позицию индикатора ложной суперсилы
        FunctionGameOver();//функция "конец игры"


    }
    IEnumerator PowerupCountdownRoutine() //функция времени обратного отсчёта (когда игрок получает суперсилы)
    {
        yield return new WaitForSeconds(4);//собственно, сам счётчик, запрограммированный на 7 сек
        hasPowerUp = false;//после того, как время выйдет, игрок уже не будет иметь суперсилу
        powerUpIndicator.gameObject.SetActive(false);//и индикатор исчезнет
    }

    IEnumerator CheatPowerupCountdownRoutine() //функция времени обратного отсчёта (когда игрок получает ложную суперсилы)
    {
        yield return new WaitForSeconds(7);//собственно, сам счётчик, запрограммированный на 7 сек
        hasCheatPowerUp = false;//после того, как время выйдет, игрок уже не будет иметь ложную суперсилу
        CheatPowerUpIndicator.gameObject.SetActive(false);//и индикатор исчезнет
        WithoutControl.gameObject.SetActive(false);//выключаем сообщение о потере управления
    }

    IEnumerator FunctionTimePauseStartStage() //функция временного отсутствия движения врагов (чтобы у игрока было время адаптироваться)
    {
        yield return new WaitForSeconds(2);//собственно, сам счётчик, запрограммированный на 2 сек
        TimePauseStartStage = false;


    }

    public void PlayerAppear (int PlayerPosStart)//функция появления игрока

    {
        transform.position = new Vector3(0, 0, PlayerPosStart);
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
    
    
    
    private void PlayerMove ()
    {
        if (!TriggerMessageTraining && !TriggerTrainingSecondStage && !hasCheatPowerUp && !TriggerWin)//движение игрока работает только тогда, когда у нас  НЕТ сообщений во время тренировки и это не второй этап тренировки и не победа
        {
            float forwardInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

            float horizontalInput = Input.GetAxis("Horizontal");
            playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);

            if (TriggerGameFifthStage)
            {
                playerRb.AddForce(focalPoint.transform.forward * speed/2 * RandomforwardInput);
                playerRb.AddForce(focalPoint.transform.right * speed/2 * RandomhorizontalInput);
            }

                 //Rotate = forwardInput;
            }
        
    }

    private void MoveRandom()
    {
        RandomforwardInput = Random.RandomRange(-1, 2);
        RandomhorizontalInput= Random.RandomRange(-1, 2);
       // RandomforwardInput = 0;
        //RandomhorizontalInput = 1;
    }



    private void FunctionGameOver()//функция "конец игры"
    {
        if (transform.position.y < 0 && !TriggerTrainingThirdStage)//как только шар начинает падать вниз
            gameOver = true;//переключаем триггер,вызываем "конец игры"
       

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))//если игрок подобрал силу
        {
            if (TriggerGameFirstStage || TriggerGameSecondStage)//если первый этап игры или второй, то это наша суперсила
            {
                hasPowerUp = true;//триггер силы переключаем в режим "действующий"
                StartCoroutine(PowerupCountdownRoutine());//вызываем функцию счёта
                powerUpIndicator.gameObject.SetActive(true);//индикатор суперсилы включаем
                SpawnManagerScripts.messageGameHelpFromGoodForce.gameObject.SetActive(false);//отключаем сообщение о помощи
            }

            if (TriggerGameThirdStage)//если это третий этап и есть только ложная суперсила

            {
                hasCheatPowerUp = true;//триггер ложной силы переключаем в режим "действующий"
                StartCoroutine(CheatPowerupCountdownRoutine());
                CheatPowerUpIndicator.gameObject.SetActive(true);
                WithoutControl.gameObject.SetActive(true);//включаем сообщение о потере управления

            }
            Destroy(other.gameObject);//сам бриллиант удаляем
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);//при столкновении враги отлетают в противоположные стороны
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)//если игрок сталкнулся с врагом и имеет суперсилу
        {
            Debug.Log("Player collided with" + collision.gameObject + "with powerup set to" + hasPowerUp);
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            enemyRigidbody.AddForce(awayFromPlayer * PowerStrength, ForceMode.Impulse);//добавляем супер силу при столкновении
            NumberContact = collision.contactCount;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("EnemyBoss"))
            playerRb.AddForce((-awayFromPlayer) * PowerStrength, ForceMode.Impulse);//при столкновении с боссом игрок очень далеко отлетает

        if (collision.gameObject.CompareTag("Enemy"))
         NumberContact++;

    }

    private void FunctionMessageBegin ()//функция появления самого первого сообщения в игре
    {
        if (TriggerMouseDouble && !TriggerStartGame && TriggerMessageBegin)//если игрок нажал левую кнопку мыши и игра ещё не началась, но сообщение первое есть
        {
            TriggerMessageBegin = false;//триггер сообщение переключался (больше оно нам не нужно)
            MessageTraining.gameObject.SetActive(false);//не нужное сообщение выключаем
            TriggerMessageTrainingFirst = true;//даём разрешение на появление второго сообщения (или первого в тренировке)
            MessageTrainingFirst.gameObject.SetActive(true);//запускаем второе сообщение тренировки
            TriggerMouseDouble = false;//переключаем триггер нажатия мышки
        }

    }

    private void FunctionInputMouse1AndStartGame()//функция быстрого начала игры (без тренировки)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !TriggerStartGame && TriggerMessageBegin)//если нажата правая кнопки мыши, приступаем к последнему этапу
        {
            TriggerMessageBegin = false;
            TriggerTrainingThirdStage = true;
            MessageTraining.gameObject.SetActive(false);
        }
    }

    private void FunctionTrainingStartFirstStage()//функция начала тренировки (первый этап)
    {
        if (TriggerMouseDouble && !TriggerStartGame && TriggerMessageTrainingFirst)

        {
            TriggerMessageTrainingFirst = false;//переключаем триггер первого сообщения
            MessageTrainingFirst.gameObject.SetActive(false);//выключаем наше второе сообщение (оно же первое в тренировке) 
            TriggerMessageTraining = false; //помечаем, что сообщения прошли и можно двигаться
            TriggerTrainingFirstStage = true;//помечаем, что начался первый этап тренировки
            TriggerMouseDouble = false;
        }
    }

    private void FunctionTrainingEndFirstStage()//функция условий завершения первого этапа тренировки
    {
        if ((transform.position.x < -5 || transform.position.x > 5 || transform.position.z < -5 || transform.position.z > 5) && TriggerTrainingFirstStage)//если игрок откатился достаточно далеко от центра и это первый этап тренировки
        {
            TriggerTrainingFirstStage = false;//первый этап пройден
            TriggerMessageTraining = true;//у нас появляется сообщение
            TriggerMessageTrainingSecond = true;//готовимся ко второму сообщению в тренировке
            MessageTrainingSecond.gameObject.SetActive(true);//запускаем второе сообщение в тренировке
            TriggerMouseDouble = false;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }
    }

    private void FunctionTrainingStartSecondStage()
    {
        if (TriggerMouseDouble && !TriggerStartGame && TriggerMessageTrainingSecond)//повторяем всё, как для первого сообщения
        {
            TriggerMessageTrainingSecond = false;
            MessageTrainingSecond.gameObject.SetActive(false);
            TriggerMessageTraining = false;
            TriggerTrainingSecondStage = true;//помечаем, что начался второй этап тренировки
            TriggerMouseDouble = false;
        }
    }

    private void FunctionTrainingEndSecondStage()
    {
        if (((focalPoint.transform.rotation.y > 0.3 || focalPoint.transform.rotation.y < -0.3) && TriggerTrainingSecondStage))//если игрок повернул камеру более, чем на 180 градусов
        {


            TriggerTrainingSecondStage = false;//первый этап пройден
            TriggerMessageTraining = true;//у нас появляется сообщение
            TriggerMessageTrainingThird = true;//готовимся к третьему сообщению в тренировке
            MessageTrainingThird.gameObject.SetActive(true);//запускаем третье сообщение в тренировке
            TriggerMouseDouble = false;
            // Rotate = focalPoint.transform.rotation.y;//смотрим его поворот
        }
    }

    private void FunctionTrainingStartThirdStage()
    {
        if (TriggerMouseDouble && !TriggerStartGame && TriggerMessageTrainingThird)//повторяем всё, как для первого сообщения
        {
            TriggerMessageTrainingThird = false;
            MessageTrainingThird.gameObject.SetActive(false);
            TriggerMessageTraining = false;
            TriggerTrainingThirdStage = true;//помечаем, что начался третий этап тренировки
            TriggerMouseDouble = false;
            //TriggerPlayerPosition = false;
            playerRb.AddForce(focalPoint.transform.forward * 0);
            playerRb.AddForce(focalPoint.transform.right * 0);

        }
    }
    private void FunctionTrainingEndThirdStage()
    {
        if ((transform.position.y < -10 && TriggerTrainingThirdStage))//если игрок упал в пропасть
        {
            PlayerAppear(7);
        }


        if ((transform.position.x > -3 && transform.position.x < 3 && transform.position.z > -3 && transform.position.z < 3) && TriggerTrainingThirdStage)//если игрок вернулся обратно в центр
        {
            TriggerTrainingThirdStage = false;//третий этап пройден
            TriggerMessageTraining = true;//у нас появляется сообщение
            TriggerMessageTrainingFourth = true;//готовимся к четвёртому сообщению в тренировке
            MessageTrainingFourth.gameObject.SetActive(true);//запускаем четвёртое сообщение в тренировке
            TriggerMouseDouble = false;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }
    }
}
//продолжение в следующем проекте C:\Users\asus\MyFirstGame\KART\New Unity Project\Assets