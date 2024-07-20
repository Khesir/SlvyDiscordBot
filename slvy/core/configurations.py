import os
import pathlib

from dotenv import load_dotenv

# TODO: Figure out how to properly set this configuration for the bot

BASE_DIR = pathlib.Path(__file__).parent

COGS_DIR = BASE_DIR / "cogs"

basic_config = None

def load_basic_configuration():
    load_dotenv()

    global basic_config

    discord_api_token = os.getenv('DISCORD_API_TOKEN')
    discord_command_prefix = os.getenv('DISCORD_COMMAND_PREFIX', '!')

    basic_config = {
        'DISCORD_API_TOKEN': discord_api_token,
        'DISCORD_COMMAND_PREFIX': discord_command_prefix,
    }

# Learn to do kwargs, and args for dynamic configuration
class Configuration: 
    def __init__(self) -> None:
        pass

    def get_config_name(self, name) -> str:
        pass


def getDiscordToken():
    """ Gets the bot token
    
    Returns
    -------
    str
        token
    """
    try:
        return basic_config['DISCORD_API_TOKEN']
    except KeyError as e:
        raise RuntimeError("Bot basic config has not been loaded yet") from e

def getPrefix():
    """ Gets the desired prefix
    
    Returns
    -------
    str
        prefix
    """
    try:
        return basic_config['DISCORD_COMMAND_PREFIX']
    except KeyError as e:
        raise RuntimeError("Bot basic config has not been loaded yet") from e
