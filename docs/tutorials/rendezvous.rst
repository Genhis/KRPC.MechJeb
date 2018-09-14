.. _tutorials/rendezvous:
.. currentmodule:: MechJeb

Rendezvous with a Target
========================

This script will rendezvous any rocket with a targetted rocket. The script assumes the rocket is in orbit, planes match and the target is set.

The complete program is available in a variety of languages:

:download:`Java</scripts/tutorials/rendezvous/RendezvousWithTarget.java>`,
:download:`Python</scripts/tutorials/rendezvous/RendezvousWithTarget.py>`

First of all, we start with connecting to the server and initializing MechJeb service instance.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.java
      :language: java
      :lines: 16-19
      :lineno-start: 16
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 1-1
      :linenos:
    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 13-15
      :lineno-start: 13
      :linenos:

Then, we plan a Hohmann transfer to the target. If anything has gone wrong with the maneuver, we display a warning - the same as MechJeb would.
If we don't select a target, the program throws ``OperationException``.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.java
      :language: java
      :lines: 21-29
      :lineno-start: 21
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 17-25
      :lineno-start: 17
      :linenos:

After that, we execute the maneuver node we just created. Since we are going to create multiple maneuvers, we create a method to execute maneuver nodes.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.java
      :language: java
      :lines: 31-33
      :lineno-start: 31
      :linenos:
    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.java
      :language: java
      :lines: 54-65
      :lineno-start: 54
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 27-29
      :lineno-start: 27
      :linenos:
    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 3-11
      :lineno-start: 3
      :linenos:

Although we now have an intercept, the between vessels distance may be too large, so we use ``OperationCourseCorrection`` (in Maneuver Planner window known as *fine tune closest approach*).

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.java
      :language: java
      :lines: 35-41
      :lineno-start: 35
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 31-37
      :lineno-start: 31
      :linenos:

Finally, we match speed with the target and close connection when it's done.

.. tabs::

  .. group-tab:: Java

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.java
      :language: java
      :lines: 43-52
      :lineno-start: 43
      :linenos:

  .. group-tab:: Python

    .. literalinclude:: /scripts/tutorials/rendezvous/RendezvousWithTarget.py
      :language: python
      :lines: 39-47
      :lineno-start: 39
      :linenos:

The vessels should now be next to each other (50m distance), so we can start :ref:`tutorials/docking`.
