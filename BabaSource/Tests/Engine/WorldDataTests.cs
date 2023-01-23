﻿using Core.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Engine;

[TestFixture]
public class WorldDataTests
{
    [Test]
    public void Serialize()
    {
        var serialized = expectedCompiledMap.Serialize();

        Assert.AreEqual(expectedSerialized, serialized);
    }

    private const string expectedSerialized = """
        # world new [baba] city
        ---- BEGIN WORLD ----
        AQAAAA8AAABuZXcgW2JhYmFdIGNpdHk=
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
        AQAAAAAAAAAAAAAAAAAGAAAAZ2xvYmFs
        ACEAqAAAAAAAAAAEAAAABQAAAAAAAAAJAAAAdGV4dF9iYWJhAAMAKQEAAAAAAAAF
        AAAABQAAAAAAAAAHAAAAdGV4dF9pcwAhANkBAAAAAAAABgAAAAUAAAAAAAAACAAA
        AHRleHRfeW91
        ---- END MAP ----

        # 2 region 0 - autumnregion
        ---- BEGIN MAP ----
        AgAAAAAAAAAAAAAAAAAXAAAAcmVnaW9uIDAgLSBhdXR1bW5yZWdpb24=
        ADEADQAAAAAAAAACAAAAAgAAAAAAAAAEAAAAYm9hdAAxAA0AAAAAAAAABQAAAAQA
        AAAAAAAABAAAAGJvYXQ=
        ---- END MAP ----

        # 3 region 1 - region 2
        ---- BEGIN MAP ----
        AwAAAAAAAAAAAAAAAAATAAAAcmVnaW9uIDEgLSByZWdpb24gMg==

        ---- END MAP ----

        # 4 region 2 - new region 2
        ---- BEGIN MAP ----
        BAAAAAAAAAAAAAAAAAAXAAAAcmVnaW9uIDIgLSBuZXcgcmVnaW9uIDI=

        ---- END MAP ----

        # 5 region 3 - starting region
        ---- BEGIN MAP ----
        BQAAAAAAAAAAAAAAAAAaAAAAcmVnaW9uIDMgLSBzdGFydGluZyByZWdpb24=

        ---- END MAP ----

        # 7 6 uplayer - start
        ---- BEGIN MAP ----
        BwAAAAAAAAAAAAAAAAARAAAANiB1cGxheWVyIC0gc3RhcnQ=
        ACoAuwEAAAAAAAAAAAAAAAAAAAAAAAAJAAAAdGV4dF90cmVlAAMAKQEAAAAAAAAB
        AAAAAAAAAAAAAAAHAAAAdGV4dF9pcwApAK0BAAAAAAAAAgAAAAAAAAAAAAAACQAA
        AHRleHRfc3RvcAAqALwBAAAAAAAAAAAAAAEAAAAAAAAACgAAAHRleHRfdHJlZXMA
        AwApAQAAAAAAAAEAAAABAAAAAAAAAAcAAAB0ZXh0X2lzACkArQEAAAAAAAACAAAA
        AQAAAAAAAAAJAAAAdGV4dF9zdG9w
        ---- END MAP ----

        # 6 start
        ---- BEGIN MAP ----
        BgAAAAgAAAAAAAcAAwAFAAAAc3RhcnQ=
        ACoA4AEAAAAAAAAAAAAAAAAAAAAAAAAEAAAAdHJlZQAqAAQAAAAAAAAAAQAAAAAA
        AAAAAAAABAAAAGJhYmE=
        ---- END MAP ----

        # 9 8 uplayer - start2
        ---- BEGIN MAP ----
        CQAAAAAAAAAAAAAAAAASAAAAOCB1cGxheWVyIC0gc3RhcnQy
        ACoAuwEAAAAAAAACAAAAAQAAAAAAAAAJAAAAdGV4dF90cmVlAAMAKQEAAAAAAAAD
        AAAAAQAAAAAAAAAHAAAAdGV4dF9pcwApAK0BAAAAAAAABAAAAAEAAAAAAAAACQAA
        AHRleHRfc3RvcA==
        ---- END MAP ----

        # 8 start2
        ---- BEGIN MAP ----
        CAAAAAoAAAABAAkAAwAGAAAAc3RhcnQy
        ACoA4AEAAAAAAAAAAAAAAAAAAAAAAAAEAAAAdHJlZQAqAAEAAAAAAAAAAQAAAAAA
        AAAAAAAABgAAAGFtb25naQ==
        ---- END MAP ----

        # 11 10 uplayer - doggos cattos
        ---- BEGIN MAP ----
        CwAAAAAAAAAAAAAAAAAaAAAAMTAgdXBsYXllciAtIGRvZ2dvcyBjYXR0b3M=
        ADIAHwAAAAAAAAAHAAAABgAAAAAAAAADAAAAY2F0ABQABgAAAAAAAAAHAAAACgAA
        AAAAAAAGAAAAYmFuYW5hABEAoQAAAAAAAAADAAAAAwAAAAAAAAALAAAAdGV4dF9h
        bW9uZ2k=
        ---- END MAP ----

        # 10 doggos cattos
        ---- BEGIN MAP ----
        CgAAAAAAAAAIAAsAAQANAAAAZG9nZ29zIGNhdHRvcw==
        AAIALAAAAAAAAAACAAAAAQAAAAAAAAADAAAAZG9nAAIALAAAAAAAAAAEAAAABAAA
        AAAAAAADAAAAZG9nABEALAAAAAAAAAABAAAABwAAAAAAAAADAAAAZG9nAAMA7QEA
        AAAAAAAJAAAABwAAAAAAAAAEAAAAd29ybQARACwAAAAAAAAAAAAAAAUAAAAAAAAA
        AwAAAGRvZwARACwAAAAAAAAAAAAAAAoAAAAAAAAAAwAAAGRvZwARAAEAAAAAAAAA
        BgAAAAIAAAAAAAAABgAAAGFtb25naQACACwAAAAAAAAAAgAAAAEAAAAAAAAAAwAA
        AGRvZwACACwAAAAAAAAABAAAAAQAAAAAAAAAAwAAAGRvZwARACwAAAAAAAAAAQAA
        AAcAAAAAAAAAAwAAAGRvZwADAO0BAAAAAAAACQAAAAcAAAAAAAAABAAAAHdvcm0A
        EQAsAAAAAAAAAAAAAAAFAAAAAAAAAAMAAABkb2cAEQAsAAAAAAAAAAAAAAAKAAAA
        AAAAAAMAAABkb2cAEQABAAAAAAAAAAYAAAACAAAAAAAAAAYAAABhbW9uZ2kAAgAs
        AAAAAAAAAAIAAAABAAAAAAAAAAMAAABkb2cAAgAsAAAAAAAAAAQAAAAEAAAAAAAA
        AAMAAABkb2cAEQAsAAAAAAAAAAEAAAAHAAAAAAAAAAMAAABkb2cAAwDtAQAAAAAA
        AAkAAAAHAAAAAAAAAAQAAAB3b3JtABEALAAAAAAAAAAAAAAABQAAAAAAAAADAAAA
        ZG9nABEALAAAAAAAAAAAAAAACgAAAAAAAAADAAAAZG9nABEAAQAAAAAAAAAGAAAA
        AgAAAAAAAAAGAAAAYW1vbmdp
        ---- END MAP ----
        """;

    private static WorldData expectedCompiledMap = new WorldData()
    {
        GlobalWordMapId = 1,
        Name = "new [baba] city",
        Maps = new() {
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 33,
                    ObjectId = 168,
                    Facing = 0,
                    x = 4,
                    y = 5,
                    Name = "text_baba",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 297,
                    Facing = 0,
                    x = 5,
                    y = 5,
                    Name = "text_is",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 33,
                    ObjectId = 473,
                    Facing = 0,
                    x = 6,
                    y = 5,
                    Name = "text_you",
                } }) {
                MapId = 1,
                Name = "global",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 49,
                    ObjectId = 13,
                    Facing = 0,
                    x = 2,
                    y = 2,
                    Name = "boat",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 49,
                    ObjectId = 13,
                    Facing = 0,
                    x = 5,
                    y = 4,
                    Name = "boat",
                } }) {
                MapId = 2,
                Name = "region 0 - autumnregion",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {  }) {
                MapId = 3,
                Name = "region 1 - region 2",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {  }) {
                MapId = 4,
                Name = "region 2 - new region 2",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {  }) {
                MapId = 5,
                Name = "region 3 - starting region",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 443,
                    Facing = 0,
                    x = 0,
                    y = 0,
                    Name = "text_tree",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 297,
                    Facing = 0,
                    x = 1,
                    y = 0,
                    Name = "text_is",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 41,
                    ObjectId = 429,
                    Facing = 0,
                    x = 2,
                    y = 0,
                    Name = "text_stop",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 444,
                    Facing = 0,
                    x = 0,
                    y = 1,
                    Name = "text_trees",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 297,
                    Facing = 0,
                    x = 1,
                    y = 1,
                    Name = "text_is",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 41,
                    ObjectId = 429,
                    Facing = 0,
                    x = 2,
                    y = 1,
                    Name = "text_stop",
                } }) {
                MapId = 7,
                Name = "6 uplayer - start",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 480,
                    Facing = 0,
                    x = 0,
                    y = 0,
                    Name = "tree",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 4,
                    Facing = 0,
                    x = 1,
                    y = 0,
                    Name = "baba",
                } }) {
                MapId = 6,
                Name = "start",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 8,
                westNeighbor = 0,
                upLayer = 7,
                region = 3,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 443,
                    Facing = 0,
                    x = 2,
                    y = 1,
                    Name = "text_tree",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 297,
                    Facing = 0,
                    x = 3,
                    y = 1,
                    Name = "text_is",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 41,
                    ObjectId = 429,
                    Facing = 0,
                    x = 4,
                    y = 1,
                    Name = "text_stop",
                } }) {
                MapId = 9,
                Name = "8 uplayer - start2",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 480,
                    Facing = 0,
                    x = 0,
                    y = 0,
                    Name = "tree",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 42,
                    ObjectId = 1,
                    Facing = 0,
                    x = 1,
                    y = 0,
                    Name = "amongi",
                } }) {
                MapId = 8,
                Name = "start2",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 10,
                westNeighbor = 1,
                upLayer = 9,
                region = 3,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 50,
                    ObjectId = 31,
                    Facing = 0,
                    x = 7,
                    y = 6,
                    Name = "cat",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 20,
                    ObjectId = 6,
                    Facing = 0,
                    x = 7,
                    y = 10,
                    Name = "banana",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 161,
                    Facing = 0,
                    x = 3,
                    y = 3,
                    Name = "text_amongi",
                } }) {
                MapId = 11,
                Name = "10 uplayer - doggos cattos",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 0,
                upLayer = 0,
                region = 0,
            },
            new MapData(new ObjectData[] {
                new ObjectData() {
                    Occupied = false,
                    Color = 2,
                    ObjectId = 44,
                    Facing = 0,
                    x = 2,
                    y = 1,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 2,
                    ObjectId = 44,
                    Facing = 0,
                    x = 4,
                    y = 4,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 1,
                    y = 7,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 493,
                    Facing = 0,
                    x = 9,
                    y = 7,
                    Name = "worm",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 0,
                    y = 5,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 0,
                    y = 10,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 1,
                    Facing = 0,
                    x = 6,
                    y = 2,
                    Name = "amongi",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 2,
                    ObjectId = 44,
                    Facing = 0,
                    x = 2,
                    y = 1,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 2,
                    ObjectId = 44,
                    Facing = 0,
                    x = 4,
                    y = 4,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 1,
                    y = 7,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 493,
                    Facing = 0,
                    x = 9,
                    y = 7,
                    Name = "worm",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 0,
                    y = 5,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 0,
                    y = 10,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 1,
                    Facing = 0,
                    x = 6,
                    y = 2,
                    Name = "amongi",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 2,
                    ObjectId = 44,
                    Facing = 0,
                    x = 2,
                    y = 1,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 2,
                    ObjectId = 44,
                    Facing = 0,
                    x = 4,
                    y = 4,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 1,
                    y = 7,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 3,
                    ObjectId = 493,
                    Facing = 0,
                    x = 9,
                    y = 7,
                    Name = "worm",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 0,
                    y = 5,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 44,
                    Facing = 0,
                    x = 0,
                    y = 10,
                    Name = "dog",
                },
                new ObjectData() {
                    Occupied = false,
                    Color = 17,
                    ObjectId = 1,
                    Facing = 0,
                    x = 6,
                    y = 2,
                    Name = "amongi",
                },
            }) {
                MapId = 10,
                Name = "doggos cattos",
                northNeighbor = 0,
                southNeighbor = 0,
                eastNeighbor = 0,
                westNeighbor = 8,
                upLayer = 11,
                region = 1,
            } },
        Regions = new() {
            new RegionData() {
                RegionId = 0,
                WordLayerId = 2,
                Theme = "autumn",
                Music = "editorsong",
                Name = "autumnregion"
            },
            new RegionData() {
                RegionId = 1,
                WordLayerId = 3,
                Theme = "garden",
                Music = "default",
                Name = "region 2"
            },
            new RegionData() {
                RegionId = 2,
                WordLayerId = 4,
                Theme = "swamp",
                Music = "default",
                Name = "new region 2"
            },
            new RegionData() {
                RegionId = 3,
                WordLayerId = 5,
                Theme = "garden",
                Music = "default",
                Name = "starting region"
            } }
    };
}
