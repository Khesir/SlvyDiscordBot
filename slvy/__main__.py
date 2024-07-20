
import asyncio 
import core.configurations as config
import logging
from core.bot import Slvy
from logger import init_logging

log = logging.getLogger('main')

async def run_bot(slvy : Slvy) -> None:
    init_logging(0)
    token = config.basic_config['DISCORD_API_TOKEN']

    if token:
        await slvy.start(token)
    else:
        log.warning("Token must be set in the env")
        # somehting to close this
    return None

def main():
    slvy = None
    try:
        loop = asyncio.new_event_loop()

        config.load_basic_configuration()

        slvy = Slvy(prefix = config.getPrefix())
        loop.create_task(run_bot(slvy))

        loop.run_forever()
    except KeyboardInterrupt:
        log.info("Oh you, I recieved CTRL+C, gonna shut down")
        if slvy is not None:
            if slvy.is_closed():
                slvy.close()
    finally:
        loop.run_until_complete(loop.shutdown_asyncgens())

        loop.run_until_complete(asyncio.sleep(2))
        asyncio.set_event_loop(None)
        loop.stop()
        loop.close()

if __name__ == "__main__":
    main()