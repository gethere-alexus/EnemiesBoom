namespace Infrastructure.Services.WindowProvider
{
    public interface IWindowsProvider
    {
        
        void OpenWindow(WindowType window);
        void CloseWindow(WindowType window);
    }
}