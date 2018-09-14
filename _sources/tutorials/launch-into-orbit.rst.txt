.. _tutorials/launch-into-orbit:
.. currentmodule:: MechJeb

The First Launch
================

This tutorial launches any rocket into a 100km circular orbit with 6° inclination.

The complete program is available in a variety of languages:

:download:`Java</scripts/tutorials/launch-into-orbit/LaunchIntoOrbit.java>`,
:download:`Python</scripts/tutorials/launch-into-orbit/LaunchIntoOrbit.py>`

We start with connecting to the server, getting ``AscentAutopilot`` module and setting launch parameters. Parameters which are omitted will be the same as in *Ascent Guidance* window.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/launch-into-orbit/LaunchIntoOrbit.java
      :language: java
      :lines: 12-27
      :lineno-start: 12
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/launch-into-orbit/LaunchIntoOrbit.py
      :language: python
      :lines: 1-17
      :linenos:

Next, we engage the autopilot and activate the first stage. After that, the autopilot takes over control of the vessel.
The program uses streams to check if the autopilot is still running. When the autopilot stops, the program continues. You can learn more about streams in kRPC documentation for your language:
`C# <https://krpc.github.io/krpc/csharp/client.html#synchronizing-with-stream-updates>`_, 
`C++ <https://krpc.github.io/krpc/cpp/client.html#synchronizing-with-stream-updates>`_, 
`Java <https://krpc.github.io/krpc/java/client.html#synchronizing-with-stream-updates>`_, 
`Lua <https://krpc.github.io/krpc/lua/client.html#synchronizing-with-stream-updates>`_, 
`Python <https://krpc.github.io/krpc/python/client.html#synchronizing-with-stream-updates>`_.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/launch-into-orbit/LaunchIntoOrbit.java
      :language: java
      :lines: 28-41
      :lineno-start: 28
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/launch-into-orbit/LaunchIntoOrbit.py
      :language: python
      :lines: 18-28
      :lineno-start: 18
      :linenos:

The rocket should now be in a circular 100km orbit with 6° inclination.
