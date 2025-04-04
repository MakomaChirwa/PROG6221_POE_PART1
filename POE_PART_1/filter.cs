using System.Collections.Generic;
using System;

namespace POE_PART_1
{
        public class filter
        {
            private List<string> replies = new List<string>();
            private HashSet<string> ignoreWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            private HashSet<string> cybersecurityKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            public filter()
            {

                // Initialize stored replies, ignored words, and cybersecurity keywords
                StoreReplies();
                StoreIgnoreWords();
                StoreCybersecurityKeywords();
            }
            public string ProcessQuestions(string question)
            {
                if (string.IsNullOrEmpty(question))
                {
                    return "Please enter a valid question.";
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

                if (filteredWords.Count == 0)
                {
                    return "Your question doesn't seem to be related to cybersecurity. Please ask something related to online security, hacking threats, or data protection.";
                }

                // Search for replies
                HashSet<string> matchedReplies = new HashSet<string>();

                foreach (var word in filteredWords)
                {
                    foreach (var reply in replies)
                    {
                        if (reply.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchedReplies.Add(reply);
                        }
                    }
                }

                // Return response
                if (matchedReplies.Count > 0)
                {
                    return "Possible answers:\n- " + string.Join("\n- ", matchedReplies);
                }
                else
                {
                    return "I can only answer cybersecurity-related questions. Please try again.";
                }


            }

            private void StoreReplies()
            {
                replies.Add("Passwords need to be protected.");
                replies.Add("Importance of password:" +
                   "\r\n\r\nPasswords are the first line of defense against cyber threats." +
                   "\r\n\r\nWeak passwords are a major cause of data breaches." +
                   "\r\n\r\nA strong password protects sensitive data and online accounts.");
                replies.Add("Structured Query Language (SQL) plays a crucial role in cybersecurity, both as a target of cyberattacks (SQL Injection) and as a tool for security analysis (log monitoring, threat detection, and auditing).\r\n\r\n." +
                    "What is SQL Injection?" +
                    "\r\nSQL injection (SQLi) is a common web attack where hackers exploit vulnerable SQL queries to manipulate a database, steal data, or gain unauthorized access." +
                    "How SQL Injection Works" +
                    "\r\n\r\nAttackers insert malicious SQL code into input fields (e.g., login forms, search bars)." +
                    "\r\n\r\nThe server processes the input as an SQL query, allowing attackers to bypass authentication, retrieve sensitive information, or alter data." +
                    "How to Prevent SQL Injection:" +
                    "✅ Use Stored Procedures to limit direct SQL execution." +
                    "\r\n✅ Sanitize user input—block special characters (', --, ;, etc.)." +
                    "\r\n✅ Limit database permissions—use the least privilege principle." +
                    "\r\n✅ Use Web Application Firewalls (WAFs) to detect and block SQLi attempts.");
                replies.Add("The most common cyber attack is phishing." +
                    "\r\n\r\nOthers are ransomware, data breaches, identity theft.");
                replies.Add("Ways to deal with threats:" +
                    "\r\n\r\nKeep software and operating systems updated to patch vulnerabilities." +
                    "\r\n✅ Use strong passwords and enable Multi-Factor Authentication (MFA)." +
                    "\r\n✅ Be cautious of phishing attacks—don’t click on suspicious links or attachments." +
                    "\r\n✅ Back up important data regularly (cloud + offline storage)." +
                    "\r\n✅ Use firewalls and antivirus software to protect against malware." +
                    "\r\n✅ Limit public Wi-Fi use—use a VPN for secure browsing.\r\n\r\n");
                replies.Add("What to do if you have been attacked:" + "" +
                    " 1️⃣ Disconnect from the internet (for malware, ransomware, or unauthorized access)." +
                    "\r\n2️⃣ Change affected passwords and notify your security team." +
                    "\r\n3️⃣ Check bank accounts and emails for signs of fraud." +
                    "\r\n4️⃣ Scan your system using updated antivirus software." +
                    "\r\n5️⃣ Report cyber incidents to IT security, law enforcement, or cybersecurity authorities.\r\n\r\n.");
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
                string[] keywords = { "password", "sql", "injection", "phishing", "firewall", "hacking", "authentication", "encryption", "data", "security", "malware", "virus", "breach", "protection", "attack", "attacked" };
                foreach (var word in keywords)
                {
                    cybersecurityKeywords.Add(word);
                }
            }
        } // end of class
    } // end of file


