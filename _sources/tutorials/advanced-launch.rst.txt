.. _tutorials/advanced-launch:
.. currentmodule:: MechJeb

Advanced Launch into Orbit
==========================

This tutorial is about setting advanced path parameters to AscentAutopilot. If you haven't already done so, please read :ref:`tutorials/launch-into-orbit` tutorial.

The complete program is available in a variety of languages:

:download:`Java</scripts/tutorials/advanced-launch/AdvancedLaunchIntoOrbit.java>`,
:download:`Python</scripts/tutorials/advanced-launch/AdvancedLaunchIntoOrbit.py>`

First of all, you need to set the ascent path you want to use. If you don't, the program will use the path set in AscentGuidance window which may or may not be the one you want.
Then, you can set path parameters. The parameters are the same as if you used *Edit ascent path* option.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/advanced-launch/AdvancedLaunchIntoOrbit.java
      :language: java
      :lines: 28-35
      :lineno-start: 28
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/advanced-launch/AdvancedLaunchIntoOrbit.py
      :language: python
      :lines: 17-24
      :lineno-start: 17
      :linenos:
