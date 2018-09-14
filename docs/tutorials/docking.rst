.. _tutorials/docking:
.. currentmodule:: MechJeb

Docking with a Target
=====================

This script will dock any rocket with a targetted rocket. The script assumes the target is a ship and it is next to the target.

The complete program is available in a variety of languages:

:download:`Java</scripts/tutorials/docking/DockingWithTarget.java>`,
:download:`Python</scripts/tutorials/docking/DockingWithTarget.py>`

First of all, we start with connecting to the server, initializing service instances and getting the active vessel.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/docking/DockingWithTarget.java
      :language: java
      :lines: 15-21
      :lineno-start: 15
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/docking/DockingWithTarget.py
      :language: python
      :lines: 1-7
      :linenos:

Then, we set the first docking port as the controlling part, find a free docking port attached to the target vessel and set it as the target.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/docking/DockingWithTarget.java
      :language: java
      :lines: 23-32
      :lineno-start: 23
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/docking/DockingWithTarget.py
      :language: python
      :lines: 9-17
      :lineno-start: 9
      :linenos:

Finally, we engage Docking Autopilot and close the connection when it finishes.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/docking/DockingWithTarget.java
      :language: java
      :lines: 34-48
      :lineno-start: 34
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/docking/DockingWithTarget.py
      :language: python
      :lines: 19-30
      :lineno-start: 19
      :linenos:

The vessels should now be docked.
