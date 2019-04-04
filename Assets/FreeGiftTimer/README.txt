Free Gift Timer for Unity
=========================

Version 1.0.2

## Introduction

A time-based free gift is one way for users to play your game
periodically. The user has to wait for a certain period of time to
get the next free gift. Therefore he/she will come back to play
your game on a regular basis.

Free Gift Timer plugin makes you easily add countdown timer with
a few lines of code. Since this plugin gets the current time from
the Internet, it is difficult to cheat.

Target platform: Android, iOS (also work on Unity Editor)

Asset Store: https://www.assetstore.unity3d.com/#!/content/87245


## Features

  * Simple, easy to use
  * Automatic state saving and restoration
  * It is hard to cheat (because current time is gotten from the Internet)
  * No server required
  * Work on Android/iOS (also on Unity Editor)


## How to use

To use `FreeGiftTimer`, you do not need to place `GameObject` on
`Hierarchy`, you can operate it by directly calling the API from
the script.

You can distinguish between three states by examining the state of
`FreeGiftTimer`:

  * The clock is invalid
  * The clock is valid, but has yet to reach the time to send free
    gifts
  * The clock is valid, and you can send free gifts

You can handle free gifts appropriately by displaying the UI
according to the state.

For details on how to use the API, please see the sample scene
script or online documentation:

  https://goo.gl/4aUKcU


## Support

If you have any questions or troubles, please contact the following
e-mail address:

  support@peculia.jp


## Changelog

  * Apr 21 2017: Version 1.0.2
    * Add a graphical sample scene, which uses uGUI
  * Apr 6 2017: Version 1.0.0
    * First release
