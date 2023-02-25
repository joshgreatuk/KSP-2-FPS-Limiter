using SpaceWarp.API;
using UnityEngine;

namespace Fpsl
{
    [MainMod]
    public class FpslMod : Mod
    {
        public Vector2 fpsLimits = new Vector2(0, 240);

        bool windowOpen = false;
        bool LimitEnabled = true;
        int targetFPS = 60;

        public override void Initialize()
        {
            Logger.Info("FPS Limiter is initialized");
        }

        private void Update()
        {
            if (Input.GetKeyDown("f2")) windowOpen = !windowOpen;
        }

        Rect windowRect = new Rect(new Vector2(Screen.width, Screen.height)/2, new Vector2(300, 100));

        private void OnGUI()
        {
            if (windowOpen)
            {
                windowRect = GUI.Window(0, windowRect, DrawWindow, "Inno's FPS Limiter");
            }
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.Label("This will disable Vsync!");
            LimitEnabled = GUILayout.Toggle(LimitEnabled, new GUIContent("Limiter Enabled"));

            GUILayout.BeginHorizontal();
            GUILayout.Label("FPS Target:");
            targetFPS = (int)GUILayout.HorizontalSlider(targetFPS, fpsLimits.x, fpsLimits.y);
            targetFPS = (int)Mathf.Clamp(Int32.Parse(GUILayout.TextField(targetFPS.ToString())), fpsLimits.x, fpsLimits.y);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0, 0, 300, 20));
        }

        private void FixedUpdate()
        {
            if (LimitEnabled && Application.targetFrameRate != targetFPS)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = targetFPS;
            }
            else if (!LimitEnabled) Application.targetFrameRate = -1;
        }
    }
}