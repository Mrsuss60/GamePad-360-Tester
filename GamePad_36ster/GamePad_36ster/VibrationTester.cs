using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GamepadTester
{
    public class VibrationTester
    {
        private const float VibrationThreshold = 0.0f; // 
        private const float VibrationAmplification = 1.2f; 
        private const float SmoothingFactor = 0.3f; 
        private float currentLeftMotor = 0f;
        private float currentRightMotor = 0f;

        public void Update(GamePadState currentState, PlayerIndex playerIndex)
        {
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
            if (triggerValue <= VibrationThreshold)
                return 0f;

            float normalizedValue = (triggerValue - VibrationThreshold) / (1f - VibrationThreshold);

            float curvedValue = CustomCurve(normalizedValue);

            float mappedValue = MathHelper.Lerp(5f / 255f, 1f, curvedValue);

            float amplifiedValue = mappedValue * VibrationAmplification;

            return MathHelper.Clamp(amplifiedValue, 0f, 1f);
        }

        private float CustomCurve(float x)
        {
            return x * x * (3 - 2 * x);
        }

        private float SmoothValue(float current, float target)
        {
            return MathHelper.Lerp(current, target, SmoothingFactor);
        }
    }
}