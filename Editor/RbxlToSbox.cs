using Editor;
using Sandbox;
using RbxlReader;
using RbxlReader.Instances;
using System.Linq;

public static class RbxlToSbox {

    public static void ImportFile(string filePath, Scene scene) {
        PlaceBinary place = new(filePath);

        foreach (GameObject obj in scene.Children) {
            if (obj.Tags.Has("rbxl")) {
                Log.Info("Found old .rbxl import object. Rewritting...");
                obj.Destroy();
            }
        }

        GameObject root = scene.CreateObject();
        root.Name = "Root";
        root.Tags.Add("rbxl");
        recursiveInstanceHandler(place.Workspace, scene, root);
    }

    private static void recursiveInstanceHandler(Instance parent, Scene scene, GameObject previousObj) {
        Instance[] instances = parent.GetChildren();
        if (instances.Length == 0) return;

        foreach(Instance instance in instances) {
            var gameObj = scene.CreateObject();
            gameObj.Parent = previousObj;
            handleProperties(instance, gameObj);

            recursiveInstanceHandler(instance, scene, gameObj);
        }
    }

    private static void handleProperties(Instance instance, GameObject gameObject) {
        var comp = gameObject.AddComponent<InstanceComponent>(true);
        comp.InstanceId = instance.Id;
        gameObject.Name = instance.Name;
        comp.ClassName = instance.ClassName;
    }

}