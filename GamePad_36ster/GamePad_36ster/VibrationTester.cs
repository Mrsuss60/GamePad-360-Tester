using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GamepadTester
{
    public class VibrationTester
    {
        private const float VIBRATION_THRESHOLD = 0.01f;
        private const float VIBRATION_AMPLIFICATION = 1.0f; 
        private const float SMOOTHING_FACTOR = 0.1f;
        private const float MIN_VIBRATION_INTENSITY = 0.005f; 
        private const float MAX_VIBRATION_INTENSITY = 1.0f; 

        private float currentLeftMotor = 0f;
        private float currentRightMotor = 0f;
        private bool wasConnected = false;

        public void Update(GamePadState currentState, PlayerIndex playerIndex)
        {
            if (!currentState.IsConnected)
            {
                if (wasConnected)
                {
                    GamePad.SetVibration(playerIndex, 0f, 0f);
                    currentLeftMotor = 0f;
                    currentRightMotor = 0f;
                }
                wasConnected = false;
                return;
            }
            wasConnected = true;

            float leftTrigger = currentState.Triggers.Left;
            float rightTrigger = currentState.Triggers.Right;

            float targetLeftMotor = CalculateMotorIntensity(leftTrigger);
            float targetRightMotor = CalculateMotorIntensity(rightTrigger);

            currentLeftMotor = SmoothValue(currentLeftMotor, targetLeftMotor);
            currentRightMotor = SmoothValue(currentRightMotor, targetRightMotor);

            GamePad.SetVibration(playerIndex, currentLeftMotor, currentRightMotor);
        }

        private float CalculateMotorIntensity(float triggerValue)
        {
            if (triggerValue <= VIBRATION_THRESHOLD)
                return 0f;

            float normalizedValue = (triggerValue - VIBRATION_THRESHOLD) / (1f - VIBRATION_THRESHOLD);
            float mappedValue = MathHelper.Lerp(MIN_VIBRATION_INTENSITY, MAX_VIBRATION_INTENSITY, normalizedValue);
            float finalValue = mappedValue * VIBRATION_AMPLIFICATION;

            return MathHelper.Clamp(finalValue, 0f, MAX_VIBRATION_INTENSITY);
        }

        private float SmoothValue(float current, float target)
        {
            return MathHelper.Lerp(current, target, SMOOTHING_FACTOR);
        }

        public void UpdateImmediate(GamePadState currentState, PlayerIndex playerIndex)
        {
            if (!currentState.IsConnected)
            {
                GamePad.SetVibration(playerIndex, 0f, 0f);
                return;
            }

            float leftIntensity = Math.Max(0f, currentState.Triggers.Left - VIBRATION_THRESHOLD);
            float rightIntensity = Math.Max(0f, currentState.Triggers.Right - VIBRATION_THRESHOLD);

            if (leftIntensity > 0f)
                leftIntensity = leftIntensity / (1f - VIBRATION_THRESHOLD);
            if (rightIntensity > 0f)
                rightIntensity = rightIntensity / (1f - VIBRATION_THRESHOLD);

            GamePad.SetVibration(playerIndex, leftIntensity, rightIntensity);
        }

        public void UpdateSensitiveCurve(GamePadState currentState, PlayerIndex playerIndex)
        {
            if (!currentState.IsConnected)
            {
                GamePad.SetVibration(playerIndex, 0f, 0f);
                return;
            }

            float leftTrigger = currentState.Triggers.Left;
            float rightTrigger = currentState.Triggers.Right;

            float leftIntensity = CalculateWithSensitiveCurve(leftTrigger);
            float rightIntensity = CalculateWithSensitiveCurve(rightTrigger);

            GamePad.SetVibration(playerIndex, leftIntensity, rightIntensity);
        }

        private float CalculateWithSensitiveCurve(float triggerValue)
        {
            if (triggerValue <= VIBRATION_THRESHOLD)
                return 0f;

            float normalized = (triggerValue - VIBRATION_THRESHOLD) / (1f - VIBRATION_THRESHOLD);

            float curved = (float)Math.Pow(normalized, 0.5);

            return curved;
        }

        public void Stop(PlayerIndex playerIndex)
        {
            GamePad.SetVibration(playerIndex, 0f, 0f);
            currentLeftMotor = 0f;
            currentRightMotor = 0f;
        }
    }
}