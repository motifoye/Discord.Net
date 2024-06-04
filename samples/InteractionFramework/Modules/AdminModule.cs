using Discord;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractionFramework.Modules
{
    public class AdminModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }

        private InteractionHandler _handler;

        public AdminModule(InteractionHandler handler)
        {
            _handler = handler;
        }

        [SlashCommand("kick", "Выгнать участника с сервера")]
        public async Task Kick(IGuildUser user, [Summary(description: "Причина")] string reason = null)
        {
            var CurrentUser = Context.Guild.GetUser(Context.User.Id);

            if (!CurrentUser.GuildPermissions.KickMembers)
            {
                await RespondAsync(text: ":red_circle: `Не достаточно прав`", ephemeral: true);
                return;
            }
            
            await user.KickAsync(reason);
            await RespondAsync(text: ":green_circle: `Команда выполнена`", ephemeral: true);
        }

        [SlashCommand("ban", "Забанить участника сервера")]
        public async Task Ban(IGuildUser user, [Summary(description: "Причина")] string reason = null)
        {
            var CurrentUser = Context.Guild.GetUser(Context.User.Id);

            if (!CurrentUser.GuildPermissions.BanMembers)
            {
                await RespondAsync(text: ":red_circle: `Не достаточно прав`", ephemeral: true);
                return;
            }

            await user.BanAsync(reason:reason);
            await RespondAsync(text: ":green_circle: `Команда выполнена`", ephemeral: true);
        }




        [SlashCommand("spawner", "swap")]
        public async Task Spawn()
        {
            var menuBuilder = new SelectMenuBuilder()
                .WithPlaceholder("Select an option")
                .WithCustomId("menu-1")
                .WithMinValues(1)
                .WithMaxValues(1)
                .AddOption("Option A", "opt-a", "Option B is lying!")
                .AddOption("Option B", "opt-b", "Option A is telling the truth!");

            var builder = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            await ReplyAsync("Whos really lying?", components: builder.Build());
        }

        [SlashCommand("role", "роль пользователю")]
        public async Task Role_56df165df(IGuildUser userid, IRole role)
        {
            var GuildUser = Context.Guild.GetUser(Context.User.Id);
            if (!GuildUser.GuildPermissions.ManageRoles)
            {
                await RespondAsync(text: ":warning:недостаточно прав", ephemeral: true);
                return;
            }
            await userid.AddRoleAsync(role);
            await RespondAsync(":wirning: роль");
        }
    }
}
