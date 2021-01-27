import java.io.IOException;

import krpc.client.Connection;
import krpc.client.RPCException;
import krpc.client.Stream;
import krpc.client.StreamException;
import krpc.client.services.MechJeb;
import krpc.client.services.MechJeb.AscentAutopilot;
import krpc.client.services.MechJeb.AscentClassic;
import krpc.client.services.SpaceCenter;

public class AdvancedLaunchIntoOrbit {
    public static void main(String[] args) throws IOException, RPCException, StreamException {
        try(Connection conn = Connection.newInstance("Adv. launch into orbit")) {
            SpaceCenter sc = SpaceCenter.newInstance(conn);
            MechJeb mj = MechJeb.newInstance(conn);
            AscentAutopilot ascent = mj.getAscentAutopilot();

            //All of these options will be filled directly into AscentGuidance window and can be modified manually during flight.
            ascent.setDesiredOrbitAltitude(100000);
            ascent.setDesiredInclination(6);

            ascent.setForceRoll(true);
            ascent.setVerticalRoll(90);
            ascent.setTurnRoll(90);

            ascent.setAscentPathIndex(0); //use AscentClassic as the ascent path

            AscentClassic path = ascent.getAscentPathClassic();
            path.setTurnShapeExponent(0.5); //set the turn shape to 50%
            path.setAutoPath(false); //don't use autopath
            path.setTurnStartAltitude(3000);
            path.setTurnStartVelocity(120);
            path.setTurnEndAltitude(65000);

            ascent.setAutostage(true);
            ascent.setEnabled(true);
            sc.getActiveVessel().getControl().activateNextStage(); //launch the vessel

            Stream<Boolean> enabled = conn.addStream(ascent, "getEnabled");
            enabled.setRate(1); //we don't need a high throughput rate, 1 second is more than enough
            synchronized(enabled.getCondition()) {
                while(enabled.get())
                    enabled.waitForUpdate();
            }
            enabled.remove();

            System.out.println("Launch complete!");
        }
    }
}
