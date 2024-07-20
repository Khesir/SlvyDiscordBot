from datetime import datetime, timezone
import logging
import sys
from discord.ext import commands
import discord

import rich
from rich import box
from rich.table import Table
from rich.columns import Columns
from rich.panel import Panel
from rich.text import Text

INTRO = r"""
  _________.__               
 /   _____/|  |___  _____.__.
 \_____  \ |  |\  \/ <   |  |
 /        \|  |_\   / \___  |
/_______  /|____/\_/  / ____|
        \/            \/                                                   
"""

log = logging.getLogger()


def init_events(bot : discord.Client):

    @bot.event
    async def on_connect():
        if bot._uptime is None:
            log.info("Connected to Discord. Getting ready...")

    @bot.event
    async def on_ready():
        try:
            
            await _on_ready()
            await bot.wait_until_ready()
            if not bot.synced:
                await bot.tree.sync()
                bot.synced = True
                await SlvyStatus(bot)
        except Exception as exc:
            log.critical("The bot failed to get ready!", exc_info=exc)


    async def SlvyStatus(bot):
       
        activity = discord.Activity(
            type=discord.ActivityType.watching,
            name=f"Just testing"
        )
        await bot.change_presence(
            status=discord.Status.online,
            activity=activity
        )

        log.info("Slvy is Online")

    async def _on_ready():
        if bot._uptime is not None:
            return
        
        bot._uptime = datetime.now(timezone.utc)
        
        guilds = len(bot.guilds)
        users = len(set([m for m in bot.get_all_members()]))

        
        invite_url = discord.utils.oauth_url(bot.application_id, scopes=("bot",))
        dpy_version = discord.__version__

        
        table_general_info = Table(show_edge=False, show_header=False, box=box.MINIMAL)
        table_general_info.add_row("Prefix: s!")
        table_general_info.add_row("Discord.py version", dpy_version)

        table_counts = Table(show_edge=False, show_header=False, box=box.MINIMAL)

        table_counts.add_row("Servers", str(guilds))
        if bot.intents.members:  # Lets avoid 0 Unique Users
            table_counts.add_row("Unique Users", str(users))

        rich_console = rich.get_console()

        rich_console.print(INTRO, style="red", markup=False, highlight=False)
        if guilds:
            rich_console.print(
                Columns(
                    [Panel(table_general_info, title=bot.user.display_name), Panel(table_counts)],
                    equal=True,
                    align="center",
                )
            )
        else:
            rich_console.print(Columns([Panel(table_general_info, title=bot.user.display_name)]))

        
        rich_console.print(
            "Loaded {} cogs with {} commands".format(len(bot.cogs), len(bot.commands))
        )

        

        if invite_url:
            rich_console.print(f"\nInvite URL: {Text(invite_url, style=f'link {invite_url}')}")
            # We generally shouldn't care if the client supports it or not as Rich deals with it.

    @bot.event
    async def on_command_error(ctx, error):
        if isinstance(error, commands.CommandOnCooldown):
            msg = '**Command on cooldown** Retry after **{:.2f}s**'.format(
                error.retry_after)
            await ctx.send(msg)
        elif not isinstance(error, Exception):
            await log.warning(error)
        else:
            await log.warning(error)

    @bot.event
    async def on_guild_join(guild : discord.Guild):
        pass