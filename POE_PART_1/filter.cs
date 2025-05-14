using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Net.NetworkInformation;
using System.IO;

namespace POE_PART_1
{
    public class filter
    {
        private List<string> topics = new List<string>();
        private List<List<string>> topicReplies = new List<List<string>>();
        private HashSet<string> ignoreWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> cybersecurityKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private List<string> sentimentWords = new List<string>();
        private List<string> sentimentMessages = new List<string>();
        private string lastTopic = null;
        private Random rand = new Random();
        public filter()
        {

            // Initialize stored replies, ignored words, cybersecurity and sentiment keywords 
            StoreTopicsReplies();
            StoreIgnoreWords();
            StoreCybersecurityKeywords();
            StoreSentimentWords(); 
        }
        public string ProcessQuestions(string question, User_Memory memory)
        {
            if (string.IsNullOrEmpty(question))
                return "Please enter a valid question.";

            string lowerInput = question.ToLower();

            // Detect sentiment
            string sentiment = DetectSentiment(lowerInput);
            int sentimentIndex = sentimentWords.IndexOf(sentiment); 

            if ((lowerInput.Contains("remind") || lowerInput.Contains("favourite")) && !string.IsNullOrEmpty(memory.FavoriteTopic))
            {
                int Index = topics.IndexOf(memory.FavoriteTopic);
                if (Index < 0) 
                {
                    var responses = topicReplies[Index];
                    return $"{memory.Username}, here something about your favorite topic ({memory.FavoriteTopic}):\n{responses[rand.Next(responses.Count)]}";
                }
            }
            // Split input into words, filter ignored words, and keep only cybersecurity-related words
            string[] words = question.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> filteredWords = new List<string>();

            foreach (var word in words)
            {
                string lowerWord = word.ToLower();
                if (!ignoreWords.Contains(lowerWord) && cybersecurityKeywords.Contains(lowerWord))
                {
                    filteredWords.Add(lowerWord);
                }
            }
            // If sentiment detected
            if (sentimentIndex >= 0)
            {
                string msg = sentimentMessages[sentimentIndex];

                if (filteredWords.Count == 0)
                    return msg + " Feel free to ask any cybersecurity-related question.";
                else
                    return msg + "\nHere's something useful:\n" + GetTopicResponse(filteredWords[0], memory);
            }

            if (filteredWords.Count == 0)
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    int index = topics.IndexOf(lastTopic);
                    if (index >= 0)
                    {
                        return $"Following up on {lastTopic}, here's more info:\n{topicReplies[index][rand.Next(topicReplies[index].Count)]}";
                    }
                }

                return "Hmm, I couldn't detect a cybersecurity topic in your question. Try again?";
            }

            // Match and respond to detected keyword
            return GetTopicResponse(filteredWords[0], memory);

        } // end of processquestions 

        private string GetTopicResponse(string topic, User_Memory memory)
        {
            lastTopic = topic;
            int index = topics.IndexOf(topic);
            if (index >= 0)
            {
                var responses = topicReplies[index];
                return $"{memory.Username}, here's something about {topic}:\n{responses[rand.Next(responses.Count)]}";
            }

            return $"I recognize that as a cybersecurity keyword, but I don’t have tips on {topic} yet.";
        }
        
        //  detect sentiment 
        private string DetectSentiment(string input)
        {
            for (int i = 0; i < sentimentWords.Count; i++)
            {
                if (input.Contains(sentimentWords[i]))
                    return sentimentWords[i];
            }
            return "neutral";
        }
      public void TryCaptureUserPreference(string input, User_Memory memory)
        {
            string lowered = input.ToLower(); 
            foreach (var keyword in cybersecurityKeywords)
            {
                if (lowered.Contains("favorite") && lowered.Contains(keyword))
                {
                    memory.FavoriteTopic = keyword;
                }
            }
        }

        private void StoreTopicsReplies()
            {
            // use name index 
            topics.Add("password");
            topicReplies.Add(new List<string>
        {
            "Use strong, unique passwords for each account.",
            "Avoid common passwords like '123456'.",
            "A good password should be at least 12 characters with mixed symbols."
        });

            topics.Add("phishing");
            topicReplies.Add(new List<string>
        {
            "Phishing is a fake message pretending to be real. Don’t click suspicious links.",
            "Hover over email links before clicking to verify the URL.",
            "Never give out personal info unless you're sure of the recipient."
        });

            topics.Add("sql");
            topicReplies.Add(new List<string>
        {
            "SQL Injection allows hackers to access your database via vulnerable queries.",
            "Use prepared statements or stored procedures to avoid SQL Injection.",
            "Always sanitize user inputs in database-driven apps."
        });

            topics.Add("malware");
            topicReplies.Add(new List<string>
        {
            "Malware can steal your data or damage your files. Use antivirus software.",
            "Keep your OS and apps updated to block malware.",
            "Avoid downloading files from untrusted sources."
        });

            // Add more as needed...
        }


            private void StoreIgnoreWords()
            {
                string[] commonWords = { "what", "is", "a", "tell", "how", "can", "I", "about", "the", "explain", "does", "to", "are", "and" };
                foreach (var word in commonWords)
                {
                    ignoreWords.Add(word);
                }
            }

            private void StoreCybersecurityKeywords()
            {
                string[] keywords = { "password", "sql", "injection", "phishing", "firewall", "hacking", "hacked", "authentication", "encryption", "data", "security", "malware", "virus", "breach", "protection", "attack", "attacked" };
                foreach (var word in keywords)
                {
                    cybersecurityKeywords.Add(word);
                }
            }

       

    } // end of class
} // end of file


