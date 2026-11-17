import krpc

conn = krpc.connect(name="Adv. launch into orbit")

sc = conn.space_center
mj = conn.mech_jeb
ascent = mj.ascent_autopilot

#All of these options will be filled directly into Ascent Guidance window and can be modified manually during flight.
ascent.desired_orbit_altitude = 100000
ascent.desired_inclination = 6

ascent.force_roll = True
ascent.vertical_roll = 90
ascent.turn_roll = 90

ascent.ascent_type = 0 #use the Classic ascent profile

ascent.turn_shape_exponent = 0.5 #set the turn shape to 50%
ascent.auto_path = False #don't use autopath
ascent.turn_start_altitude = 3000
ascent.turn_start_velocity = 120
ascent.turn_end_altitude = 65000

ascent.autostage = True
ascent.enabled = True
sc.active_vessel.control.activate_next_stage() #launch the vessel

with conn.stream(getattr, ascent, "enabled") as enabled:
	enabled.rate = 1 #we don't need a high throughput rate, 1 second is more than enough
	with enabled.condition:
		while enabled():
			enabled.wait()

print("Launch complete!")
conn.close()
