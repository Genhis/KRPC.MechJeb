import krpc

#This script assumes the vessel is next to the target and the target is a ship.
conn = krpc.connect(name="Docking with target")
sc = conn.space_center
mj = conn.mech_jeb
active = sc.active_vessel

print("Setting the first docking port as the controlling part")
parts = active.parts
parts.controlling = parts.docking_ports[0].part

print("Looking for a free docking port attached to the target vessel")
for dp in sc.target_vessel.parts.docking_ports:
    if not dp.docked_part:
        sc.target_docking_port = dp
        break

print("Starting the docking process")
docking = mj.docking_autopilot
docking.enabled = True

with conn.stream(getattr, docking, "enabled") as enabled:
    enabled.rate = 1 #we don't need a high throughput rate, 1 second is more than enough
    with enabled.condition:
        while enabled():
            enabled.wait()

print("Docking complete!")
conn.close()
