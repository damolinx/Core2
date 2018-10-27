namespace Core2.Commands.Menu
{
    public class PageMenuCommand : MenuCommand
    {
        public PageMenuCommand(string title, string backLabel = "Back")
            : base(title, backLabel)
        {
            this.Settings.RequiresClearScreen = true;
        }
    }
}
