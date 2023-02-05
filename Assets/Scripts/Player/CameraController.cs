using UnityEngine;
using FeTo.SOArchitecture;

[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    private static string
        TRIGGER_TOTECHTREE = "techtree",
        TRIGGER_TODEFENSE = "defense";

    Animator animator;

    [SerializeField] IntVariable gameMode;
    [SerializeField] GameEvent gameModeChanged;

    private void Awake() {
        animator = GetComponent<Animator>();
        gameMode.SetValue((int)GameMode.DEFENSE);
    }

    public void ChangeMode() {
        if (gameMode.Value == (int)GameMode.DEFENSE) {
            animator.SetTrigger(TRIGGER_TOTECHTREE);
            gameMode.SetValue((int)GameMode.SWITCHING);
            gameModeChanged.Raise();
        } else if (gameMode.Value == (int)GameMode.TECHTREE) {
            animator.SetTrigger(TRIGGER_TODEFENSE);
            gameMode.SetValue((int)GameMode.SWITCHING);
            gameModeChanged.Raise();
        }
        // If switching game mode, ignore
    }

    public void setGameMode(GameMode gameMode) {
        this.gameMode.SetValue((int)gameMode);
        gameModeChanged.Raise();
    }

}
