import krpc

def execute_node():
    print("Executing next maneuver node")
    executor.execute_one_node()
    
    with conn.stream(getattr, executor, "enabled") as enabled:
        enabled.rate = 1 #we don't need a high throughput rate, 1 second is more than enough
        with enabled.condition:
            while enabled():
                enabled.wait()

#This script assumes the vessel is in orbit, planes match and the target is set.
conn = krpc.connect(name="Rendezvous with target")
mj = conn.mech_jeb

print("Planning Hohmann transfer")
planner = mj.maneuver_planner
hohmann = planner.operation_transfer
hohmann.make_node()

#check for warnings
warning = hohmann.error_message
if warning:
    print(warning)

#execute the node
executor = mj.node_executor
execute_node()

#fine tune closest approach to the target
print("Correcting course")
fineTuneClosestApproach = planner.operation_course_correction
fineTuneClosestApproach.intercept_distance = 50 #50 meters seems to be optimal distance; if you use 0, you can hit the target
fineTuneClosestApproach.make_node()
executor.tolerance = 0.01 #do a high-precision maneuver (0.01 dV tolerance)
execute_node()

print("Matching speed with the target")
matchSpeed = planner.operation_kill_rel_vel
matchSpeed.time_selector.time_reference = mj.TimeReference.closest_approach #match speed at the closest approach
matchSpeed.make_node()
executor.tolerance = 0.1 #return the precision back to normal
execute_node()

print("Rendezvous complete!")
conn.close()
