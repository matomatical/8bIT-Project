using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
    public class HeaderManager {
        
        // Strips off the header of an update, returns information contained in the header
        public static List<byte> StripHeader(List<byte> data) {
            // get the protocol
            byte protocol = data[0];
            List<byte> headerInfo = new List<byte>();

            // add protocol to header and remove from message
            headerInfo.Add(protocol);
            data.RemoveAt(0);

            if (protocol == UpdateManager.PROTOCOL_VERSION) {
                // get the update type
                headerInfo.Add(data[0]);
                // remove the update type
                data.RemoveAt(0);
            }

            return headerInfo;
        }

        // Applies a header to an upddate (data), at this point just a protocol version and update type
         public static void ApplyHeader(List<byte> data, byte type) {
            data.Insert(0, UpdateManager.PROTOCOL_VERSION);
            data.Insert(1, type);
        }
    }
}
