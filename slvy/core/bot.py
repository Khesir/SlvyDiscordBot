

from discord import Intents
from discord.ext import commands

from core._events import init_events

from core.cogs.music import MusicCog

class Slvy(commands.Bot):

    def __init__(self, *args, **kwargs):
        super().__init__(command_prefix= kwargs["prefix"], intents=Intents.all())
        self.synced = False
        self.token = None
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
        # await self.db.close_db_connection()