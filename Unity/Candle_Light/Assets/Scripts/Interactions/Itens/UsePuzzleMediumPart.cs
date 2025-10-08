using UnityEngine;

public class UsePuzzleMediumPart : Interactable, IInteractable, IObserver

{
    [Tooltip("Cordenas de Um Pano cartesiano  X e Y")]
    [SerializeField] private Vector2 cordMap;

    //------------------------- Variaveis Globais privadas -------------------------------
    private bool action = false;
    private PointLight lightCandle;
    private int message = 0;
    private Vector2[] cordMapDirection = new Vector2[3];
    private Vector2[] Temporarydirection;
    void Start(){
        lightCandle = GetComponentInChildren<PointLight>();
        lightCandle.Extunguish();
    }

    private void OnEnable(){
        RegisterEvent();
    }
    private void OnDisable(){
        UnregisterEvent();
    }

    [HideInInspector] public bool IsFullyLit => lightCandle != null && lightCandle.visualLight.enabled && lightCandle.enabled;
    /*------------------------------------------------------------------------------
    Função:     OnEventRaised
    Descrição:  Chama a função respectiva do Atuador, para que ele possa executar sua função.
    Entrada:    int - indentificação para dizer qual ação o atuador fará.
                object - Informação com tipo generico do que o objeto faz
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void OnEventRaised(int message, object additionalInformation){
        Temporarydirection = (Vector2[])additionalInformation;
        if (cordMapDirection.Length != 3) cordMapDirection = new Vector2[3];
        if (Temporarydirection.Length >= 2){
            cordMapDirection[0] = Temporarydirection[0];
            cordMapDirection[1] = Temporarydirection[1];
        }
        cordMapDirection[2] = cordMap;
        cordMapDirection[0] += cordMap;
        cordMapDirection[1] += cordMap;
    }
    public void BaseAction(){
        ActiveSelf();
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    channel.NotifyObservers(message, cordMapDirection);
                }
            }
        }
    }
    public void ActiveSelf(){
        Debug.Log(gameObject.name + " Acendeu a vela");
        action = !action;
        message = action ? 1 : 0;
        ExecuteOrder(message);
        lightCandle.LightUp(action);
    }
    public Vector2[] GetCordMap(){
        return cordMapDirection;
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    protected override void UnregisterEvent(){
        UnregisterEventPublic();
    }

    /*------------------------------------------------------------------------------
    Função:     RegisterEvent
    Descrição:  Registra este objeto como um observador em todos os canais de evento.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void RegisterEvent(){
        if (_observerEventListening != null){
            foreach (var channel in _observerEventListening){
                if (channel != null){
                    channel.RegisterObserver(this);
                }
            }
        }
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEventPublic
    Descrição:  Desregistra este objeto de todos os canais de evento.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UnregisterEventPublic(){
        if (_observerEventListening  != null){
            foreach (var channel in _observerEventListening ){
                if (channel != null){
                    channel.UnregisterObserver(this);
                }
            }
        }
    }
}
