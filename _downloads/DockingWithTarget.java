import java.io.IOException;

import krpc.client.Connection;
import krpc.client.RPCException;
import krpc.client.Stream;
import krpc.client.StreamException;
import krpc.client.services.MechJeb;
import krpc.client.services.MechJeb.DockingAutopilot;
import krpc.client.services.SpaceCenter;
import krpc.client.services.SpaceCenter.DockingPort;
import krpc.client.services.SpaceCenter.Parts;
import krpc.client.services.SpaceCenter.Vessel;

public class DockingWithTarget {
    public static void main(String[] args) throws IOException, RPCException, StreamException {
        //This script assumes the vessel is next to the target and the target is a ship.
        Connection conn = Connection.newInstance("Docking with a target");

        SpaceCenter sc = SpaceCenter.newInstance(conn);
        MechJeb mj = MechJeb.newInstance(conn);
        Vessel active = sc.getActiveVessel();

        System.out.println("Setting the first docking port as the controlling part");
        Parts parts = active.getParts();
        parts.setControlling(parts.getDockingPorts().get(0).getPart());

        System.out.println("Looking for a free docking port attached to the target vessel");
        for(DockingPort dp : sc.getTargetVessel().getParts().getDockingPorts())
            if(dp.getDockedPart() == null) {
                sc.setTargetDockingPort(dp);
                break;
            }

        System.out.println("Starting the docking process");
        DockingAutopilot docking = mj.getDockingAutopilot();
        docking.setEnabled(true);

        Stream<Boolean> enabled = conn.addStream(docking, "getEnabled");
        enabled.setRate(1); //we don't need a high throughput rate, 1 second is more than enough
        synchronized(enabled.getCondition()) {
            while(enabled.get())
                enabled.waitForUpdate();
        }
        enabled.remove();

        System.out.println("Docking complete!");
        conn.close();
    }
}
