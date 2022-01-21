# Unofficial StashCat API Client

Based on the Python implementation <https://gitlab.com/aeberhardt/stashcat-api-client>

## Motivation

[StashCat](https://stashcat.com/en/) is the backend powering the schul.cloud app that is used by schools in Germany to communicate with parents. Unfortunately the Android schul.cloud app depends on Google push notifications which don't work on de-Googled phones (e.g. running [LineageOS](https://lineageos.org/)). But without notifications you would constantly have to open the app to see if something is new.

I used this API client to build my own notification system via Signal using <https://github.com/bbernhard/signal-cli-rest-api>. It's thus reduced to getting basic information about channels and conversations.

And if somebody from StashCat sees this: please support notifications on de-Googled phones. Signal, Threema, Teams - they all support it.

## Disclaimer

"StashCat" belongs to [stashcat GmbH](https://stashcat.com/en/legal-notice/), I am in no way affiliated to them and they have nothing to do with this repo.
