using System;
using System.ComponentModel;


public class AuthenticationSystem {
    public static List<User> users = new List<User>(); // List of user accounts


    public static void Welcome() {
        string? choice = ""; // User's menu choice

        // Display interactive menu prompt
        while (choice != "3") {
            Console.WriteLine("\nHello, please register or login");
            Console.WriteLine("[1] Register");
            Console.WriteLine("[2] Login");
            Console.WriteLine("[3] Exit");
            Console.Write("Enter an option: ");
            choice = Console.ReadLine();

            // Handle user's menu choice
            switch (choice) {
                case "1": // Load register form
                    Register();
                    break;
                case "2": // Load login form
                    Login();
                    break;
                case "3": // Exit window
                    Environment.Exit(0); 
                    break;
                default: // Handle invalid response
                    Console.WriteLine("\nInvalid response, please try again");
                    break;
            }   
        }    
    }


    private static void Register() {
        string? username;
        string? password;
        string? choice = "";
        bool validCredentials = false;

        // Prompt user to enter account credentials
        while (validCredentials == false) {
            Console.Write("\nEnter a username: ");
            username = Console.ReadLine();
            Console.Write("Enter a password: ");
            password = Console.ReadLine();
            Console.Write("Please confirm if you would like to create this account (yes or no): ");
            choice = Console.ReadLine();

            // Check for valid user credentials
            if (choice.ToLower() == "yes") {
                validCredentials = true; // Flag for valid or invalid credentials

                // Check if username already exists
                foreach (var existingUsername in users) {
                    if (username == existingUsername.Username) {
                        Console.WriteLine("\nThis username already exists, please try again");
                        validCredentials = false;
                        break;
                    }
                }

                // Ensure password length is appropriate 
                if (validCredentials && (password.Length < 8 || password.Length > 20)) {
                    validCredentials = false;
                    if (password.Length < 8) Console.WriteLine("\nInvalid password, too short");
                    if (password.Length > 20) Console.WriteLine("\nInvalid password, too long");
                }

                // If user credentials are valid, create and add new account to list of accounts
                if (validCredentials == true) {
                    User user = new User(username, password);
                    users.Add(user);
                    Console.WriteLine("\nSuccessful registration");
                    Login(); // Load login form
                }

            } else {
                Welcome(); // Load welcome form
            }
        }
    }


    private static void Login() {
        string? username;
        string? password;
        string? choice = "";
        bool loginSuccessful = false;

        while (loginSuccessful == false) {
            Console.Write("\nEnter your username: ");
            username = Console.ReadLine();
            Console.Write("Enter your password: ");
            password = Console.ReadLine();

            foreach (var user in users) {
                if (username == user.Username && password == user.Password) {
                    Console.WriteLine("\nSuccessful login");
                    AccountHome.Home(user); // Load users account form 
                    loginSuccessful = true;
                    break;
                }
            }

            if (loginSuccessful == false) {
                Console.WriteLine("\nInvalid Credentials, please try again");
            }
        }
    }


    private static void Main(string[] args) {
        Welcome(); // Load welcome form
    }
}


