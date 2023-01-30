﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class WorldDataSerialized
{

    public const string expectedSerialized = """
        # world new [baba] city
        ---- BEGIN WORLD ----
        AQAPAAAAbmV3IFtiYWJhXSBjaXR5
        ---- END WORLD ----

        # 0 autumnregion
        ---- BEGIN REGION ----
        AAACAAYAAABhdXR1bW4KAAAAZWRpdG9yc29uZwwAAABhdXR1bW5yZWdpb24=
        ---- END REGION ----

        # 1 region 2
        ---- BEGIN REGION ----
        AQADAAYAAABnYXJkZW4HAAAAZGVmYXVsdAgAAAByZWdpb24gMg==
        ---- END REGION ----

        # 2 new region 2
        ---- BEGIN REGION ----
        AgAEAAUAAABzd2FtcAcAAABkZWZhdWx0DAAAAG5ldyByZWdpb24gMg==
        ---- END REGION ----

        # 3 starting region
        ---- BEGIN REGION ----
        AwAFAAYAAABnYXJkZW4HAAAAZGVmYXVsdA8AAABzdGFydGluZyByZWdpb24=
        ---- END REGION ----

        # 1 global
        ---- BEGIN MAP ----
        IAAAAAAQAAAAAAAAAAAAAAAAAGAAAAZ2xvYmFspAAAAAACEABAAAAAAAAAAEAAAA
        BQAAAAAAAAABAAAAAAAAAAAAAAQAAABiYWJhAAMAKwEAAAAAAAAFAAAABQAAAAAA
        AAABAAAAAAAAAAAAAAIAAABpcwAhAN0BAAAAAAAABgAAAAUAAAAAAAAAAQAAAAAA
        AAAAAAADAAAAeW91
        ---- END MAP ----

        # 2 region 0 - autumnregion
        ---- BEGIN MAP ----
        OAAAAAAgAAAAAAAAAAAAAAAAAXAAAAcmVnaW9uIDAgLSBhdXR1bW5yZWdpb24=cA
        AAAAADEADQAAAAAAAAACAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAQAAABib2F0ADEA
        DQAAAAAAAAAFAAAABAAAAAAAAAAAAAAAAAAAAAAAAAQAAABib2F0
        ---- END MAP ----

        # 3 region 1 - region 2
        ---- BEGIN MAP ----
        NAAAAAAwAAAAAAAAAAAAAAAAATAAAAcmVnaW9uIDEgLSByZWdpb24gMg==AAAAAA
        ---- END MAP ----

        # 4 region 2 - new region 2
        ---- BEGIN MAP ----
        OAAAAABAAAAAAAAAAAAAAAAAAXAAAAcmVnaW9uIDIgLSBuZXcgcmVnaW9uIDI=AA
        AAAA
        ---- END MAP ----

        # 5 region 3 - starting region
        ---- BEGIN MAP ----
        PAAAAABQAAAAAAAAAAAAAAAAAaAAAAcmVnaW9uIDMgLSBzdGFydGluZyByZWdpb2
        4=AAAAAA
        ---- END MAP ----

        # 7 6 uplayer - start
        ---- BEGIN MAP ----
        MAAAAABwAAAAAAAAAAAAAAAAARAAAANiB1cGxheWVyIC0gc3RhcnQ=TAEAAAACoA
        5AEAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAQAAAB0cmVlAAMAKwEAAAAA
        AAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAIAAABpcwApALEBAAAAAAAAAgAAAAAA
        AAAAAAAAAQAAAAAAAAAAAAAEAAAAc3RvcAAqAOUBAAAAAAAAAAAAAAEAAAAAAAAA
        AQAAAAAAAAAAAAAFAAAAdHJlZXMAAwArAQAAAAAAAAEAAAABAAAAAAAAAAEAAAAA
        AAAAAAAAAgAAAGlzACkAsQEAAAAAAAACAAAAAQAAAAAAAAABAAAAAAAAAAAAAAQA
        AABzdG9w
        ---- END MAP ----

        # 6 start
        ---- BEGIN MAP ----
        IAAAAABgAAAAgAAAAAAAcAAwAFAAAAc3RhcnQ=cAAAAAACoA5AEAAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAAAAAQAAAB0cmVlACoABAAAAAAAAAABAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAQAAABiYWJh
        ---- END MAP ----

        # 9 8 uplayer - start2
        ---- BEGIN MAP ----
        MAAAAACQAAAAAAAAAAAAAAAAASAAAAOCB1cGxheWVyIC0gc3RhcnQyqAAAAAACoA
        5AEAAAAAAAACAAAAAQAAAAAAAAABAAAAAAAAAAAAAAQAAAB0cmVlAAMAKwEAAAAA
        AAADAAAAAQAAAAAAAAABAAAAAAAAAAAAAAIAAABpcwApALEBAAAAAAAABAAAAAEA
        AAAAAAAAAQAAAAAAAAAAAAAEAAAAc3RvcA==
        ---- END MAP ----

        # 8 start2
        ---- BEGIN MAP ----
        IAAAAACAAAAAoAAAABAAkAAwAGAAAAc3RhcnQydAAAAAACoA5AEAAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAAAAAQAAAB0cmVlACoAAQAAAAAAAAABAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAYAAABhbW9uZ2k=
        ---- END MAP ----

        # 11 10 uplayer - doggos cattos
        ---- BEGIN MAP ----
        PAAAAACwAAAAAAAAAAAAAAAAAaAAAAMTAgdXBsYXllciAtIGRvZ2dvcyBjYXR0b3
        M=rAAAAAADIAHwAAAAAAAAAHAAAABgAAAAAAAAAAAAAAAAAAAAAAAAMAAABjYXQA
        FAAGAAAAAAAAAAcAAAAKAAAAAAAAAAAAAAAAAAAAAAAABgAAAGJhbmFuYQARAAEA
        AAAAAAAAAwAAAAMAAAAAAAAAAQAAAAAAAAAAAAAGAAAAYW1vbmdp
        ---- END MAP ----

        # 10 doggos cattos
        ---- BEGIN MAP ----
        LAAAAACgAAAAAAAAAIAAsAAQANAAAAZG9nZ29zIGNhdHRvcw==hAEAAAAAIALAAA
        AAAAAAACAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAMAAABkb2cAAgAsAAAAAAAAAAQA
        AAAEAAAAAAAAAAAAAAAAAAAAAAAAAwAAAGRvZwARACwAAAAAAAAAAQAAAAcAAAAA
        AAAAAAAAAAAAAAAAAAADAAAAZG9nAAMA8QEAAAAAAAAJAAAABwAAAAAAAAAAAAAA
        AAAAAAAAAAQAAAB3b3JtABEALAAAAAAAAAAAAAAABQAAAAAAAAAAAAAAAAAAAAAA
        AAMAAABkb2cAEQAsAAAAAAAAAAAAAAAKAAAAAAAAAAAAAAAAAAAAAAAAAwAAAGRv
        ZwARAAEAAAAAAAAABgAAAAIAAAAAAAAAAAAAAAAAAAAAAAAGAAAAYW1vbmdp
        ---- END MAP ----
        """;
}
