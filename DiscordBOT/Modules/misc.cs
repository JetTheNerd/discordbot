using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DiscordBOT.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        DiscordSocketClient _client;

        [Command("echo")]
        public async Task Echo([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        public async Task Talk()
        {
            ulong id = 603324320202358815; // 3
            var chnl = _client.GetChannel(id) as IMessageChannel; // 4
            await Context.Channel.SendMessageAsync("BRUH");
        }
    }
}
