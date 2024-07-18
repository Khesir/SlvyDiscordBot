
import asyncio 
import core.configurations as config
from core.bot import Slvy

async def run_bot(slvy : Slvy) -> None:
    
    token = config.basic_config['DISCORD_API_TOKEN']

    if token:
        await slvy.start(token)
    else:
        print("Token must be set in the env")
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
        print("Oh you, I recieved CTRL+C, gonna shut down")
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