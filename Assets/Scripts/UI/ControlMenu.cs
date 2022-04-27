using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlMenu : Singleton<ControlMenu>{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _principal;
    [SerializeField] private GameObject _sonido;
    [SerializeField] private GameObject _graficos;
    [SerializeField] private GameObject _canvasPrincipal;
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private Slider _volumenMaster;
    [SerializeField] private AudioMixer _ambienteMixer;
    [SerializeField] private Slider _volumenAmbiente;
    [SerializeField] private AudioMixer _efectosMixer;
    [SerializeField] private Slider _volumenEfectos;

    public bool Pausa { get; private set; }

    public void Start(){
        CambiarCalidad(PlayerPrefs.GetInt("numeroDeCalidad", 2));

        if (!PlayerPrefs.HasKey("volumenMaster") || !PlayerPrefs.HasKey("volumenAmbiente") || !PlayerPrefs.HasKey("volumenEfectos") || !PlayerPrefs.HasKey("volumenMasterBarra") || !PlayerPrefs.HasKey("volumenAmbienteBarra") || !PlayerPrefs.HasKey("volumenEfectosBarra")){
            PlayerPrefs.SetFloat("volumenMaster", 0);
            PlayerPrefs.SetFloat("volumenAmbiente", 0);
            PlayerPrefs.SetFloat("volumenEfectos", 0);
            PlayerPrefs.SetFloat("volumenMasterBarra", 1);
            PlayerPrefs.SetFloat("volumenAmbienteBarra", 1);
            PlayerPrefs.SetFloat("volumenEfectosBarra", 1);
        }

        _masterMixer.SetFloat("masterVol", PlayerPrefs.GetFloat("volumenMaster"));
        _volumenMaster.value = PlayerPrefs.GetFloat("volumenMasterBarra");

        _ambienteMixer.SetFloat("ambienteVol", PlayerPrefs.GetFloat("volumenAmbiente"));
        _volumenAmbiente.value = PlayerPrefs.GetFloat("volumenAmbienteBarra");

        _efectosMixer.SetFloat("efectosVol", PlayerPrefs.GetFloat("volumenEfectos"));
        _volumenEfectos.value = PlayerPrefs.GetFloat("volumenEfectosBarra");
    }

    protected override void Awake() {
        base.Awake();
        Pausa = false;
    }

    public void TogleMenu(){
        if(Pausa){
            Pausa = false;
            _menu.SetActive(false);
            Time.timeScale = 1;
            _canvasPrincipal.SetActive(true);
            _masterMixer.SetFloat("masterVol", PlayerPrefs.GetFloat("volumenMaster"));
        }else{
            _masterMixer.SetFloat("masterVol", -60f);
            ActivarVista(_principal);
            Pausa = true;
            _menu.SetActive(true);
            Time.timeScale = 0;
            _canvasPrincipal.SetActive(false);
        }
    }

    private void ActivarVista(GameObject vista){
        _principal.SetActive(false);
        _sonido.SetActive(false);
        _graficos.SetActive(false);
        vista.SetActive(true);
    }

    public void ActivarSonido(){
        ActivarVista(_sonido);
    }

    public void ActivarGraficos(){
        ActivarVista(_graficos);
    }

    public void ActivarPrincipal(){
        ActivarVista(_principal);
    }

    public void Salir(){
        Application.Quit();
    }

    public void CambiarCalidad(int calidad){
        Debug.Log(calidad);
        if(calidad == 0){
            BalanceoJuego.Instancia.Particulas = false;
        }else{
            BalanceoJuego.Instancia.Particulas = true;
        }
        QualitySettings.SetQualityLevel(calidad);
        PlayerPrefs.SetInt("numeroDeCalidad", calidad);
    }

    public void ControlVolumenMaster(){
        float valorVolumen = Mathf.Log10(_volumenMaster.value) * 20;
        _masterMixer.SetFloat("masterVol", valorVolumen);
        PlayerPrefs.SetFloat("volumenMaster", valorVolumen);
        PlayerPrefs.SetFloat("volumenMasterBarra", _volumenMaster.value);
    }

    public void ControlVolumenAmbiente(){
        float valorVolumen = Mathf.Log10(_volumenAmbiente.value) * 20;
        _ambienteMixer.SetFloat("ambienteVol", valorVolumen);
        PlayerPrefs.SetFloat("volumenAmbiente", valorVolumen);
        PlayerPrefs.SetFloat("volumenAmbienteBarra", _volumenAmbiente.value);
    }

    public void ControlVolumenEfectos(){
        float valorVolumen = Mathf.Log10(_volumenEfectos.value) * 20;
        _efectosMixer.SetFloat("efectosVol", valorVolumen);
        PlayerPrefs.SetFloat("volumenEfectos", valorVolumen);
        PlayerPrefs.SetFloat("volumenEfectosBarra", _volumenEfectos.value);
    }
}