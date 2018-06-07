﻿namespace RPGSystems.StatSystem {
    /// <summary>
    /// Allows the stat to scale based of a level
    /// </summary>
    public interface IStatScalable {
        void ScaleStatToLevel(int level);
    }
}
