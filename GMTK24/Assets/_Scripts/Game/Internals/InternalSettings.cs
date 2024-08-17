namespace com.game.internals
{
    public static class InternalSettings
    {
        // If true, some additional console features get enabled for debugging.
        public static readonly bool DEBUG_MODE = false;

        // If true, the player can move even they are in a dialogue.
        public static readonly bool CAN_MOVE_IN_DIALOGUE = false;

        // If true, 'OpenGenericMenu' input action exits dialogue instead of opening the generic menu, if the player is in a dialogue.
        public static readonly bool ESC_EXITS_DIALOGUE = false;

        // If true, 'Cancel' input action unselects currently selected object -by 'EventSystem.current'- instead of appyling direct logic.
        public static readonly bool CANCEL_UNSELECTS_FIRST = false;

        // If true, player will exit dialogue automatically when they move.
        public static readonly bool EXIT_DIALOGUE_ON_MOVE = false;
    }
}
