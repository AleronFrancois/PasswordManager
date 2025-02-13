public class AccountHome {
    public static void Home(User user) {
        string? choice = ""; // User's menu choice

        while (choice != "3") {
            Console.WriteLine($"\nWelcome {user.Username}");
            Console.WriteLine("[1] View profile");
            Console.WriteLine("[2] Logout");
            Console.WriteLine("[3] Exit");
            Console.Write("Enter an option: ");
            choice = Console.ReadLine();

            switch (choice) {
                case "1":
                    break;
                case "2":
                    AuthenticationSystem.Welcome(); 
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid response, please try again");
                    break;
            }
        } 
    }
}