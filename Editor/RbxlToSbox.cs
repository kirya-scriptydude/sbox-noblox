using Editor;
using Sandbox;
using RbxlReader;
using RbxlReader.Instances;

public static class RbxlToSbox {

    public static void ImportFile(string filePath, Scene scene) {
        PlaceBinary place = new(filePath);

        GameObject root = scene.CreateObject();
        root.Name = "Root";
        root.Tags.Add("rbxl");
        recursiveInstanceHandler(place.Workspace, scene, root);

        Log.Info("Handling properties...");
    }

    private static void recursiveInstanceHandler(Instance parent, Scene scene, GameObject previousObj) {
        Instance[] instances = parent.GetChildren();
        if (instances.Length == 0) return;

        foreach(Instance instance in instances) {
            var gameObj = scene.CreateObject();
            gameObj.Name = instance.Name;
            gameObj.Parent = previousObj;
            recursiveInstanceHandler(instance, scene, gameObj);
        }
    }

}