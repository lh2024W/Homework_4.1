namespace Homework_4._1
{
    public class Program
    {
        private static DatabaseService databaseService;

        static void Main()
        {
            databaseService = new DatabaseService();
            databaseService.EnsurePopulated();

            //databaseService.GetUserSettings(1);

            databaseService.RemoveUser(1);

        }
    }
}
