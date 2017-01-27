using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener {

    public override void SceneLoadLocalDone(string map)
    {
        Vector2 pos = new Vector2(Random.Range(-5, 5), Random.Range(-5, -5));

        //bolt instantiation from prefab
        BoltNetwork.Instantiate(BoltPrefabs.chara_Net, pos, Quaternion.identity);
    }
}