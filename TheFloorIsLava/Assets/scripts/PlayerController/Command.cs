public class Command
{
    IAction action;

    public Command(IAction action)
    {
        this.action = action;
    }

    public void Execute()
    {
        action.Perform();
    }
}
