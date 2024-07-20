import logging

from discord import Intents
from discord.ext import commands

from core._events import init_events
from core._driver.mongo import Database
from .configurations import basic_config, COGS_DIR

# List of features
from core.cogs.music import MusicCog

log = logging.getLogger("slvy")

class Slvy(commands.Bot):

    def __init__(self, *args, **kwargs):
        super().__init__(command_prefix= kwargs["prefix"], intents=Intents.all())
        self.synced = False
        self.token = None
        self.db = Database
        self._config = basic_config
        self._uptime = None

        self.help_command = None

    async def start(self, token):
        self.token = token
        await self._pre_login()
        await self.login(token)
        await self.connect()

    async def _pre_login(self) -> None:
        init_events(self)

    async def setup_hook(self) -> None:
        await self._pre_connect()

    async def _pre_connect(self) -> None:
        """
        All cogs will be added here
        """
        await self.add_cog(MusicCog(self))

    async def close(self):
        await super().close()
        await self.db.close_db_connection()

    
    # This is a help command custom so dont worry much about it
    # @commands.hybrid_command(name='help', description='Shows help information')
    # async def hybrid_help(self,ctx):
    #     help_command = MyHelpCommand()
    #     help_command.context = ctx
    #     await help_command.command_callback(ctx)