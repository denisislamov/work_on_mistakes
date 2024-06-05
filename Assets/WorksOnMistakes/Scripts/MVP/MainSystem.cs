using UnityEngine;

public class MainSystem : MonoBehaviour
{
    [Header("MVP")]
    [SerializeField] private Data mvpData;
    [SerializeField] private MvpView mvpView;
    
    private MvpModel _mvpModel;
    private MvpPresenter _mvpPresenter;
    
    [Header("MVC")]
    [SerializeField] private MvcData mvcData;
    [SerializeField] private MvcView mvcView;
    
    private MvcModel _mvcModel;
    private MvcController _mvcController;
    
    [Header("MVVM")]
    [SerializeField] private MvvmModel mvvmModel;
    [SerializeField] private MvvmView mvvmView;
    
    private MvvmViewModel _mvvmViewModel;
    
    private void Awake()
    {
        _mvpModel = new MvpModel(mvpData);
        _mvpPresenter = new MvpPresenter(_mvpModel, mvpView);
        
        _mvcModel = new MvcModel(mvcData);
        _mvcController = new MvcController(_mvcModel, mvcView);
        
        _mvvmViewModel = new MvvmViewModel(mvvmModel);
        mvvmView.SetViewModel(_mvvmViewModel);
    }
    
    private void OnDestroy()
    {
        _mvpPresenter.UnregisterListeners();
        _mvcController.UnregisterListeners();
    }
}
