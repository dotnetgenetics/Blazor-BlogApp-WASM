namespace BlogAppWASM.Services
{
    public class BlogStateService
    {
        public event Action? OnChange;
        public void NotifyStateChanges() => OnChange?.Invoke();
    }
}
