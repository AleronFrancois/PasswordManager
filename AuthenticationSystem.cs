using System.Security.Cryptography;
using System.Text;
using System;


public class AuthenticationSystem {
    public static List<User> users = new List<User>(); // List of user accounts
    public static string path = "C:/Users/Aleron/Documents/GitHub/PasswordManager/Users.txt"; // Path to user database
    private static int userIndex = 0; // User index number


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
        string? username; // user's entered username
        string? password; // User's entered password
        string? choice = ""; // User's menu choice
        bool validCredentials = false; // Flag for invalid or valid login attempt

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
                    password = ComputeSha256Hash(password);
                    User user = new User(username, password);
                    users.Add(user);
                    userIndex++;
                    File.AppendAllText(path, $"[{userIndex}]\nUsername: {user.Username}\nPassword: {user.Password}\n------------------------------\n");
                    Console.WriteLine("\nSuccessful registration");
                    Login(); // Load login form
                }

            } else {
                Welcome(); // Load welcome form
            }
        }
    }


    private static void Login() {
        string? username; // User's entered username
        string? password; // User's entered password
        string? choice = ""; // User's menu choice
        bool loginSuccessful = false; // Flag for invalid or valid login attempt

        // Prompt user to enter login
        while (loginSuccessful == false) {
            Console.Write("\nEnter your username: ");
            username = Console.ReadLine();
            Console.Write("Enter your password: ");
            password = Console.ReadLine();

            password = ComputeSha256Hash(password);

            // Handle valid login credentials
            foreach (var user in users) {
                if (username == user.Username && password == user.Password) {
                    Console.WriteLine("\nSuccessful login");
                    AccountHome.Home(user); // Load users account form 
                    loginSuccessful = true;
                    break;
                }
            }

            // Handle invalid login credentials
            if (loginSuccessful == false) {
                Console.WriteLine("\nInvalid Credentials, please try again");
            }
        }
    }


    private static void LoadUsers() {
        // Preload all users from database
        if (File.Exists(path)) {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i += 4) {
                string username = lines[i + 1].Split(": ")[1];
                string password = lines[i + 2].Split(": ")[1];
                users.Add(new User(username, password));
            }
        }
    }


    static string ComputeSha256Hash(string password) {
        // Converts password to Sha256 hash
        using (SHA256 sha256 = SHA256.Create()) {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes) {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }


    private static void Main(string[] args) {
        LoadUsers(); // Preload users from database
        Welcome(); // Load welcome form
    }
}


