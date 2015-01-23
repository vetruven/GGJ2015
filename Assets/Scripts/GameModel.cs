using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {
    public enum GameState{Menu, Game, End}

    public static GameState state = GameState.Game;
    public static bool isPlaying { get { return state == GameState.Game; } }
}
