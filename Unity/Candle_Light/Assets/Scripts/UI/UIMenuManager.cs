/**************************************************************
    Jogos Digitais LOURDES
    UIMenuManager

    Descrição: Gerencia a UI do menu.

    Candle Light - Jogos Digitais LURDES –  29/03/2024
    Modificado por: Italo 
***************************************************************/

//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMenuManager : MonoBehaviour
{
    //-------------------------- Variaveis Globais Visiveis --------------------------------
    
    [SerializeField] private UIMainMenu _menuPainel = default;
    [SerializeField] private UISettingsController _settingPanel = default;
    [SerializeField] private UICredits _creditsPanel = default;
    [SerializeField] private UIPopup _popupPanel = default;
    [SerializeField] private InputReader _inputReader = default;

    /*------------------------------------------------------------------------------
    Função:     Start
    Descrição:  Inicializa as funções e os inputs para o menu funcionar
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    void Start(){
        _inputReader.EnableMenuInput();
        SetMenu();       
    }
    /*------------------------------------------------------------------------------
    Função:     SetMenu
    Descrição:  Atribui as funções de cada botão da interface.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void SetMenu(){
        _menuPainel.NewGameButtonAction += StartNewGame;
        _menuPainel.SettingsButtonAction += OpenSettings;
        _menuPainel.CreditsButtonAction += OpenCredits;
        _menuPainel.ExitButtonAction += ShowExitConfirmationPopup;
    }
    /*------------------------------------------------------------------------------
    Função:     StartNewGame
    Descrição:  Lógica para Loudar a Scena.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void StartNewGame(){
        SceneManager.LoadScene("MainHaill");
    }
    /*------------------------------------------------------------------------------
    Função:     OpenSettings
    Descrição:  Abre o painel de configuraçõoes e associa o a tecla para fecha-lo.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OpenSettings(){
        _settingPanel.gameObject.SetActive(true);
        _settingPanel.Closed += CloseSettings;
    }
    /*------------------------------------------------------------------------------
    Função:     CloseSettings
    Descrição:  Fecha o painel de configuraçõoes e Desassocia a tecla.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void CloseSettings(){
        _settingPanel.Closed -= CloseSettings;
        _settingPanel.gameObject.SetActive(false);
    }
    /*------------------------------------------------------------------------------
    Função:     OpenCredits
    Descrição:  Abre o painel de créditos e associa a tecla para fecha-lo.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OpenCredits(){
        _creditsPanel.gameObject.SetActive(true);
        _creditsPanel.Closed += CloseCredits;
    }
    /*------------------------------------------------------------------------------
    Função:     CloseCredits
    Descrição:  Fecha o painel de créditos e Desassocia a tecla.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void CloseCredits(){
        _creditsPanel.Closed -= CloseCredits;
        _creditsPanel.gameObject.SetActive(false);
    }
    /*------------------------------------------------------------------------------
    Função:     ShowExitConfirmationPopup
    Descrição:  Abre um popup de sair e associa a tecla para fecha-lo.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
	public void ShowExitConfirmationPopup(){
		_popupPanel.ConfirmationResponseAction += HideExitConfirmationPopup;
		_popupPanel.gameObject.SetActive(true);
	}
    /*------------------------------------------------------------------------------
    Função:     HideExitConfirmationPopup
    Descrição:  Fecha o popup/jogo de sair e associa a tecla para fecha-lo.
    Entrada:    bool - Identifica se o jogador clicou em sim ou não.
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void HideExitConfirmationPopup(bool quitConfirmed){
		_popupPanel.ConfirmationResponseAction -= HideExitConfirmationPopup;
		_popupPanel.gameObject.SetActive(false);
		if (quitConfirmed)Application.Quit();
	}
}
