// Import necessary namespaces
using System;
using System.Collections;
using System.Drawing; // Import for logo
using System.IO;
using System.Media; // Import for audio playback

// Define the namespace for the Chatbot class
namespace poe
{
    // Define a class to hold question-answer pairs
    public class Reply
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public Reply(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }
    }

    // Define the Chatbot class
    public class Chatbot
    {
        // Define private fields for the username and replies
        private string username;
        private string userAsk;
        private ArrayList replies;

        // Define the constructor for the Chatbot class
        public Chatbot()
        {
            // Initialize the replies ArrayList
            replies = new ArrayList();

            // Display a message to the user
            Console.WriteLine("Mini AI > ");
            Console.WriteLine("Enter your Name");
            Console.WriteLine("You > ");

            // Get the username from the user
            username = Console.ReadLine();

            // Define the path to the ASCII art image
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = path.Replace("bin\\Debug\\", "");
            string full_path = Path.Combine(new_path, "ascii-text-art.jpg");

            // Load the ASCII art image
            try
            {
                Bitmap logo = new Bitmap(full_path);
                logo = new Bitmap(logo, new Size(50, 25));

                // Display the ASCII art image
                for (int height = 0; height < logo.Height; height++)
                {
                    for (int width = 0; width < logo.Width; width++)
                    {
                        // Get the color of the current pixel
                        Color pixel = logo.GetPixel(width, height);
                        int color = (pixel.R + pixel.G + pixel.B) / 3;

                        // Determine the ASCII character to display based on the color
                        char ascii_design = color > 200 ? '.' :
                                            color > 150 ? '*' :
                                            color > 100 ? '0' :
                                            color > 50 ? '#' : '@';
                        Console.Write(ascii_design);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image: " + ex.Message);
            }

            // Store replies in the ArrayList
            StoreReplies();

            // Enter a loop to handle user input
            do
            {
                // Display a message to the user
                Console.WriteLine("Mini AI >");
                Console.WriteLine("Good day " + username + ", how can I help you today?");
                Console.WriteLine(username + " > ");

                // Get the user's input
                userAsk = Console.ReadLine();

                // Answer the user's question
                Answer(userAsk);

            } while (userAsk.ToLower() != "exit");
        }

        // Define a method to store replies in the ArrayList
        private void StoreReplies()
        {
            // Add replies to the ArrayList
            replies.Add(new Reply("password", "Make sure your password is secure"));
            replies.Add(new Reply("phishing", "Phishing is a cybercrime in which a target is contacted by email by someone posing as a legitimate institution"));
            replies.Add(new Reply("safe browsing", "Safe browsing is a service that protects users from dangerous websites"));
            replies.Add(new Reply("how", "I am good and yourself?"));
            replies.Add(new Reply("name", "My Name is mini AI"));
            replies.Add(new Reply("purpose", "I am here to help you find answers to your questions"));
            replies.Add(new Reply("ask", "About anything"));
        }

        // Define a method to answer the user's question
        private void Answer(string asked)
        {
            // Split the user's input into keywords
            string[] keywords = asked.ToLower().Split(new[] { ' ', ',', ';', '.' }, StringSplitOptions.RemoveEmptyEntries);
            bool foundReply = false;

            // Check each keyword against the replies in the ArrayList
            foreach (string keyword in keywords)
            {
                foreach (Reply reply in replies)
                {
                    if (reply.Question.Equals(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        // Display the reply to the user
                        Console.WriteLine("Mini AI: " + reply.Answer);
                        PlayResponseAudio(); // method call to play audio response
                        foundReply = true;
                    }
                }
            }

            // If no replies were found, display a default message
            if (!foundReply)
            {
                Console.WriteLine("Mini AI: Sorry, I don't understand your question.");
                PlayResponseAudio(); // Play audio for unrecognized input
            }
        }

        // Define a method to play audio response
        public void PlayResponseAudio()
        {
            try
            {
                // Specify the path to the audio file
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                using (SoundPlayer player = new SoundPlayer(audioPath))
                {
                    player.PlaySync(); // Play the audio file 
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error playing audio: " + error.Message);
            } // end of catch
        }
    }

    
}