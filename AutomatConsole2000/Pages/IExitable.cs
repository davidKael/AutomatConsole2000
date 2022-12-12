namespace AutomatConsole2000.Pages
{
    /// <summary>
    /// Use on Page to declare it as Exitable
    /// </summary>
    internal interface IExitable
    {
        /// <summary>
        /// Define what should happen when exiting
        /// </summary>
        void Exit();
    }
}