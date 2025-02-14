public class AccountHome {
    public static void Home(User user) {
        string? choice = ""; // User's menu choice

        while (choice != "3") {
            Console.WriteLine($"\nWelcome {user.Username}");
            Console.WriteLine("[1] View profile");
            Console.WriteLine("[2] Change username");
            Console.WriteLine("[3] Logout");
            Console.WriteLine("[4] Exit");
            Console.Write("Enter an option: ");
            choice = Console.ReadLine();

            switch (choice) {
                case "1":
                    Profile(user);
                    break;
                case "2":
                    
                    break;
                case "3":
                    AuthenticationSystem.Welcome(); 
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid response, please try again");
                    break;
            }
        } 
    }


    public static void Profile(User user) {
        Console.WriteLine($"\nUsername: {user.Username}\nPassword: {user.Password}");
    }
}