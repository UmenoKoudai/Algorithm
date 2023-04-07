using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : InstanceSystem<GameManager>
{
    [SerializeField] Text _stageText;
    [SerializeField] PlayableDirector _director;
    int _stageNumber = 1;
    bool _isGame;

    public int StageNumber { get => _stageNumber; set => _stageNumber = value; }
    public bool IsGame { get => _isGame; }

    private void Awake()
    {
        base.Awake();
        _stageText.text = $"{_stageNumber}ŠK‘w";
        _stageText = FindObjectOfType<Text>();
        _director = FindObjectOfType<PlayableDirector>();
    }

    private void OnEnable()
    {
        _director.stopped += DirectorStop;
    }

    private void OnDisable()
    {
        _director.stopped -= DirectorStop;
    }

    void DirectorStop(PlayableDirector director)
    {
        if (_director == director)
        {
            _isGame = true;
            _director.gameObject.SetActive(false);
        }
    }
}
