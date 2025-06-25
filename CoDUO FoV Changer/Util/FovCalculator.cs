using System;

namespace CoDUO_FoV_Changer.Util
{
    public static class FovCalculator
    {
        public const double FOUR_BY_THREE_ASPECT_RATIO = 1.33333333333333333;

        // temp location, move:
        public static double CalculateCgFov(double baseFov, double targetAspectRatio)
        {
            return 2 * Math.Atan(Math.Tan(baseFov / 2 * Math.PI / 180) * (targetAspectRatio / FOUR_BY_THREE_ASPECT_RATIO)) * 180 / Math.PI;
        }

        public static double NormalizeCgFov(double cgFov, double currentAspectRatio)
        {
            

            // Calculate the current aspect ratio

            // If the aspect ratio matches the base, just return the current FOV
            if (Math.Abs(currentAspectRatio - FOUR_BY_THREE_ASPECT_RATIO) < 0.001)
                return cgFov;

            // Convert cg_fov to radians
            double fovRadians = cgFov * Math.PI / 180;

            // Compute the normalized FOV using the formula
            double normalizedRadians = 2 * Math.Atan(
                Math.Tan(fovRadians / 2) * FOUR_BY_THREE_ASPECT_RATIO / currentAspectRatio
            );

            // Convert back to degrees
            return normalizedRadians * 180 / Math.PI;
        }
    }
}
