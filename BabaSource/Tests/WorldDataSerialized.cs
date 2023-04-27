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
        AQAAAAEADwAAAG5ldyBbYmFiYV0gY2l0eQ==
        ---- END WORLD ----

        # 0 autumnregion
        ---- BEGIN REGION ----
        AAABAAAAAgAGAAAAYXV0dW1uCgAAAGVkaXRvcnNvbmcMAAAAYXV0dW1ucmVnaW9u
        ---- END REGION ----

        # 1 region 2
        ---- BEGIN REGION ----
        AQABAAAAAwAGAAAAZ2FyZGVuBwAAAGRlZmF1bHQIAAAAcmVnaW9uIDI=
        ---- END REGION ----

        # 2 new region 2
        ---- BEGIN REGION ----
        AgABAAAABAAFAAAAc3dhbXAHAAAAZGVmYXVsdAwAAABuZXcgcmVnaW9uIDI=
        ---- END REGION ----

        # 3 starting region
        ---- BEGIN REGION ----
        AwABAAAABQAGAAAAZ2FyZGVuBwAAAGRlZmF1bHQPAAAAc3RhcnRpbmcgcmVnaW9u
        ---- END REGION ----

        # 1 global
        ---- BEGIN MAP ----
        KAAAAAAQAAAAAAAAAAAAAAAAASABIABgAAAGdsb2JhbA==oAAAAAACEAAAAAAAQA
        AAAFAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAABAAAAAADAAAAAAAFAAAABQAAAAAA
        AAABAAAAAAAAAAAAAAAAAAAAAN0AAAAAIQAAAAAABgAAAAUAAAAAAAAAAQAAAAAA
        AAAAAAAAAAAAAAA5AQAA
        ---- END MAP ----

        # 2 region 0 - autumnregion
        ---- BEGIN MAP ----
        PAAAAAAgAAAAAAAAAAAAAAAAAKAAoAFwAAAHJlZ2lvbiAwIC0gYXV0dW1ucmVnaW
        9ugAAAAAADEAAAAAAAIAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAOAAAAdGhpcyBp
        cyBhIGJvYXQNAAAAADEAAAAAAAUAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        DQAAAA==
        ---- END MAP ----

        # 3 region 1 - region 2
        ---- BEGIN MAP ----
        OAAAAAAwAAAAAAAAAAAAAAAAASABIAEwAAAHJlZ2lvbiAxIC0gcmVnaW9uIDI=AA
        AAAA
        ---- END MAP ----

        # 4 region 2 - new region 2
        ---- BEGIN MAP ----
        PAAAAABAAAAAAAAAAAAAAAAAASABIAFwAAAHJlZ2lvbiAyIC0gbmV3IHJlZ2lvbi
        AyAAAAAA
        ---- END MAP ----

        # 5 region 3 - starting region
        ---- BEGIN MAP ----
        QAAAAABQAAAAAAAAAAAAAAAAAPAA8AGgAAAHJlZ2lvbiAzIC0gc3RhcnRpbmcgcm
        VnaW9uAAAAAA
        ---- END MAP ----

        # 7 6 uplayer - start
        ---- BEGIN MAP ----
        NAAAAABwAAAAAAAAAAAAAAAAAPAA8AEQAAADYgdXBsYXllciAtIHN0YXJ0QAEAAA
        ACoAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAQAEAAAADAAAAAAAB
        AAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAN0AAAAAKQAAAAAAAgAAAAAAAAAA
        AAAAAQAAAAAAAAAAAAAAAAAAAAAkAQAAACoAAAAAAAAAAAABAAAAAAAAAAEAAAAA
        AAAAAAAAAAAAAAAAQQEAAAADAAAAAAABAAAAAQAAAAAAAAABAAAAAAAAAAAAAAAA
        AAAAAN0AAAAAKQAAAAAAAgAAAAEAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAkAQAA
        ---- END MAP ----

        # 6 start
        ---- BEGIN MAP ----
        JAAAAABgAAAAgAAAAAAAcAAwAPAA8ABQAAAHN0YXJ0bAAAAAACoAAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAEAAAAqAAAAAAABAAAAAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAQAAAA=
        ---- END MAP ----

        # 9 8 uplayer - start2
        ---- BEGIN MAP ----
        OAAAAACQAAAAAAAAAAAAAAAAAPAA8AEgAAADggdXBsYXllciAtIHN0YXJ0Mg==oA
        AAAAACoAAAAAAAIAAAABAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAQAEAAAADAAAA
        AAADAAAAAQAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAN0AAAAAKQAAAAAABAAAAAEA
        AAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAkAQAA
        ---- END MAP ----

        # 8 start2
        ---- BEGIN MAP ----
        KAAAAACAAAAAoAAAAGAAkAAwAPAA8ABgAAAHN0YXJ0Mg==bAAAAAACoAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAEAAAAqAAAAAAABAAAAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAAAAAEAAAA=
        ---- END MAP ----

        # 11 10 uplayer - doggos cattos
        ---- BEGIN MAP ----
        QAAAAACwAAAAAAAAAAAAAAAAAPAA8AGgAAADEwIHVwbGF5ZXIgLSBkb2dnb3MgY2
        F0dG9zoAAAAAADIAAAAAAAcAAAAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHwAA
        AAAUAAAAAAAHAAAACgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAYAAAAAEQAAAAAA
        AwAAAAMAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAABAAAA
        ---- END MAP ----

        # 10 doggos cattos
        ---- BEGIN MAP ----
        MAAAAACgAAAAAAAAAIAAsAAQAPAA8ADQAAAGRvZ2dvcyBjYXR0b3M=eAEAAAAAIA
        AAAAAAIAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALAAAAAACAAAAAAAEAAAA
        BAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACwAAAAAEQAAAAAAAQAAAAcAAAAAAAAA
        AAAAAAAAAAAAAAAAAAAAAAAsAAAAAAMAAAAAAAkAAAAHAAAAAAAAAAAAAAAAAAAA
        AAAAAAAAAAAATQEAAAARAAAAAAAAAAAABQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        ACwAAAAAEQAAAAAAAAAAAAoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAsAAAAABEA
        AAAAAAYAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAA==
        ---- END MAP ----
        """;
}
