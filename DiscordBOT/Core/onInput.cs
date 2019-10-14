using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBOT.Core
{
    internal static class onInput
    {
        private static Dictionary<string, string> dJson;
        private static SocketTextChannel channel;
        static string getJson(string key)
        {
            string json = File.ReadAllText("SystemLang/alerts.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            dJson = data.ToObject<Dictionary<string, string>>();
            if (dJson.ContainsKey(key)) return dJson[key];
            return "";
        }
        internal static Task reqInput()
        {
            
            channel = Global.Client.GetGuild(Convert.ToUInt64(Utilities.GetAlert("GUILDID"))).GetTextChannel(Convert.ToUInt64(Utilities.GetAlert("CHANNELID")));

            ConsoleKeyInfo cki;
            Console.ForegroundColor = ConsoleColor.Cyan;
            /* MOTD */
            Console.WriteLine("");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.Write("+ ");
            Console.WriteLine("Date: " + DateTime.Now.ToShortDateString() + " | Time: " + DateTime.Now.ToShortTimeString() + " +");
            Console.WriteLine("+ Press CTRL to send a TTS via bot +");
            Console.WriteLine("+ Press T to send a message via bot+");
            Console.WriteLine("+ Press G to change the guild ID   +");
            Console.WriteLine("+ Press C to change the channel ID +");
            Console.WriteLine("+ Press S to clear stdOut (console)+");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            Console.ForegroundColor = ConsoleColor.Gray;
            while (true)
            {
                try
                {
                    channel = Global.Client.GetGuild(Convert.ToUInt64(getJson("GUILDID"))).GetTextChannel(Convert.ToUInt64(getJson("CHANNELID")));
                } catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error processing new Client information! Please check SystemLang/alerts.json!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                cki = Console.ReadKey(true);
                char cCom = cki.KeyChar;
                if (cCom == 't' || cCom == 'T')
                {
                    Console.WriteLine("Requesting Input...");
                    Console.Write(">");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string sInput = Console.ReadLine();
                    send(sInput);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if ((cki.Modifiers.HasFlag(ConsoleModifiers.Control)))
                {
                    {
                        Console.WriteLine("Requesting TTS input...");
                        channel.EnterTypingState();
                        Console.Write(">");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        string sInput = Console.ReadLine();
                        send(sInput, true);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else if (cCom == 'C' || cCom == 'c')
                {
                    Console.WriteLine("Requesting new channel ID input... ");
                    Console.Write(">");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string sChannelID = Console.ReadLine();
                    string json = File.ReadAllText("SystemLang/alerts.json");
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    jsonObj["CHANNELID"] = sChannelID;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText("SystemLang/alerts.json", output);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Applying changes...");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Done...!");

                }
                else if (cCom == 'G' || cCom == 'g')
                {
                    Console.WriteLine("Requesting new guild ID input... ");
                    Console.Write(">");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string sGuildID = Console.ReadLine();
                    string json = File.ReadAllText("SystemLang/alerts.json");
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    jsonObj["GUILDID"] = sGuildID;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText("SystemLang/alerts.json", output);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Applying changes...");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Done...!");
                }
                else if (cCom == 'S' || cCom == 's')
                {
                    Console.Clear();
                }
            }
            // return Task.CompletedTask;
        }

        private static async void send(string sForward, bool tts = false)
        {
            /* if (Global.Client == null)
             {
                 Console.WriteLine("Client unready!");
                 return;
             }*/

            try
            {

                
                await channel.SendMessageAsync(sForward, tts);
            } catch
            {
                if (sForward == "" || sForward == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input cannot be null!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}
