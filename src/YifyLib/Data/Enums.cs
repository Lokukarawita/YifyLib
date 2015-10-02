namespace YifyLib.Data
{
    /// <summary>
    /// Movie Image Type
    /// </summary>
    public enum MovieImageType
    {
        /// <summary>
        /// Background Image. Backdrop image from YTS
        /// </summary>
        Background, 
        /// <summary>
        /// Cover Image. Usually DVD, Blue-ray cover.
        /// </summary>
        Cover, 
        /// <summary>
        /// Screenshot Image from the video file
        /// </summary>
        ScreenShot
    }

    /// <summary>
    /// Image Sizes
    /// </summary>
    public enum ImageSize
    {
        /// <summary>
        /// Size is not relevant for particular type of image
        /// </summary>
        NotRelevent, 
        /// <summary>
        /// Small Image
        /// </summary>
        Small, 
        /// <summary>
        /// Large image
        /// </summary>
        Large, 
        /// <summary>
        /// Medium image
        /// </summary>
        Medium
    }
}